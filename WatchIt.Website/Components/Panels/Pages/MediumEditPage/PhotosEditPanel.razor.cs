using System.Drawing;
using System.Net;
using Blazorise;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Refit;
using WatchIt.DTO.Models.Controllers.Photos;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Controllers.Photos.PhotoBackground;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;
using Color = System.Drawing.Color;

namespace WatchIt.Website.Components.Panels.Pages.MediumEditPage;

public partial class PhotosEditPanel : Component
{
    #region SERVICES

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] public IPhotosClient PhotosClient { get; set; } = null!;

    #endregion



    #region PARAMETERS
    
    [Parameter] public long? Id { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private IEnumerable<PhotoResponse> _photos = new List<PhotoResponse>();
    private List<Guid> _photoDeleting = new List<Guid>();
    private bool _photoEditMode;
    private Guid? _photoEditId;
    private bool _photoEditSaving;
    private bool _photoEditIsBackground;
    private PhotoRequest? _photoEditRequest;
    private PhotoBackgroundRequest? _photoEditBackgroundData = new PhotoBackgroundRequest()
    {
        FirstGradientColor = Color.FromArgb(0xFF, 0xFF, 0xFF),
        SecondGradientColor = Color.FromArgb(0x00, 0x00, 0x00),
        IsUniversal = false
    };
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();

        if (Id.HasValue)
        {
            IApiResponse<IEnumerable<PhotoResponse>> response = await PhotosClient.GetPhotos(new PhotoFilterQuery
            {
                MediumId = Id.Value,
            });
            if (response.IsSuccessful)
            {
                _photos = response.Content;
            }
            else
            {
                await Base.SnackbarStack.PushAsync("An error occured. Photos cannot be downloaded.", SnackbarColor.Danger);
            }
        }
        
        _loaded = true;
        StateHasChanged();
    }

    private async Task DeletePhoto(Guid id)
    {
        _photoDeleting.Add(id);

        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;
        
        IApiResponse response = await PhotosClient.DeletePhoto(token, id);

        if (response.IsSuccessful)
        {
            NavigationManager.Refresh(true);
        }
        else
        {
            _photoDeleting.Remove(id);
            await Base.SnackbarStack.PushAsync("An error occured. Photo cannot be removed.", SnackbarColor.Danger);
        }
    }

    private void InitEditPhoto(Guid? id)
    {
        _photoEditMode = true;
        _photoEditId = id;
        _photoEditRequest = null;
        _photoEditIsBackground = false;
        _photoEditBackgroundData = new PhotoBackgroundRequest
        {
            FirstGradientColor = Color.FromArgb(0xFF, 0xFF, 0xFF),
            SecondGradientColor = Color.FromArgb(0x00, 0x00, 0x00),
            IsUniversal = false
        };
        Console.WriteLine(ColorTranslator.ToHtml(_photoEditBackgroundData.FirstGradientColor));
        if (id is not null)
        {
            PhotoResponse response = _photos.First(x => x.Id == id);
            _photoEditRequest = response.ToRequest();
            if (_photoEditRequest.BackgroundData is not null)
            {
                _photoEditIsBackground = true;
                _photoEditBackgroundData = _photoEditRequest.BackgroundData;
            }
        }
    }

    private void CancelEditPhoto()
    {
        _photoEditMode = false;
        _photoEditId = null;
    }

    private async Task SaveEditPhoto()
    {
        _photoEditSaving = true;
        
        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;

        IApiResponse response;
        _photoEditRequest.BackgroundData = _photoEditIsBackground ? _photoEditBackgroundData : null;
        if (_photoEditId is null)
        {
            response = await PhotosClient.PostPhoto(token, _photoEditRequest);
        }
        else
        {
            response = await PhotosClient.PutPhoto(token, _photoEditId.Value, _photoEditRequest);
        }

        switch (response)
        {
            case { IsSuccessful: true }:
                await Base.SnackbarStack.PushAsync("Photo saved successfully.", SnackbarColor.Success);
                NavigationManager.Refresh(true);
                break;
            case { StatusCode: HttpStatusCode.Forbidden } or { StatusCode: HttpStatusCode.Unauthorized }:
                await Base.SnackbarStack.PushAsync("You are not authorized to edit photos.", SnackbarColor.Danger);
                break;
            case { StatusCode: HttpStatusCode.BadRequest }:
                string? content = "An unknown error occured.";
                if (response.Error is ValidationApiException ex)
                {
                    string? exContent = ex.Content?.Errors.SelectMany(x => x.Value).FirstOrDefault();
                    if (exContent is not null)
                    {
                        content = exContent;
                    }
                }
                await Base.SnackbarStack.PushAsync(content, SnackbarColor.Danger);
                break;
            default:
                await Base.SnackbarStack.PushAsync("An unknown error occured.", SnackbarColor.Danger);
                break;
        }
        
        _photoEditMode = false;
        _photoEditId = null;
    }

    private async Task LoadPhoto(InputFileChangeEventArgs args)
    {
        if (args.File.ContentType.StartsWith("image"))
        {
            Stream stream = args.File.OpenReadStream(5242880);
            byte[] array;
            using (MemoryStream ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                array = ms.ToArray();
            }
            
            _photoEditRequest = new PhotoRequest
            {
                MediumId = Id!.Value,
                Image = array,
                MimeType = args.File.ContentType
            };
        }
    }

    private void EditPhotoFirstGradientColorChanged(ChangeEventArgs e)
    {
        _photoEditBackgroundData.FirstGradientColor = ColorTranslator.FromHtml(e.Value.ToString());
    }

    private void EditPhotoSecondGradientColorChanged(ChangeEventArgs e)
    {
        _photoEditBackgroundData.SecondGradientColor = ColorTranslator.FromHtml(e.Value.ToString());
    }
    
    #endregion
}
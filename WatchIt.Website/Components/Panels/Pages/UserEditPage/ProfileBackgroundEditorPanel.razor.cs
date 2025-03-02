using System.Net;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Accounts.AccountBackgroundPicture;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Panels.Pages.UserEditPage;

public partial class ProfileBackgroundEditorPanel : Component
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IMediaClient MediaClient { get; set; } = null!;
    [Inject] private IPhotosClient PhotosClient { get; set; } = null!;
    [Inject] private IAccountsClient AccountsClient { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required long Id { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private bool _editMode;
    private long? _selectedMedia;
    private IEnumerable<PhotoResponse>? _mediaPhotos;
    private bool _backgroundsLoading;
    private bool _saveLoading;

    private bool _removeLoading;

    private IEnumerable<MediumResponse> _mediaList = null!;
    
    private PhotoResponse? _selectedPhoto;
    private MediumResponse? _selectedPhotoMedia;

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await LoadMedia();

        _selectedPhoto = Base.CustomBackground;
        if (_selectedPhoto is not null)
        {
            _selectedPhotoMedia = _mediaList.First(x => x.Id == _selectedPhoto.MediumId);
        }
            
        _loaded = true;
        StateHasChanged();
    }

    private async Task LoadMedia()
    {
        IApiResponse<IEnumerable<MediumResponse>> response = await MediaClient.GetMedia();
        if (response.IsSuccessful)
        {
            _mediaList = response.Content;
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error occured. List of media could not be loaded.", SnackbarColor.Danger);
        }
    }

    private async Task Save(Guid id)
    {
        _saveLoading = true;
        
        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;

        IApiResponse<PhotoResponse> response = await AccountsClient.PutAccountBackgroundPicture(token, new AccountBackgroundPictureRequest { Id = id });
        
        switch (response)
        {
            case { IsSuccessful: true}:
                Base.CustomBackground = response.Content;
                _selectedPhoto = Base.CustomBackground;
                _selectedPhotoMedia = _mediaList.First(x => x.Id == _selectedMedia!.Value);
                await Base.SnackbarStack.PushAsync("Background picture successfully saved.", SnackbarColor.Success);
                break;
            case { StatusCode: HttpStatusCode.Forbidden } or { StatusCode: HttpStatusCode.Unauthorized }:
                await Base.SnackbarStack.PushAsync("Authentication error", SnackbarColor.Danger);
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
        _saveLoading = false;
        Cancel();
    }

    private void Cancel()
    {
        _editMode = false;
        _selectedMedia = null;
        _saveLoading = false;
        _backgroundsLoading = false;
        _mediaPhotos = null;
    }

    private void Edit()
    {
        _editMode = true;
    }
    
    private async Task Remove()
    {
        _removeLoading = true;
        
        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;
        
        IApiResponse response = await AccountsClient.DeleteAccountBackgroundPicture(token);
        if (response.IsSuccessful)
        {
            Base.CustomBackground = null;
            _selectedPhoto = null;
            _selectedPhotoMedia = null;
            await Base.SnackbarStack.PushAsync("Background picture successfully removed.", SnackbarColor.Success);
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error occured. Background picture could not be removed.", SnackbarColor.Danger);
        }
        
        _removeLoading = false;
    }

    private async Task LoadBackgrounds()
    {
        _backgroundsLoading = true;

        IApiResponse<IEnumerable<PhotoResponse>> response = await PhotosClient.GetPhotos(new PhotoFilterQuery()
        {
            IsBackground = true,
            MediumId = _selectedMedia!.Value
        });
        if (!response.IsSuccessful)
        {
            await Base.SnackbarStack.PushAsync("An error occured. Background photos could not be obtained.", SnackbarColor.Danger);
        }
        
        _mediaPhotos = response.Content;
        _backgroundsLoading = false;
    }
    
    #endregion
}
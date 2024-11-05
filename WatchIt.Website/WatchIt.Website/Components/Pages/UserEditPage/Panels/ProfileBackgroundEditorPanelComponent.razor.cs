using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Photos;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Client.Accounts;
using WatchIt.Website.Services.Client.Media;

namespace WatchIt.Website.Components.Pages.UserEditPage.Panels;

public partial class ProfileBackgroundEditorPanelComponent : ComponentBase
{
    #region SERVICES

    [Inject] private IMediaClientService MediaClientService { get; set; } = default!;
    [Inject] private IAccountsClientService AccountsClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required long Id { get; set; }
    [Parameter] public Action<PhotoResponse?>? OnBackgroundChanged { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private bool _editMode;
    private long? _selectedMedia;
    private IEnumerable<PhotoResponse>? _mediaPhotos;
    private bool _backgroundsLoading;
    private bool _saveLoading;

    private bool _removeLoading;

    private IEnumerable<MediaResponse> _mediaList = default!;
    
    private PhotoResponse? _selectedPhoto;
    private MediaResponse? _selectedPhotoMedia;

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Task.WhenAll(
            [
                MediaClientService.GetAllMedia(successAction: data => _mediaList = data),
                AccountsClientService.GetAccountProfileBackground(Id, data => _selectedPhoto = data)
            ]);

            if (_selectedPhoto is not null)
            {
                _selectedPhotoMedia = _mediaList.First(x => x.Id == _selectedPhoto.MediaId);
            }
            
            _loaded = true;
            StateHasChanged();
        }
    }

    private async Task Save(Guid id)
    {
        _saveLoading = true;
        await AccountsClientService.PutAccountProfileBackground(new AccountProfileBackgroundRequest(id), data =>
        {
            OnBackgroundChanged?.Invoke(data);
            _selectedPhoto = data;
            _selectedPhotoMedia = _mediaList.First(x => x.Id == _selectedMedia!.Value);
            _saveLoading = false;
            Cancel();
        });
    }

    private void Cancel()
    {
        _editMode = false;
        _selectedMedia = default;
        _saveLoading = false;
        _backgroundsLoading = false;
        _mediaPhotos = default;
    }

    private void Edit()
    {
        _editMode = true;
    }
    
    private async Task Remove()
    {
        _removeLoading = true;
        await AccountsClientService.DeleteAccountProfileBackground(() =>
        {
            OnBackgroundChanged?.Invoke(null);
            _selectedPhoto = null;
            _selectedPhotoMedia = null;
            _removeLoading = false;
        });
    }

    private async Task LoadBackgrounds()
    {
        _backgroundsLoading = true;
        PhotoQueryParameters query = new PhotoQueryParameters
        {
            IsBackground = true
        };
        await MediaClientService.GetMediaPhotos(_selectedMedia!.Value, query, successAction: data =>
        {
            _mediaPhotos = data;
            _backgroundsLoading = false;
        });
    }
    
    #endregion
}
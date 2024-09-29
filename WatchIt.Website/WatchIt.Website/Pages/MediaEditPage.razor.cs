using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Movies;
using WatchIt.Common.Model.Photos;
using WatchIt.Common.Model.Series;
using WatchIt.Website.Layout;
using WatchIt.Website.Services.Utility.Authentication;
using WatchIt.Website.Services.WebAPI.Media;
using WatchIt.Website.Services.WebAPI.Movies;
using WatchIt.Website.Services.WebAPI.Photos;
using WatchIt.Website.Services.WebAPI.Series;

namespace WatchIt.Website.Pages;

public partial class MediaEditPage : ComponentBase
{
    #region SERVICES

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] public IMediaWebAPIService MediaWebAPIService { get; set; } = default!;
    [Inject] public IMoviesWebAPIService MoviesWebAPIService { get; set; } = default!;
    [Inject] public ISeriesWebAPIService SeriesWebAPIService { get; set; } = default!;
    [Inject] public IPhotosWebAPIService PhotosWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public long? Id { get; set; }
    [Parameter] public string? Type { get; set; }
    
    [CascadingParameter] public MainLayout Layout { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded = false;
    private string? _error;
    
    private User? _user;

    private MediaResponse? _media;
    private MovieRequest? _movieRequest;
    private SeriesRequest? _seriesRequest;
    private Media? _mediaRequest => _movieRequest is not null ? _movieRequest : _seriesRequest;
    private bool _basicDataSaving;
    private string? _basicDataError;
    
    private MediaPosterResponse? _mediaPosterSaved;
    private MediaPosterRequest? _mediaPosterRequest;
    private bool _mediaPosterChanged;
    private bool _mediaPosterSaving;
    private bool _mediaPosterDeleting;
    
    private IEnumerable<PhotoResponse> _photos = new List<PhotoResponse>();
    private List<Guid> _photoDeleting = new List<Guid>();
    private bool _photoEditMode;
    private string? _photoEditError;
    private Guid? _photoEditId;
    private bool _photoEditSaving;
    private bool _photoEditIsBackground;
    private MediaPhotoRequest? _photoEditRequest;
    private PhotoBackgroundDataRequest? _photoEditBackgroundData = new PhotoBackgroundDataRequest()
    {
        FirstGradientColor = [0xFF, 0xFF, 0xFF],
        SecondGradientColor = [0x00, 0x00, 0x00],
        IsUniversalBackground = false
    };

    #endregion
    
    
    
    #region PRIVATE METHODS
    
    #region Main
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Layout.BackgroundPhoto = null;
            
            List<Task> step1Tasks = new List<Task>();
            List<Task> step2Tasks = new List<Task>();
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            step1Tasks.AddRange(
            [
                Task.Run(async () => _user = await AuthenticationService.GetUserAsync())
            ]);
            
            // STEP 1
            await Task.WhenAll(step1Tasks);
            if (_user is not null && _user.IsAdmin)
            {
                step2Tasks.AddRange(
                [
                    InitializeMedia()
                ]);
            }
            
            // STEP 2
            await Task.WhenAll(step2Tasks);
            if (_user is not null && _user.IsAdmin && _media is not null)
            {
                endTasks.AddRange(
                [
                    MediaWebAPIService.GetMediaPhotoRandomBackground(Id.Value, data => Layout.BackgroundPhoto = data),
                    MediaWebAPIService.GetMediaPoster(Id.Value, data =>
                    {
                        _mediaPosterSaved = data;
                        _mediaPosterRequest = new MediaPosterRequest(data);
                    }),
                    MediaWebAPIService.GetMediaPhotos(Id.Value, successAction: data => _photos = data)
                ]);
            }

            await Task.WhenAll(endTasks);
            
            _loaded = true;
            StateHasChanged();
        }
    }

    private async Task InitializeMedia()
    {
        if (Id.HasValue)
        {
            await MediaWebAPIService.GetMedia(Id.Value, data => _media = data, () => NavigationManager.NavigateTo("/media/new/movie"));
            if (_media.Type == MediaType.Movie)
            {
                await MoviesWebAPIService.GetMovie(Id.Value, data => _movieRequest = new MovieRequest(data));
            }
            else
            {
                await SeriesWebAPIService.GetSeries(Id.Value, data => _seriesRequest = new SeriesRequest(data));
            }
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(Type) && Type == "series")
            {
                _seriesRequest = new SeriesRequest
                {
                    Title = string.Empty
                };
            }
            else
            {
                _movieRequest = new MovieRequest
                {
                    Title = string.Empty
                };
            }
        }        
    }
    
    #endregion

    #region Poster
    
    private async Task LoadPoster(InputFileChangeEventArgs args)
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

            _mediaPosterRequest = new MediaPosterRequest()
            {
                Image = array,
                MimeType = args.File.ContentType
            };
            _mediaPosterChanged = true;
        }
    }

    private async Task SavePoster()
    {
        void Success(MediaPosterResponse data)
        {
            _mediaPosterSaved = data;
            _mediaPosterRequest = new MediaPosterRequest(data);
            _mediaPosterChanged = false;
            _mediaPosterSaving = false;
        }
        
        _mediaPosterSaving = true;
        await MediaWebAPIService.PutMediaPoster(Id.Value, _mediaPosterRequest, Success);
    }

    private void CancelPoster()
    {
        _mediaPosterRequest = _mediaPosterSaved is not null ? new MediaPosterRequest(_mediaPosterSaved) : null;
        _mediaPosterChanged = false;
    }

    private async Task DeletePoster()
    {
        void Success()
        {
            _mediaPosterSaved = null;
            _mediaPosterRequest = null;
            _mediaPosterChanged = false;
            _mediaPosterDeleting = false;
        }
        
        _mediaPosterDeleting = true;
        await MediaWebAPIService.DeleteMediaPoster(Id.Value, Success);
    }
    
    #endregion

    #region Basic data

    private async Task SaveBasicData()
    {
        void SuccessPost(long id)
        {
            _basicDataSaving = false;
            NavigationManager.NavigateTo($"/media/{id}/edit", true);
        }

        void BadRequest(IDictionary<string, string[]> errors)
        {
            _basicDataError = errors.SelectMany(x => x.Value).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(_basicDataError))
            {
                _basicDataSaving = false;
            }
        }
        
        _basicDataSaving = true;
        if (_media is null)
        {
            if (_movieRequest is not null)
            {
                await MoviesWebAPIService.PostMovie(_movieRequest, data => SuccessPost(data.Id), BadRequest);
            }
            else
            {
                await SeriesWebAPIService.PostSeries(_seriesRequest, data => SuccessPost(data.Id), BadRequest);
            }
        }
        else
        {
            if (_movieRequest is not null)
            {
                await MoviesWebAPIService.PutMovie(Id.Value, _movieRequest, () => _basicDataSaving = false, BadRequest);
            }
            else
            {
                await SeriesWebAPIService.PutSeries(Id.Value, _seriesRequest, () => _basicDataSaving = false, BadRequest);
            }
        }
    }

    #endregion

    #region Photos

    private async Task DeletePhoto(Guid id)
    {
        async Task Success()
        {
            NavigationManager.Refresh(true);
        }
        
        _photoDeleting.Add(id);
        await PhotosWebAPIService.DeletePhoto(id, async () => await Success());
    }

    private void InitEditPhoto(Guid? id)
    {
        _photoEditMode = true;
        _photoEditId = id;
        _photoEditRequest = null;
        _photoEditIsBackground = false;
        _photoEditBackgroundData = new PhotoBackgroundDataRequest
        {
            FirstGradientColor = [0xFF, 0xFF, 0xFF],
            SecondGradientColor = [0x00, 0x00, 0x00],
            IsUniversalBackground = false
        };
        if (id is not null)
        {
            PhotoResponse response = _photos.First(x => x.Id == id);
            _photoEditRequest = new MediaPhotoRequest(response);
            if (response.Background is not null)
            {
                _photoEditIsBackground = true;
                _photoEditBackgroundData = new PhotoBackgroundDataRequest(response.Background);
            }
        }
    }

    private void CancelEditPhoto()
    {
        _photoEditMode = false;
        _photoEditId = null;
        _photoEditError = null;
    }

    private async Task SaveEditPhoto()
    {
        void Success()
        {
            NavigationManager.Refresh(true);
        }

        void BadRequest(IDictionary<string, string[]> errors)
        {
            _photoEditError = errors.SelectMany(x => x.Value).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(_basicDataError))
            {
                _photoEditSaving = false;
            }
        }
        
        _photoEditSaving = true;
        if (_photoEditId is null)
        {
            _photoEditRequest.Background = _photoEditIsBackground ? _photoEditBackgroundData : null;
            await MediaWebAPIService.PostMediaPhoto(Id.Value, _photoEditRequest, Success, BadRequest);
        }
        else
        {
            if (_photoEditIsBackground)
            {
                await PhotosWebAPIService.PutPhotoBackgroundData(_photoEditId.Value, _photoEditBackgroundData, Success, BadRequest);
            }
            else
            {
                await PhotosWebAPIService.DeletePhotoBackgroundData(_photoEditId.Value, Success);
            }
        }
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
            
            _photoEditRequest = new MediaPhotoRequest
            {
                Image = array,
                MimeType = args.File.ContentType
            };
        }
    }

    private void EditPhotoFirstGradientColorChanged(ChangeEventArgs e)
    {
        _photoEditBackgroundData.FirstGradientColor = Convert.FromHexString(e.Value.ToString().Replace("#", string.Empty));
    }

    private void EditPhotoSecondGradientColorChanged(ChangeEventArgs e)
    {
        _photoEditBackgroundData.SecondGradientColor = Convert.FromHexString(e.Value.ToString().Replace("#", string.Empty));
    }

    #endregion
    
    #endregion
}
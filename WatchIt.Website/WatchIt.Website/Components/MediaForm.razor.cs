using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using WatchIt.Common.Model.Media;
using WatchIt.Website.Services.WebAPI.Media;

namespace WatchIt.Website.Components;

public partial class MediaForm : ComponentBase
{
    #region SERVICES

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IMediaWebAPIService MediaWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PROPERTIES
    
    [Parameter] public Media Data { get; set; }
    [Parameter] public long? Id { get; set; }
    [Parameter] public Func<Task> SaveDataAction { get; set; }
    [Parameter] public IEnumerable<string>? SaveDataErrors { get; set; }
    [Parameter] public string? SaveDataInfo { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private string? _actualPosterBase64 = null;
    private string? _actualPosterMediaType = null;
    private bool _posterChanged = false;
    private string? _posterBase64 = null;
    private string? _posterMediaType = null;
    
    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await MediaWebAPIService.GetPoster(Id.Value, (data) =>
            {
                _actualPosterBase64 = Encoding.UTF8.GetString(data.Image);
                _actualPosterMediaType = data.MimeType;
                _posterBase64 = _actualPosterBase64;
                _posterMediaType = _actualPosterMediaType;
            });
            StateHasChanged();
        }
    }

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

            _posterMediaType = args.File.ContentType;
            _posterBase64 = Convert.ToBase64String(array);
            _posterChanged = true;
        }
    }
    
    private async Task SavePoster()
    {
        void SuccessAction()
        {
            _actualPosterBase64 = _posterBase64;
            _actualPosterMediaType = _posterMediaType;
            _posterChanged = false;
        }

        MediaPosterRequest data = new MediaPosterRequest
        {
            Image = Encoding.UTF8.GetBytes(_posterBase64),
            MimeType = _posterMediaType
        };

        await MediaWebAPIService.PutPoster(Id.Value, data, SuccessAction);
    }

    private async Task DeletePoster()
    {
        void SuccessAction()
        {
            _actualPosterBase64 = null;
            _actualPosterMediaType = null;
            _posterChanged = false;
            _posterBase64 = null;
            _posterMediaType = null;
        }

        await MediaWebAPIService.DeletePoster(Id.Value, SuccessAction);
    }
    
    private void CancelPoster()
    {
        _posterBase64 = _actualPosterBase64;
        _posterMediaType = _actualPosterMediaType;
        _posterChanged = false;
    }

    #endregion
}
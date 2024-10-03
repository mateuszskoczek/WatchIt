using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using WatchIt.Common.Model;

namespace WatchIt.Website.Components;

public partial class PictureEditorComponent : ComponentBase 
{
    #region PARAMETERS
    
    [Parameter] public long? Id { get; set; }
    [Parameter] public int ContentWidth { get; set; } = 300;
    [Parameter] public string PicturePlaceholder { get; set; } = "assets/poster.png";
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public required Func<long, Action<Picture>, Task> PictureGetTask { get; set; }
    [Parameter] public required Func<long, Picture, Action<Picture>, Task> PicturePutTask { get; set; }
    [Parameter] public required Func<long, Action, Task> PictureDeleteTask { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private Picture? _pictureSaved;
    private Picture? _pictureSelected;
    private bool _pictureChanged;
    private bool _pictureSaving;
    private bool _pictureDeleting;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> endTask = new List<Task>();
            
            // STEP 0
            if (Id.HasValue)
            {
                endTask.AddRange(
                [
                    PictureGetTask(Id.Value, data =>
                    {
                        _pictureSaved = data;
                        _pictureSelected = data;
                    })
                ]);
            }
            
            // END
            await Task.WhenAll(endTask);
            
            _loaded = true;
            StateHasChanged();
        }
    }

    private async Task Load(InputFileChangeEventArgs args)
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

            _pictureSelected = new Picture
            {
                Image = array,
                MimeType = args.File.ContentType
            };
            _pictureChanged = true;
        }
    }

    private async Task Save()
    {
        void Success(Picture data)
        {
            _pictureSaved = data;
            _pictureSelected = data;
            _pictureChanged = false;
            _pictureSaving = false;
        }
        
        _pictureSaving = true;
        await PicturePutTask(Id.Value, _pictureSelected, Success);
    }

    private void Cancel()
    {
        _pictureSelected = _pictureSaved;
        _pictureChanged = false;
    }

    private async Task Delete()
    {
        void Success()
        {
            _pictureSaved = null;
            _pictureSelected = null;
            _pictureChanged = false;
            _pictureDeleting = false;
        }
        
        _pictureDeleting = true;
        await PictureDeleteTask(Id.Value, Success);
    }
    
    #endregion
}
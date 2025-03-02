using System.Reflection.Metadata;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using WatchIt.DTO.Models.Generics.Image;

namespace WatchIt.Website.Components.Panels.Common;

public partial class ImageEditorPanel : Component
{
    #region PARAMETERS
    
    [Parameter] public int ContentWidth { get; set; } = 300;
    [Parameter] public required string ImagePlaceholder { get; set; }
    [Parameter] public bool Circle { get; set; }
    [Parameter] public bool Disabled { get; set; }
    
    [Parameter] public string Class { get; set; } = string.Empty;

    [Parameter]
    public required ImageBase? Image
    {
        get => _imageSaved;
        set
        {
            _imageSet = true;
            _imageSaved = value;
            _imageSelected = ImageToRequest(value);
        }
    }

    [Parameter] public required Func<Task<ImageResponse?>> ImageGetMethod { get; set; }
    [Parameter] public required Func<ImageRequest, Task<ImageResponse?>> ImagePutMethod { get; set; }
    [Parameter] public required Func<Task<bool>> ImageDeleteMethod { get; set; }
    
    [Parameter] public Action<ImageResponse?>? OnImageChanged { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private ImageBase? _imageSaved;
    private bool _imageSet;
    private ImageRequest? _imageSelected;
    private bool _imageChanged;
    private bool _imageSaving;
    private bool _imageDeleting;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();

        if (!_imageSet)
        {
            _imageSaved = await ImageGetMethod();
            _imageSelected = ImageToRequest(_imageSaved);
        }
        
        _loaded = true;
        StateHasChanged();
    }

    private async Task LoadImageFromFile(FileChangedEventArgs args)
    {
        IFileEntry file = args.Files.First();
        Stream stream = file.OpenReadStream(5242880);
        byte[] array;
        using (MemoryStream ms = new MemoryStream())
        {
            await stream.CopyToAsync(ms);
            array = ms.ToArray();
        }
        _imageSelected = new ImageRequest
        {
            Image = array,
            MimeType = file.Type,
        };
        _imageChanged = true;
    }

    private async Task SaveImage()
    {
        _imageSaving = true;
        if (_imageSelected is not null)
        {
            ImageResponse? response = await ImagePutMethod(_imageSelected);
            if (response is not null)
            {
                _imageSaved = response;
                _imageSelected = ImageToRequest(_imageSaved);
                _imageChanged = false;
                OnImageChanged?.Invoke(response);
            }
        }
        _imageSaving = false;
    }

    private void CancelImage()
    {
        _imageSelected = ImageToRequest(_imageSaved);
        _imageChanged = false;
    }

    private async Task DeleteImage()
    {
        _imageDeleting = true;
        if (_imageSaved is not null)
        {
            bool response = await ImageDeleteMethod();
            if (response)
            {
                _imageSaved = null;
                _imageSelected = null;
                _imageChanged = false;
                OnImageChanged?.Invoke(null);
            }
        }
        _imageDeleting = false;
    }

    public static ImageRequest? ImageToRequest(ImageBase? image) => image is null ? null : new ImageRequest
    {
        Image = image.Image,
        MimeType = image.MimeType,
    };
    
    #endregion
}
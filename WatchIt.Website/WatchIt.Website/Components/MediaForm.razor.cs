using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using WatchIt.Common.Model.Media;

namespace WatchIt.Website.Components;

public partial class MediaForm : ComponentBase
{
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
        throw new NotImplementedException();
    }

    #endregion
}
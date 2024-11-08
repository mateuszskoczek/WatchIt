using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Media;
using WatchIt.Website.Services.Client.Media;

namespace WatchIt.Website.Components.Pages.MediaEditPage.Panels;

public partial class MediaEditPageHeaderPanelComponent : ComponentBase
{
    #region SERVICES

    [Inject] public IMediaClientService MediaClientService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required MediaResponse? MediaData { get; set; }
    [Parameter] public required string MediaType { get; set; }
    
    #endregion



    #region FIELDS

    private MediaPosterResponse? _poster;
    private List<KeyValuePair<string, object>> _attr = new List<KeyValuePair<string, object>>();

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (MediaData is not null)
            {
                await MediaClientService.GetMediaPoster(MediaData.Id, data => _poster = data);
                _attr.Add(new KeyValuePair<string, object>("@onclick", () => NavigationManager.NavigateTo($"/media/{MediaData.Id}")));
                StateHasChanged();
            }
        }
    }

    #endregion
}
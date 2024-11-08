using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Genres;
using WatchIt.Website.Layout;
using WatchIt.Website.Services.Client.Genres;
using WatchIt.Website.Services.Client.Media;

namespace WatchIt.Website.Pages;

public partial class GenrePage : ComponentBase
{
    #region SERVICES
    
    [Inject] private IGenresClientService GenresClientService { get; set; } = default!;
    [Inject] private IMediaClientService MediaClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS

    [Parameter] public int Id { get; set; }
    
    [CascadingParameter] public MainLayout Layout { get; set; }
    
    #endregion



    #region FIELDS

    private bool _loaded;
    private GenreResponse? _data;

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Layout.BackgroundPhoto = null;
            
            await GenresClientService.GetGenre(Id, data => _data = data);
            
            _loaded = true;
            StateHasChanged();
        }
    }

    #endregion
}
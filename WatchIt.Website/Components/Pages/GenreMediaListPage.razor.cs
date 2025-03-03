using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Pages;

public partial class GenreMediaListPage : Page
{
    #region SERVICES
    
    [Inject] private IGenresClient GenresClient { get; set; } = null!;
    [Inject] private IMediaClient MediaClient { get; set; } = null!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS

    [Parameter] public int Id { get; set; }
    
    #endregion



    #region FIELDS

    private bool _loaded;
    private GenreResponse? _data;

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();

        IApiResponse<GenreResponse> response = await GenresClient.GetGenre((short)Id);
        if (response.IsSuccessful)
        {
            _data = response.Content;
        }
        
        _loaded = true;
        StateHasChanged();
    }

    #endregion
}
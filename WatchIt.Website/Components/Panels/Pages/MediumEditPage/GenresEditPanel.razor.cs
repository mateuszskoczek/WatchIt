using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Panels.Pages.MediumEditPage;

public partial class GenresEditPanel : Component
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IGenresClient GenresClient { get; set; } = null!;
    [Inject] private IMediaClient MediaClient { get; set; } = null!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required BaseMediumResponse? Data { get; set; }

    #endregion
    
    
    
    #region FIELDS
    
    private Dictionary<GenreResponse, bool> _chosenGenres = new Dictionary<GenreResponse, bool>();
    private List<GenreResponse> _availableGenres = new List<GenreResponse>();
    
    private short? _selectedGenre;
    private bool _addLoading;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await LoadData();
        
        StateHasChanged();
    }

    private async Task LoadData()
    {
        if (Data is not null)
        {
            foreach (GenreResponse genre in Data.Genres)
            {
                _chosenGenres[genre] = false;
            }
        }

        IApiResponse<IEnumerable<GenreResponse>> response = await GenresClient.GetGenres();
        if (response.IsSuccessful)
        {
            IEnumerable<short> tempSelected = _chosenGenres.Keys.Select(x => x.Id);
            _availableGenres.AddRange(response.Content.Where(x => !tempSelected.Contains(x.Id)));
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error occured. Genres could not be obtained.", SnackbarColor.Danger);
        }
    }

    private async Task AddGenre()
    {
        _addLoading = true;
        
        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;
        
        IApiResponse response = await MediaClient.PostMediumGenre(token, Data.Id, _selectedGenre!.Value);
        
        if (response.IsSuccessful)
        {
            GenreResponse selectedGenre = _availableGenres.First(x => x.Id == _selectedGenre);
            _availableGenres.Remove(selectedGenre);
            _chosenGenres[selectedGenre] = false;
            _selectedGenre = null;
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error occured. Genre could not be added.", SnackbarColor.Danger);
        }
        _addLoading = false;
    }

    private async Task RemoveGenre(GenreResponse genre)
    {
        _chosenGenres[genre] = true;
        
        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;
        
        IApiResponse response = await MediaClient.DeleteMediumGenre(token, Data.Id, genre.Id);
        
        if (response.IsSuccessful)
        {
            _chosenGenres.Remove(genre);
            _availableGenres.Add(genre);
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error occured. Genre could not be removed.", SnackbarColor.Danger);
        }
    }

    #endregion
}
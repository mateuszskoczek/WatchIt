using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;
using WatchIt.Website.Services.Client.Genres;
using WatchIt.Website.Services.Client.Media;

namespace WatchIt.Website.Components.Pages.MediaEditPage.Panels;

public partial class MediaGenresEditPanelComponent : ComponentBase
{
    #region SERVICES

    [Inject] private IGenresClientService GenresClientService { get; set; } = default!;
    [Inject] private IMediaClientService MediaClientService { get; set; } = default!;

    #endregion
    
    
    
    #region PARAMETERS

    [Parameter] public required MediaResponse Data { get; set; }

    #endregion
    
    
    
    #region FIELDS
    
    private Dictionary<GenreResponse, bool> _chosenGenres = new Dictionary<GenreResponse, bool>();
    private List<GenreResponse> _availableGenres = new List<GenreResponse>();
    
    private short? _selectedGenre;
    private bool _addLoading;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            foreach (GenreResponse genre in Data.Genres)
            {
                _chosenGenres[genre] = false;
            }
            await GenresClientService.GetGenres(successAction: data =>
            {
                IEnumerable<short> tempSelected = _chosenGenres.Keys.Select(x => x.Id);
                _availableGenres.AddRange(data.Where(x => !tempSelected.Contains(x.Id)));
            });
            StateHasChanged();
        }
    }

    private async Task AddGenre()
    {
        _addLoading = true;
        await MediaClientService.PostMediaGenre(Data.Id, _selectedGenre!.Value, () =>
        {
            GenreResponse selectedGenre = _availableGenres.First(x => x.Id == _selectedGenre);
            _availableGenres.Remove(selectedGenre);
            _chosenGenres[selectedGenre] = false;
            _addLoading = false;
            _selectedGenre = null;
        });
    }

    private async Task RemoveGenre(GenreResponse genre)
    {
        _chosenGenres[genre] = true;
        await MediaClientService.DeleteMediaGenre(Data.Id, genre.Id, () =>
        {
            _chosenGenres.Remove(genre);
            _availableGenres.Add(genre);
        });
    }

    #endregion
}
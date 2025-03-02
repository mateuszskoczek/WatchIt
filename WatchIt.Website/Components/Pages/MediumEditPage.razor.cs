using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Pages;

public partial class MediumEditPage : Page
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IPeopleClient PeopleClient { get; set; } = null!;
    [Inject] private IMediaClient MediaClient { get; set; } = null!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public long? Id { get; set; }
    [Parameter] public string? Type { get; set; }
    
    #endregion



    #region FIELDS

    private bool _loaded;
    
    private BaseMediumResponse? _data;
    private List<PersonResponse>? _people;

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();
        
        await Task.WhenAll(
        [
            LoadData(),
            LoadPeople(),
            LoadBackground(),
        ]);
        
        _loaded = true;
        StateHasChanged();
    }

    private async Task LoadData()
    {
        if (Id.HasValue)
        {
            IApiResponse<MediumResponse> response = await MediaClient.GetMedium(Id.Value);
            if (response.IsSuccessful)
            {
                switch (response.Content.Type)
                {
                    case MediumResponseType.Movie:
                        IApiResponse<MediumMovieResponse> responseMovie = await MediaClient.GetMediumMovie(Id.Value, true);
                        _data = responseMovie.Content;
                        break;
                    case MediumResponseType.Series:
                        IApiResponse<MediumSeriesResponse> responseSeries = await MediaClient.GetMediumSeries(Id.Value, true);
                        _data = responseSeries.Content;
                        break;
                }
            }
            else
            {
                await Base.SnackbarStack.PushAsync("An error occured. Medium data could not be obtained.", SnackbarColor.Danger);
            }
        }
    }

    private async Task LoadPeople()
    {
        IApiResponse<IEnumerable<PersonResponse>> response = await PeopleClient.GetPeople();
        if (response.IsSuccessful)
        {
            _people = response.Content.ToList();
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error has occured during loading people.", SnackbarColor.Danger);
        }
    }

    private async Task LoadBackground()
    {
        if (Id.HasValue)
        {
            IApiResponse<PhotoResponse?> response = await MediaClient.GetMediumBackgroundPhoto(Id.Value);
            if (response.IsSuccessful && response.Content is not null)
            {
                Base.CustomBackground = response.Content;
            }
        }
    }

    #endregion
}
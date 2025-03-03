using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Pages;

public partial class PersonEditPage : Page
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IPeopleClient PeopleClient { get; set; } = null!;
    [Inject] private IMediaClient MediaClient { get; set; } = null!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public long? Id { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;

    private PersonResponse? _data;
    private List<MediumResponse>? _media;

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();
        
        await Task.WhenAll(
        [
            LoadData(),
            LoadMedia(),
        ]);
        
        _loaded = true;
        StateHasChanged();
    }

    private async Task<bool> LoadData()
    {
        if (!Id.HasValue)
        {
            return true;
        }
        
        IApiResponse<PersonResponse> response = await PeopleClient.GetPerson(Id.Value, true);
        if (response.IsSuccessful)
        {
            _data = response.Content;
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error has occured during loading person data.", SnackbarColor.Danger);
        }
        
        return response.IsSuccessful;
    }

    private async Task LoadMedia()
    {
        IApiResponse<IEnumerable<MediumResponse>> response = await MediaClient.GetMedia();
        if (response.IsSuccessful)
        {
            _media = response.Content.ToList();
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error has occured during loading media.", SnackbarColor.Danger);
        }
    }

    #endregion
}
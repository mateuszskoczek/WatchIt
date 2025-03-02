using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Components.Panels.Pages.PersonPage;

namespace WatchIt.Website.Components.Pages;

public partial class PersonPage : Page
{
    #region SERVICES

    [Inject] private IPeopleClient PeopleClient { get; set; } = null!;
    [Inject] private IMediaClient MediaClient { get; set; } = null!;
    [Inject] private IRolesClient RolesClient { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public long Id { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private RatingPanel _ratingPanel = null!;

    private bool _loaded;
    
    private PersonResponse? _data;
    private IEnumerable<MediumResponse>? _media;
    
    #endregion



    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();

        await Task.WhenAll(
        [
            LoadData(),
            LoadMedia(),
            IncrementViewCounter(),
        ]);
        
        _loaded = true;
        StateHasChanged();
    }

    private async Task LoadData()
    {
        IApiResponse<PersonResponse> response = await PeopleClient.GetPerson(Id, true);
        if (response.IsSuccessful)
        {
            _data = response.Content;
        }
    }

    private async Task LoadMedia()
    {
        IApiResponse<IEnumerable<MediumResponse>> response = await MediaClient.GetMedia(includePictures: true);
        if (response.IsSuccessful)
        {
            _media = response.Content;
        }
    }

    private async Task IncrementViewCounter()
    {
        await PeopleClient.PutPeopleViewCount(Id);
    }

    #endregion
}
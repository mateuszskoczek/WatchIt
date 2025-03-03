using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;

namespace WatchIt.Website.Components.Pages;

public partial class MediumPage : Page
{
    #region SERVICES

    [Inject] private IPeopleClient PeopleClient { get; set; } = null!;
    [Inject] private IMediaClient MediaClient { get; set; } = null!;
    [Inject] private IPhotosClient PhotosClient { get; set; } = null!;
    [Inject] private IRolesClient RolesClient { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public long Id { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    //private RatingPanel _ratingPanel = null!;

    private bool _loaded;
    
    private MediumResponse? _data;
    private IEnumerable<PersonResponse>? _people;
    
    #endregion



    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();

        await Task.WhenAll(
        [
            LoadData(),
            LoadPeople(),
            IncrementViewCounter(),
            LoadBackground(),
        ]);
        
        _loaded = true;
        StateHasChanged();
    }

    private async Task LoadData()
    {
        IApiResponse<MediumResponse> response = await MediaClient.GetMedium(Id, true);
        if (response.IsSuccessful)
        {
            _data = response.Content;
        }
    }

    private async Task LoadPeople()
    {
        IApiResponse<IEnumerable<PersonResponse>> response = await PeopleClient.GetPeople(includePictures: true);
        if (response.IsSuccessful)
        {
            _people = response.Content;
        }
    }

    private async Task IncrementViewCounter()
    {
        await MediaClient.PutMediumViewCount(Id);
    }

    private async Task LoadBackground()
    {
        IApiResponse<PhotoResponse?> response = await MediaClient.GetMediumBackgroundPhoto(Id);
        if (response.IsSuccessful)
        {
            Base.CustomBackground = response.Content;
        }
    }

    #endregion
}
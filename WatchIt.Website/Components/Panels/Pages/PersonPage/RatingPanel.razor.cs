using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;

namespace WatchIt.Website.Components.Panels.Pages.PersonPage;

public partial class RatingPanel : Component
{
    #region SERVICES

    [Inject] protected IPeopleClient PeopleClient { get; set; } = null!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required PersonResponse Data { get; set; }

    #endregion
    
    
    
    #region FIELDS
    
    private RatingOverallResponse _globalRating;
    private RatingUserOverallResponse? _userRating;
    
    #endregion



    #region PUBLIC METHODS

    public async Task Update()
    {
        await Task.WhenAll(
        [
            GetGlobalRating(),
            GetUserRating()
        ]);
        StateHasChanged();
    }

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();

        _globalRating = Data.Rating;
        await GetUserRating();
        
        StateHasChanged();
    }

    private async Task GetUserRating()
    {
        if (Base.AuthorizationLoaded && Base.AuthorizedAccount is not null)
        {
            IApiResponse<RatingUserOverallResponse> response = await PeopleClient.GetPersonUserRating(Data.Id, Base.AuthorizedAccount.Id);
            if (response.IsSuccessful)
            {
                _userRating = response.Content;
            }
        }
    }

    private async Task GetGlobalRating()
    {
        IApiResponse<RatingOverallResponse> response = await PeopleClient.GetPersonRating(Data.Id);
        if (response.IsSuccessful)
        {
            _globalRating = response.Content;
        }
    }

    #endregion
}
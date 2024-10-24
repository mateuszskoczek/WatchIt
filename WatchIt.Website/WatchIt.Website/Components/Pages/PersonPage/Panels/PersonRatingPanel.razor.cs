using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Rating;
using WatchIt.Website.Services.Utility.Authentication;
using WatchIt.Website.Services.WebAPI.Persons;

namespace WatchIt.Website.Components.Pages.PersonPage.Panels;

public partial class PersonRatingPanel : ComponentBase
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] private IPersonsWebAPIService PersonsWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required long Id { get; set; }
    [Parameter] public RatingResponse? Rating { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private User? _user;
    
    private RatingResponse? _userRating;
    
    #endregion



    #region PUBLIC METHODS

    public async Task UpdateRating()
    {
        await Task.WhenAll(UpdateGlobalRating(), UpdateUserRating());
        StateHasChanged();
    }

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> step1Tasks = new List<Task>(1);
            List<Task> endTasks = new List<Task>(2);
            
            // STEP 0
            step1Tasks.AddRange(
            [
                Task.Run(async () => _user = await AuthenticationService.GetUserAsync())
            ]);
            if (Rating is null)
            {
                endTasks.AddRange(
                [
                    UpdateGlobalRating()
                ]);
            }
            
            // STEP 1
            await Task.WhenAll(step1Tasks);
            endTasks.AddRange(
            [
                UpdateUserRating()
            ]);
            
            // END
            await Task.WhenAll(endTasks);

            StateHasChanged();
        }
    }

    protected async Task UpdateGlobalRating() => await PersonsWebAPIService.GetPersonGlobalRating(Id, data => Rating = data);

    protected async Task UpdateUserRating()
    {
        if (_user is not null)
        {
            await PersonsWebAPIService.GetPersonUserRating(Id, _user.Id, data => _userRating = data);
        }
    }

    #endregion
}
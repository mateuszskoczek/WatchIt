using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;
using WatchIt.Common.Model.Persons;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Model.Roles;
using WatchIt.Website.Services.Utility.Authentication;
using WatchIt.Website.Services.WebAPI.Persons;

namespace WatchIt.Website.Components.Pages.MediaPage.Subcomponents;

public partial class RoleComponent<TRole> : ComponentBase where TRole : IRoleResponse
{
    #region SERVICES

    [Inject] private IPersonsWebAPIService PersonsWebAPIService { get; set; } = default!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required TRole Role { get; set; }
    [Parameter] public required Func<Guid, Action<RatingResponse>, Task> GetGlobalRatingAction { get; set; }
    [Parameter] public required Func<Guid, Action<short>, Action, Task> GetUserRatingAction { get; set; }
    [Parameter] public required Func<Guid, RatingRequest, Task> PutRatingAction { get; set; }
    [Parameter] public required Func<Guid, Task> DeleteRatingAction { get; set; }
    
    #endregion



    #region FIELDS

    private bool _authenticated;
    private PersonResponse? _person;
    private Picture? _picture;
    private RatingResponse? _globalRating;
    
    private int _yourRating;

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            endTasks.AddRange(
            [
                Task.Run(async () => _authenticated = await AuthenticationService.GetAuthenticationStatusAsync()),
                PersonsWebAPIService.GetPersonPhoto(Role.PersonId, data => _picture = data),
                PersonsWebAPIService.GetPerson(Role.PersonId, data => _person = data),
                GetGlobalRatingAction(Role.Id, data => _globalRating = data),
                GetUserRatingAction(Role.Id, data => _yourRating = data, () => _yourRating = 0)
            ]);
            
            // END
            await Task.WhenAll(endTasks);
            
            StateHasChanged();
        }
    }

    private async Task RatingChanged()
    {
        if (_yourRating == 0)
        {
            await DeleteRatingAction(Role.Id);
        }
        else
        {
            await PutRatingAction(Role.Id, new RatingRequest((short)_yourRating));
        }
        
        await GetGlobalRatingAction(Role.Id, data => _globalRating = data);
    }

    #endregion
}
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.Database.Model.People;
using WatchIt.DTO.Models.Controllers.Genders.Gender;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Controllers.People.Person.Query;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;

namespace WatchIt.Website.Components.List;

public partial class PeopleUserRatedFilter : Filter<PersonUserRatedResponse, Person, PersonFilterQuery>
{
    #region SERVICES

    [Inject] private IGendersClient GendersClient { get; set; } = default!;

    #endregion
    
    
    
    #region FIELDS

    private IEnumerable<GenderResponse> _genders = [];

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();
        
        IApiResponse<IEnumerable<GenderResponse>> response = await GendersClient.GetGenders();
        if (response.IsSuccessful)
        {
            _genders = response.Content;
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error has occured. List of genders could not be obtained.", SnackbarColor.Danger);
        }
        StateHasChanged();
    }

    #endregion
}
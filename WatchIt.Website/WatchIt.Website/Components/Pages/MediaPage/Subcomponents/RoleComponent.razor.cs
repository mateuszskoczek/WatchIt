using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;
using WatchIt.Common.Model.Persons;
using WatchIt.Common.Model.Roles;
using WatchIt.Website.Services.WebAPI.Persons;

namespace WatchIt.Website.Components.Pages.MediaPage.Subcomponents;

public partial class RoleComponent<TRole> : ComponentBase where TRole : IRoleResponse
{
    #region SERVICES

    [Inject] private IPersonsWebAPIService PersonsWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required TRole Role { get; set; }
    
    #endregion



    #region FIELDS

    private PersonResponse? _person;
    private Picture? _picture; 

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
                PersonsWebAPIService.GetPersonPhoto(Role.PersonId, data =>
                {
                    _picture = data;
                    StateHasChanged();
                }),
                PersonsWebAPIService.GetPerson(Role.PersonId, data =>
                {
                    _person = data;
                    StateHasChanged();
                })
            ]);
            
            // END
            await Task.WhenAll(endTasks);
        }
    }

    #endregion
}
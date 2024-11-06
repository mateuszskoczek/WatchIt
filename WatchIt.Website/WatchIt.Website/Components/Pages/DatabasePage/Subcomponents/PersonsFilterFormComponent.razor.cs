using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Genders;
using WatchIt.Common.Model.Persons;
using WatchIt.Website.Components.Common.ListComponent;
using WatchIt.Website.Services.Client.Genders;

namespace WatchIt.Website.Components.Pages.DatabasePage.Subcomponents;

public partial class PersonsFilterFormComponent : FilterFormComponent<PersonResponse, PersonQueryParameters>
{
    #region SERVICES

    [Inject] private IGendersClientService GendersClientService { get; set; } = default!;

    #endregion
    
    
    
    #region FIELDS

    private IEnumerable<GenderResponse> _genders = [];

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
                GendersClientService.GetAllGenders(successAction: data => _genders = data)
            ]);
            
            // END
            await Task.WhenAll(endTasks);
            
            StateHasChanged();
        }
    }

    #endregion
}
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Genders;
using WatchIt.Common.Model.Persons;
using WatchIt.Website.Services.WebAPI.Persons;

namespace WatchIt.Website.Components.PersonEditPage;

public partial class PersonEditFormComponent : ComponentBase
{
    #region SERVICES

    [Inject] private IPersonsWebAPIService PersonsWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public long? Id { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;

    private IEnumerable<GenderResponse> _genders = [];
    
    private PersonRequest _person = new PersonRequest();

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
                // TODO: Add gender fetch
            ]);
            if (Id.HasValue)
            {
                endTasks.AddRange(
                [
                    PersonsWebAPIService.GetPerson(Id.Value, data => _person = new PersonRequest(data))
                ]);
            }
            
            // END
            await Task.WhenAll(endTasks);
            
            _loaded = true;
            StateHasChanged();
        }
    }

    #endregion
}
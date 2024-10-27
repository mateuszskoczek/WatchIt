using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Genders;
using WatchIt.Common.Model.Persons;
using WatchIt.Website.Services.Client.Genders;
using WatchIt.Website.Services.Client.Persons;

namespace WatchIt.Website.Components.Pages.PersonEditPage.Panels;

public partial class PersonEditFormPanelComponent : ComponentBase
{
    #region SERVICES

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IPersonsClientService PersonsClientService { get; set; } = default!;
    [Inject] private IGendersClientService GendersClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public long? Id { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    private bool _saving;
    private string? _error;

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
                GendersClientService.GetAllGenders(successAction: data => _genders = data)
            ]);
            if (Id.HasValue)
            {
                endTasks.AddRange(
                [
                    PersonsClientService.GetPerson(Id.Value, data => _person = new PersonRequest(data))
                ]);
            }
            
            // END
            await Task.WhenAll(endTasks);
            
            _loaded = true;
            StateHasChanged();
        }
    }

    private async Task Save()
    {
        void PutSuccess()
        {
            _error = null;
            _saving = false;
        }

        void PostSuccess(PersonResponse data)
        {
            NavigationManager.NavigateTo($"person/{data.Id}/edit", true);
        }

        void BadRequest(IDictionary<string, string[]> errors)
        {
            _error = errors.SelectMany(x => x.Value).FirstOrDefault() ?? "Unknown error";
            _saving = false;
        }

        void AuthError()
        {
            _error = "Authentication error";
            _saving = false;
        }
        
        _saving = true;
        if (Id.HasValue)
        {
            await PersonsClientService.PutPerson(Id.Value, _person, PutSuccess, BadRequest, AuthError, AuthError);
        }
        else
        {
            await PersonsClientService.PostPerson(_person, PostSuccess, BadRequest, AuthError, AuthError);
        }
    }

    #endregion
}
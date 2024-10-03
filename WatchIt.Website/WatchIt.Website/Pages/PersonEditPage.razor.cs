using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Persons;
using WatchIt.Website.Layout;
using WatchIt.Website.Services.Utility.Authentication;
using WatchIt.Website.Services.WebAPI.Persons;

namespace WatchIt.Website.Pages;

public partial class PersonEditPage : ComponentBase
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] private IPersonsWebAPIService PersonsWebAPIService { get; set; } = default!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public long? Id { get; set; }
    
    [CascadingParameter] public MainLayout Layout { get; set; }
    
    #endregion



    #region FIELDS

    private bool _loaded;
    private string? _error;
    
    private User? _user;

    private PersonResponse? _person;

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> step1Tasks = new List<Task>();
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            step1Tasks.AddRange(
            [
                Task.Run(async () => _user = await AuthenticationService.GetUserAsync())
            ]);
            
            // STEP 1
            await Task.WhenAll(step1Tasks);
            if (_user?.IsAdmin == true && Id.HasValue)
            {
                endTasks.AddRange(
                [
                    PersonsWebAPIService.GetPerson(Id.Value, data => _person = data)
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
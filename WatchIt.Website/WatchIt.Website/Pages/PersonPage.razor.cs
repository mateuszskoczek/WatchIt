using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Persons;
using WatchIt.Website.Components.Pages.PersonPage.Panels;
using WatchIt.Website.Layout;
using WatchIt.Website.Services.WebAPI.Persons;

namespace WatchIt.Website.Pages;

public partial class PersonPage : ComponentBase
{
    #region SERVICES

    [Inject] private IPersonsWebAPIService PersonsWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public long Id { get; set; }

    [CascadingParameter] public MainLayout Layout { get; set; } = default!;
    
    #endregion



    #region FIELDS

    private bool _loaded;

    private PersonRatingPanel _ratingPanel = default!;
    
    private PersonResponse? _person;

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // INIT
            Layout.BackgroundPhoto = null;
            
            List<Task> step1Tasks = new List<Task>(1);
            List<Task> endTasks = new List<Task>(1);
            
            // STEP 0
            step1Tasks.AddRange(
            [
                PersonsWebAPIService.GetPerson(Id, data => _person = data)
            ]);
            
            // STEP 1
            await Task.WhenAll(step1Tasks);
            if (_person is not null)
            {
                endTasks.AddRange(
                [
                    PersonsWebAPIService.PostPersonView(Id),
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
using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Model.Roles;
using WatchIt.Common.Query;

namespace WatchIt.Website.Components.Pages.MediaPage.Subcomponents;

public partial class RoleListComponent<TRole, TQuery> : ComponentBase where TRole : IRoleResponse, IQueryOrderable<TRole> where TQuery : QueryParameters<TRole>
{
    #region SERVICES
    
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required long Id { get; set; }
    [Parameter] public TQuery Query { get; set; } = Activator.CreateInstance<TQuery>();
    [Parameter] public Func<TRole, string>? AdditionalTextSource { get; set; } 
    
    [Parameter] public required Func<long, TQuery, Action<IEnumerable<TRole>>, Task> GetRolesAction { get; set; }
    [Parameter] public required Func<Guid, Action<RatingResponse>, Task> GetGlobalRatingAction { get; set; }
    [Parameter] public required Func<Guid, long, Action<short>, Action, Task> GetUserRatingAction { get; set; }
    [Parameter] public required Func<Guid, RatingRequest, Task> PutRatingAction { get; set; }
    [Parameter] public required Func<Guid, Task> DeleteRatingAction { get; set; }

    #endregion
    
    
    
    #region FIELDS
    
    private readonly int _pageSize = 20;

    private bool _loaded;
    private bool _allItemsLoaded;
    
    private List<TRole> _roles = new List<TRole>();
    private bool _rolesFetching;

    #endregion
    
    
    
    #region PUBLIC METHODS

    public async Task Refresh()
    {
        _loaded = false;
        _roles.Clear();
        Query.After = null;
        Query.First = _pageSize;
        await GetRoles(true);
        _loaded = true;
    }
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> endTasks = new List<Task>();
            
            // INIT
            Query.First = _pageSize;
            
            // STEP 0
            endTasks.AddRange(
            [
                GetRoles(true)
            ]);
            
            // END
            await Task.WhenAll(endTasks);
            
            _loaded = true;
            StateHasChanged();
        }
    }

    private async Task GetRoles(bool firstFetch = false)
    {
        _rolesFetching = true;
        await GetRolesAction(Id, Query, data =>
        {
            _roles.AddRange(data);
            if (data.Count() < _pageSize)
            {
                _allItemsLoaded = true;
            }
            else
            {
                if (firstFetch)
                {
                    Query.After = 0;
                }
            }
            Query.After += data.Count();
            _rolesFetching = false;
        });
    }

    #endregion
}
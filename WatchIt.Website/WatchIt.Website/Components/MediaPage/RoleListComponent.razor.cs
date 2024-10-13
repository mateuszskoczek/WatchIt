using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Roles;
using WatchIt.Common.Query;

namespace WatchIt.Website.Components.MediaPage;

public partial class RoleListComponent<TRole, TQuery> : ComponentBase where TRole : IRoleResponse, IQueryOrderable<TRole> where TQuery : QueryParameters<TRole>
{
    #region PROPERTIES
    
    [Parameter] public required long Id { get; set; }
    [Parameter] public required Func<long, TQuery, Action<IEnumerable<TRole>>, Task> GetRolesAction { get; set; }
    [Parameter] public TQuery Query { get; set; } = Activator.CreateInstance<TQuery>();
    [Parameter] public Func<TRole, string>? AdditionalTextSource { get; set; } 

    #endregion
    
    
    
    #region FIELDS
    
    private readonly int _pageSize = 20;

    private bool _loaded;
    private bool _allItemsLoaded;
    
    private List<TRole> _roles = new List<TRole>();
    private bool _rolesFetching;

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
                Query.After += _pageSize;
            }
            _rolesFetching = false;
        });
    }

    #endregion
}
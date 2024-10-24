using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;
using WatchIt.Common.Model.Rating;

namespace WatchIt.Website.Components.Common.Subcomponents;

public partial class RoleListComponent<TRole, TQuery, TRoleParent> : ComponentBase where TRole : WatchIt.Common.Model.Roles.IRoleResponse, WatchIt.Common.Query.IQueryOrderable<TRole>
                                                                                   where TQuery : WatchIt.Common.Query.QueryParameters<TRole>
{
    #region PARAMETERS
    
    [Parameter] public required long Id { get; set; }
    [Parameter] public TQuery Query { get; set; } = Activator.CreateInstance<TQuery>();
    [Parameter] public required Func<long, TQuery, Action<IEnumerable<TRole>>, Task> GetRolesAction { get; set; }
    
    [Parameter] public required Func<TRole, TRoleParent, string> NameSource { get; set; }
    [Parameter] public Func<TRole, TRoleParent, string>? AdditionalInfoSource { get; set; }
    
    [Parameter] public required Func<long, Action<TRoleParent>, Task> GetRoleParentMethod { get; set; }
    [Parameter] public required Func<TRole, long> ParentItemIdSource { get; set; }
    [Parameter] public required string ParentUrlTemplate { get; set; }
    
    [Parameter] public required string PosterPlaceholder { get; set; }
    [Parameter] public required Func<long, Action<Picture>, Task> PosterDownloadingTask { get; set; }
    
    [Parameter] public required Func<Guid, Action<RatingResponse>, Task> GetGlobalRatingMethod { get; set; }
    [Parameter] public required Func<Guid, long, Action<short>, Action, Task> GetUserRatingMethod { get; set; }
    [Parameter] public required Func<Guid, RatingRequest, Task> PutRatingMethod { get; set; }
    [Parameter] public required Func<Guid, Task> DeleteRatingMethod { get; set; }
    
    [Parameter] public Action? OnRatingChanged { get; set; }
    
    #endregion
    
    
    
    #region FIELDS
    
    private readonly int _pageSize = 20;

    private bool _loaded;
    private bool _allItemsLoaded;
    
    private Dictionary<TRole, TRoleParent> _roles = new Dictionary<TRole, TRoleParent>();
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
            Query.OrderBy = "rating.average";
            
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
        await GetRolesAction(Id, Query, async data =>
        {
            List<TRole> newRolesArray = data.ToList();
            Dictionary<TRole, TRoleParent> newRoles = new Dictionary<TRole, TRoleParent>();
            await Parallel.ForEachAsync(data, new ParallelOptions { MaxDegreeOfParallelism = 4 }, async (item, _) => await GetRoleParentMethod(ParentItemIdSource(item), parent => newRoles[item] = parent));
            foreach (KeyValuePair<TRole, TRoleParent> kvp in newRoles.OrderBy(x => newRolesArray.IndexOf(x.Key)))
            {
                _roles[kvp.Key] = kvp.Value;
            }
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
            StateHasChanged();
        });
    }

    #endregion
}
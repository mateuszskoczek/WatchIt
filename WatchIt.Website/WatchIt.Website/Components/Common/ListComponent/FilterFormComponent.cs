using Microsoft.AspNetCore.Components;
using WatchIt.Common.Query;

namespace WatchIt.Website.Components.Common.ListComponent;

public abstract class FilterFormComponent<TItem, TQuery> : ComponentBase where TItem : IQueryOrderable<TItem> where TQuery : QueryParameters<TItem>
{
    #region PARAMETERS
    
    [CascadingParameter]
    protected ListComponent<TItem, TQuery> Parent { get; set; }

    #endregion
    
    
    
    #region FIELDS
    
    protected TQuery? Query => Parent?.Query;
    
    #endregion
}
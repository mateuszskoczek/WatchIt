using Microsoft.AspNetCore.Components;
using WatchIt.Common.Query;

namespace WatchIt.Website.Components.DatabasePage;

public abstract class FilterFormComponent<TItem, TQuery> : ComponentBase where TItem : IQueryOrderable<TItem> where TQuery : QueryParameters<TItem>
{
    #region PARAMETERS
    
    [CascadingParameter]
    protected DatabasePageComponent<TItem, TQuery> Parent { get; set; }

    #endregion
    
    
    
    #region FIELDS
    
    protected TQuery? Query => Parent?.Query;
    
    #endregion
}
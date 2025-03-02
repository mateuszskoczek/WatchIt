using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Query;

namespace WatchIt.Website.Components.List;

public class Filter<TItem, TEntity, TQuery> : Component where TEntity : class where TQuery : IFilterQuery<TEntity>
{
    #region PARAMETERS
    
    [CascadingParameter]
    public required List<TItem, TEntity, TQuery> Parent { get; set; }

    #endregion
    
    
    
    #region FIELDS
    
    protected TQuery? Query => Parent.Query;
    
    #endregion
}
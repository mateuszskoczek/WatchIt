using System.Net;
using Microsoft.AspNetCore.Components;

namespace WatchIt.Website.Components.Subcomponents.Common;

public partial class Searchbar : Component
{
    #region SERVICES

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public Action? OnCloseButtonClicked { get; set; }
    [Parameter] public Action? OnSearchButtonClicked { get; set; }
    
    #endregion



    #region FIELDS

    private string? _searchText;

    #endregion
    
    
    
    #region PRIVATE METHODS

    public void Search()
    {
        OnSearchButtonClicked?.Invoke();
        if (!string.IsNullOrWhiteSpace(_searchText))
        {
            string query = WebUtility.UrlEncode(_searchText);
            NavigationManager.NavigateTo($"/search/{query}", true);
        }
    }
    
    #endregion
}
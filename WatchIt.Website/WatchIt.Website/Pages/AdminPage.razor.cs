using Microsoft.AspNetCore.Components;
using WatchIt.Website.Services.Utility.Authentication;

namespace WatchIt.Website.Pages;

public partial class AdminPage
{
    #region SERVICE

    [Inject] public IAuthenticationService AuthenticationService { get; set; } = default!;

    #endregion



    #region FIELDS

    private bool _loaded = false;
    private bool _authenticated = false;

    #endregion
    
    
    
    #region PRIVATE METHODS
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            User? user = await AuthenticationService.GetUserAsync();
            if (user is not null && user.IsAdmin)
            {
                _authenticated = true;
            }
            _loaded = true;
            StateHasChanged();
        }
    }
    
    #endregion
}
using Microsoft.AspNetCore.Components;
using WatchIt.Website.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Pages;

public partial class AdminPage
{
    #region SERVICES

    [Inject] public IAuthenticationService AuthenticationService { get; set; } = default!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [CascadingParameter] public MainLayout Layout { get; set; }
    
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
            Layout.BackgroundPhoto = null;
            
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
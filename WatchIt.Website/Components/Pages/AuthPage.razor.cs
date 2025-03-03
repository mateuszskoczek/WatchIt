using System.Net;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Tokens;

namespace WatchIt.Website.Components.Pages;

public partial class AuthPage
{
    #region SERVICES

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    #endregion
    
    
    
    #region PARAMETERS

    [SupplyParameterFromQuery(Name = "redirect_to")]
    private string RedirectTo { get; set; } = "/";
    
    #endregion
    
    
    
    #region FIELDS

    private bool _isSingUp;
    
    #endregion

    
    
    #region METHODS

    protected override async Task OnFirstRenderAsync()
    {
        Base.CustomBackground = null;
        if (Base.AuthorizedAccount is not null)
        {
            NavigationManager.NavigateTo(WebUtility.UrlDecode(RedirectTo));
        }
    }

    #endregion
}
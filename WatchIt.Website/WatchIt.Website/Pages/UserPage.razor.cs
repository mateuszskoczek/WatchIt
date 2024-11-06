using System.Net;
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Website.Layout;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Client.Accounts;
using WatchIt.Website.Services.Client.Media;
using WatchIt.Website.Services.Client.Persons;

namespace WatchIt.Website.Pages;

public partial class UserPage : ComponentBase
{
    #region SERVICES
    
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] private IAccountsClientService AccountsClientService { get; set; } = default!;
    [Inject] private IMediaClientService MediaClientService { get; set; } = default!;
    [Inject] private IPersonsClientService PersonsClientService { get; set; } = default!;
    
    #endregion 
    
    
    
    #region PARAMETERS
    
    [Parameter] public long? Id { get; set; }
    
    [CascadingParameter] public MainLayout Layout { get; set; } = default!;
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    private bool _redirection;
    private bool _owner;
    private AccountResponse? _accountData;
    
    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> step1Tasks = new List<Task>();
            List<Task> endTasks = new List<Task>();
            
            // INIT
            Layout.BackgroundPhoto = null;
            
            // STEP 0
            step1Tasks.AddRange(
            [
                GetUserData()
            ]);
            
            // STEP 1
            await Task.WhenAll(step1Tasks);
            endTasks.AddRange(
            [
                AccountsClientService.GetAccountProfileBackground(_accountData.Id, data => Layout.BackgroundPhoto = data)
            ]);
            
            // END
            await Task.WhenAll(endTasks);

            _loaded = !_redirection;
            StateHasChanged();
        }
    }

    private async Task GetUserData()
    {
        User? user = await AuthenticationService.GetUserAsync();
        if (!Id.HasValue)
        {
            if (user is null)
            {
                NavigationManager.NavigateTo($"/auth?redirect_to={WebUtility.UrlEncode("/user")}");
                _redirection = true;
                return;
            }
            Id = user.Id;
        }
        
        await AccountsClientService.GetAccount(Id.Value, data => _accountData = data);
        _owner = Id.Value == user?.Id;
    }
    

    #endregion
}
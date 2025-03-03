using System.Net;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Accounts;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Controllers.Accounts.AccountProfileInfo;
using WatchIt.DTO.Models.Controllers.Genders.Gender;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Panels.Pages.UserEditPage;

public partial class EditFormPanel : Component
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IAccountsClient AccountsClient { get; set; } = null!;
    [Inject] private IGendersClient GendersClient { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required AccountResponse Data { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    private bool _saving;

    private IEnumerable<GenderResponse> _genders = [];
    
    private AccountProfileInfoRequest _data = new AccountProfileInfoRequest();

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        _data = Data.ToProfileInfoRequest();
        IApiResponse<IEnumerable<GenderResponse>> response = await GendersClient.GetGenders();
        if (response.IsSuccessful)
        {
            _genders = response.Content;
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error occured. List of genders could not be obtained.", SnackbarColor.Danger);
        }
            
        _loaded = true;
        StateHasChanged();
    }

    private async Task Save()
    {
        _saving = true;
        
        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;
        
        IApiResponse response = await AccountsClient.PatchAccountProfileInfo(token, _data!);
        switch (response)
        {
            case { IsSuccessful: true}:
                await Base.SnackbarStack.PushAsync("Profile info successfully saved.", SnackbarColor.Success);
                break;
            case { StatusCode: HttpStatusCode.Forbidden } or { StatusCode: HttpStatusCode.Unauthorized }:
                await Base.SnackbarStack.PushAsync("Authentication error", SnackbarColor.Danger);
                break;
            case { StatusCode: HttpStatusCode.BadRequest }:
                string? content = "An unknown error occured.";
                if (response.Error is ValidationApiException ex)
                {
                    string? exContent = ex.Content?.Errors.SelectMany(x => x.Value).FirstOrDefault();
                    if (exContent is not null)
                    {
                        content = exContent;
                    }
                }
                await Base.SnackbarStack.PushAsync(content, SnackbarColor.Danger);
                break;
            default:
                await Base.SnackbarStack.PushAsync("An unknown error occured.", SnackbarColor.Danger);
                break;
        }
        
        _saving = false;
    }

    #endregion
}
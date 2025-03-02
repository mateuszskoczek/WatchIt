using System.Net;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Genders.Gender;
using WatchIt.DTO.Models.Controllers.People;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Panels.Pages.PersonEditPage;

public partial class EditFormPanel : Component
{
    #region SERVICES

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IPeopleClient PeopleClient { get; set; } = null!;
    [Inject] private IGendersClient GendersClient { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS

    [Parameter] public PersonResponse? Data { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;
    
    #endregion



    #region FIELDS

    private bool _loaded;
    private bool _saving;
    
    private List<GenderResponse> _genders = [];
    
    private PersonRequest _request = new PersonRequest();

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();

        if (Data is not null)
        {
            _request = Data.ToRequest();
        }

        IApiResponse<IEnumerable<GenderResponse>> gendersResponse = await GendersClient.GetGenders();
        if (gendersResponse.IsSuccessful)
        {
            _genders.AddRange(gendersResponse.Content);
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error has occured. List of genders could not be obtained.", SnackbarColor.Danger);
        }
        
        _loaded = true;
        StateHasChanged();
    }

    private async Task SaveData()
    {
        _saving = true;

        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;

        IApiResponse<PersonResponse> response = await (Data switch
        {
            null => PeopleClient.PostPerson(token, _request),
            _ => PeopleClient.PutPerson(token, Data.Id, _request),
        });
        switch (response)
        {
            case { IsSuccessful: true }:
                switch (Data)
                {
                    case null: NavigationManager.NavigateTo($"people/{response.Content.Id}/edit", true); break;
                    default: await Base.SnackbarStack.PushAsync("Data saved successfully.", SnackbarColor.Success); break;
                }
                break;
            case { StatusCode: HttpStatusCode.Forbidden } or { StatusCode: HttpStatusCode.Unauthorized }:
                await Base.SnackbarStack.PushAsync("You are not authorized to edit people data.", SnackbarColor.Danger);
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
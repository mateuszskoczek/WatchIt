using System.Net;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Media;
using WatchIt.DTO.Models.Controllers.Media.Medium.Request;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Panels.Pages.MediumEditPage;

public partial class EditFormPanel : Component
{
    #region SERVICES

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IMediaClient MediaClient { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS

    [Parameter] public BaseMediumResponse? Data { get; set; }
    [Parameter] public required NullType TypeIfNull { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;
    
    #endregion



    #region FIELDS

    private bool _loaded;
    private bool _saving;

    private MediumRequest? _request;

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();
        
        LoadData();
        
        _loaded = true;
        StateHasChanged();
    }

    private void LoadData()
    {
        _request = Data switch
        {
            null => TypeIfNull switch
            {
                NullType.Movie => new MediumMovieRequest(),
                NullType.Series => new MediumSeriesRequest(),
            },
            _ => Data.ToRequest()
        };
    }

    private async Task SaveData()
    {
        _saving = true;
        
        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;

        IApiResponse<BaseMediumResponse> response = Data switch
        {
            null => TypeIfNull switch
            {
                NullType.Movie => await MediaClient.PostMediumMovie(token, _request as MediumMovieRequest ?? throw new InvalidOperationException()),
                NullType.Series => await MediaClient.PostMediumSeries(token, _request as MediumSeriesRequest ?? throw new InvalidOperationException()),
            },
            MediumMovieResponse => await MediaClient.PutMediumMovie(token, Data.Id, _request as MediumMovieRequest ?? throw new InvalidOperationException()),
            MediumSeriesResponse => await MediaClient.PutMediumSeries(token, Data.Id, _request as MediumSeriesRequest ?? throw new InvalidOperationException()),
            _ => throw new InvalidOperationException()
        };
        switch (response)
        {
            case { IsSuccessful: true }:
                switch (Data)
                {
                    case null: NavigationManager.NavigateTo($"media/{response.Content.Id}/edit", true); break;
                    default: await Base.SnackbarStack.PushAsync("Data saved successfully.", SnackbarColor.Success); break;
                }
                break;
            case { StatusCode: HttpStatusCode.Forbidden } or { StatusCode: HttpStatusCode.Unauthorized }:
                await Base.SnackbarStack.PushAsync("You are not authorized to edit media data.", SnackbarColor.Danger);
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
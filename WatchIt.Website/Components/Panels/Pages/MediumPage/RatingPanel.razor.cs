using System.Net;
using System.Runtime.InteropServices;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Panels.Pages.MediumPage;

public partial class RatingPanel : Component
{
    #region SERVICES
    
    [Inject] protected IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] protected IMediaClient MediaClient { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required BaseMediumResponse Data { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private RatingOverallResponse _globalRating;
    private int _userRating = 0;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();

        _globalRating = Data.Rating;

        if (Base.AuthorizedAccount is not null)
        {
            IApiResponse<RatingUserResponse> response = await MediaClient.GetMediumUserRating(Data.Id, Base.AuthorizedAccount.Id);
            if (response.IsSuccessful || response.StatusCode == HttpStatusCode.NotFound)
            {
                _userRating = Convert.ToInt32(response.Content?.Rating ?? 0);
            }
            else
            {
                await Base.SnackbarStack.PushAsync("An error occured. User rating could not be obtained.", SnackbarColor.Danger);
            }
        }
        
        StateHasChanged();
    }

    private async Task UserRatingChanged(int value)
    {
        if (value == _userRating)
        {
            return;
        }
        
        string accessToken = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;

        IApiResponse response;
        if (value == 0)
        {
            response = await MediaClient.DeleteMediumRating(accessToken, Data.Id);
        }
        else
        {
            response = await MediaClient.PutMediumRating(accessToken, Data.Id, new RatingRequest { Rating = Convert.ToByte(value) });
        }
        
        if (response.IsSuccessful)
        {
            _userRating = value;

            IApiResponse<RatingOverallResponse> globalRatingResponse = await MediaClient.GetMediumRating(Data.Id);
            if (globalRatingResponse.IsSuccessful)
            {
                _globalRating = globalRatingResponse.Content;
            }
            else
            {
                await Base.SnackbarStack.PushAsync("An error occured. Global rating could not be updated.", SnackbarColor.Danger);
            }
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error occured. User rating could not be changed.", SnackbarColor.Danger);
        }
    }

    #endregion
}
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.Website.Clients;

namespace WatchIt.Website.Components.Subcomponents.Common;

public partial class AccountPicture : Component
{
    #region SERVICES
    
    [Inject] private IAccountsClient AccountsClient { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required AccountResponse Item { get; set; }
    [Parameter] public bool ProfilePictureIncluded { get; set; }
    [Parameter] public required int Size { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;
    
    #endregion
    
    
    
    #region FIELDS
    
    private ImageResponse? _picture; 
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        if (ProfilePictureIncluded)
        {
            _picture = Item.ProfilePicture;
        }
        else
        {
            IApiResponse<ImageResponse> response = await AccountsClient.GetAccountProfilePicture(Item.Id);
            if (response.IsSuccessful)
            {
                _picture = response.Content;
            }
        }
        StateHasChanged();
    }

    #endregion
}
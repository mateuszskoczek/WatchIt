using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Subcomponents.Common;

public partial class VerticalListItem : Component
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required string Name { get; set; }
    [Parameter] public string? AdditionalInfo { get; set; }
    
    [Parameter] public int NameSize { get; set; } = 25;
    
    [Parameter] public required string PicturePlaceholder { get; set; }
    [Parameter] public int PictureHeight { get; set; } = 150;
    [Parameter] public required Func<Task<ImageResponse?>> PictureFunc { get; set; }
    
    [Parameter] public string? SecondaryRatingTitle { get; set; }

    [Parameter] public RatingOverallResponse? GlobalRating { get; set; }
    [Parameter] public required Func<Task<RatingOverallResponse?>> GetGlobalRatingMethod { get; set; }
    [Parameter] public Func<Task<IRatingResponse?>>? GetSecondaryRatingMethod { get; set; }
    [Parameter] public Func<long, Task<int?>>? GetYourRatingMethod { get; set; }
    [Parameter] public Func<RatingRequest, Task>? PutYourRatingMethod { get; set; }
    [Parameter] public Func<Task>? DeleteYourRatingMethod { get; set; }
    [Parameter] public Action? OnRatingChanged { get; set; }
    
    [Parameter] public required string ItemUrl { get; set; }

    #endregion
    
    
    
    #region FIELDS
    
    private ImageResponse? _picture;
    
    private IRatingResponse? _secondaryRating;
    private int _yourRating;

    private bool _globalRatingLoaded;
    private bool _secondaryRatingLoaded;
    private bool _yourRatingLoaded;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();
        
        List<Task> tasks = 
        [
            Task.Run(async () => _picture = await PictureFunc()),
        ];
        if (GlobalRating is null)
        {
            tasks.Add(UpdateGlobalRating());
        }
        else
        {
            _globalRatingLoaded = true;
        }
        if (GetSecondaryRatingMethod is not null && !string.IsNullOrWhiteSpace(SecondaryRatingTitle))
        {
            tasks.Add(GetSecondaryRatingMethod());
        }
        if (GetYourRatingMethod is not null && Base.AuthorizedAccount is not null)
        {
            tasks.Add(GetUserRating());
        }
        await Task.WhenAll(tasks);
        StateHasChanged();
    }

    private async Task GetUserRating()
    {
        int? rating = await GetYourRatingMethod!(Base.AuthorizedAccount!.Id);
        _yourRating = rating ?? 0;
        _yourRatingLoaded = true;
    }
    
    private async Task RatingChanged()
    {
        if (Base.AuthorizedAccount is null)
        {
            await Base.SnackbarStack.PushAsync("An error has occurred. You are not logged in.", SnackbarColor.Danger);
            return;
        }
        
        if (_yourRating == 0)
        {
            await DeleteYourRatingMethod!();
        }
        else
        {
            await PutYourRatingMethod!(new RatingRequest
            {
                Rating = (byte)_yourRating,
            });
        }
        
        await UpdateGlobalRating();
        OnRatingChanged?.Invoke();
    }

    private async Task UpdateGlobalRating()
    {
        _globalRatingLoaded = false;
        GlobalRating = await GetGlobalRatingMethod();
        _globalRatingLoaded = true;
    }

    #endregion
}
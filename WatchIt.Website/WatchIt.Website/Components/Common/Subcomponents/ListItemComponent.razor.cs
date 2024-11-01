using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;
using WatchIt.Common.Model.Rating;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Common.Subcomponents;

public partial class ListItemComponent : ComponentBase
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required string Name { get; set; }
    [Parameter] public string? AdditionalInfo { get; set; }
    
    [Parameter] public int NameSize { get; set; } = 25;
    
    [Parameter] public required string PosterPlaceholder { get; set; }
    [Parameter] public int PosterHeight { get; set; } = 150;
    [Parameter] public required Func<Action<Picture>, Task> PosterDownloadingTask { get; set; }
    
    [Parameter] public RatingResponse? GlobalRating { get; set; }
    [Parameter] public short? SecondaryRatingSingle { get; set; }
    [Parameter] public RatingResponse? SecondaryRatingMultiple { get; set; }
    [Parameter] public string? SecondaryRatingTitle { get; set; }
    [Parameter] public required Func<Action<RatingResponse>, Task> GetGlobalRatingMethod { get; set; }
    [Parameter] public Func<long, Action<short>, Action, Task>? GetUserRatingMethod { get; set; }
    [Parameter] public Func<RatingRequest, Task>? PutRatingMethod { get; set; }
    [Parameter] public Func<Task>? DeleteRatingMethod { get; set; }
    [Parameter] public Action? OnRatingChanged { get; set; }
    
    [Parameter] public required string ItemUrl { get; set; }

    #endregion
    
    
    
    #region FIELDS
    
    private User? _user;
    
    private Picture? _poster;
    
    private int _userRating;
    private bool _userRatingLoaded;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> step1Tasks = new List<Task>(1);
            List<Task> endTasks = new List<Task>(3);
            
            // STEP 0
            if (GetUserRatingMethod is not null)
            {
                step1Tasks.AddRange(
                [
                    Task.Run(async () => _user = await AuthenticationService.GetUserAsync()), 
                ]);
            }
            endTasks.AddRange(
            [
                PosterDownloadingTask(data => _poster = data),
            ]);
            if (GlobalRating is null)
            {
                endTasks.AddRange(
                [
                    GetGlobalRatingMethod(data => GlobalRating = data),
                ]);
            }
            
            // STEP 1
            await Task.WhenAll(step1Tasks);
            StateHasChanged();
            if (GetUserRatingMethod is not null && _user is not null)
            {
                endTasks.AddRange(
                [
                    GetUserRatingMethod(_user.Id, 
                        data =>
                        {
                            _userRating = data;
                            _userRatingLoaded = true;
                        }, 
                        () =>
                        {
                            _userRating = 0;
                            _userRatingLoaded = true;
                        }
                    )
                ]);
            }
            
            await Task.WhenAll(endTasks);
            
            StateHasChanged();
        }
    }
    
    private async Task RatingChanged()
    {
        if (_userRating == 0)
        {
            await DeleteRatingMethod!();
        }
        else
        {
            await PutRatingMethod!(new RatingRequest((short)_userRating));
        }
        
        await GetGlobalRatingMethod(data => GlobalRating = data);
        OnRatingChanged?.Invoke();
    }

    #endregion
}
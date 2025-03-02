namespace WatchIt.Website.Components.Pages;

public abstract class Page : Component
{
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();
        
        Base.CustomBackground = null;
        StateHasChanged();
    }

    #endregion
}
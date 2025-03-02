using Microsoft.AspNetCore.Components;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.People;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Controllers.Roles;
using WatchIt.DTO.Models.Controllers.Roles.Role.Query;
using WatchIt.DTO.Models.Controllers.Roles.Role.Response;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Panels.Common;

public partial class RolesPanel<TRole, TRoleParent> : Component where TRole : RoleResponse
{
    #region SERVICES

    [Inject] protected IRolesClient RolesClient { get; set; } = null!;
    [Inject] protected IAuthenticationService AuthenticationService { get; set; } = null!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required string Title { get; set; }
    [Parameter] public IEnumerable<TRoleParent>? RoleParents { get; set; }
    [Parameter] public required string ParentName { get; set; }
    
    [Parameter] public required Func<TRole, TRoleParent, string> NameFunc { get; set; }
    [Parameter] public required Func<TRole, TRoleParent, string>? AdditionalInfoFunc { get; set; }
    [Parameter] public required string PicturePlaceholder { get; set; }
    [Parameter] public required Func<TRole, TRoleParent, Task<ImageResponse?>> PictureFunc { get; set; }
    [Parameter] public required Func<TRole, TRoleParent, string> UrlFunc { get; set; }
    [Parameter] public required Func<TRole, TRoleParent, RatingOverallResponse> GlobalRatingFunc { get; set; }
    
    [Parameter] public required Func<Task<IEnumerable<IRoleTypeResponse>?>> GetRoleTypesMethod { get; set; }
    [Parameter] public required Func<Task<IEnumerable<TRole>?>> GetRolesMethod { get; set; }
    [Parameter] public required Func<TRole, IEnumerable<TRoleParent>, TRoleParent> ParentFunc { get; set; }
    [Parameter] public Action? OnRatingChanged { get; set; }
    
    [Parameter] public string Class { get; set; } = string.Empty;

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private IEnumerable<IRoleTypeResponse>? _roleTypes;
    private IEnumerable<TRole>? _roles;

    private short _checkedType;
    private IEnumerable<TRole> _rolesVisible = [];

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();

        await LoadRoles();
        if (_roles is not null && _roles.Any())
        {
            await LoadRoleTypes();
        }

        if (_roleTypes is not null && _roleTypes.Any())
        {
            _checkedType = _roleTypes.First().Id;
            //_rolesVisible = _roles.Where(x => x.TypeId == _checkedType);
        }
        
        _loaded = true;
        StateHasChanged();
    }

    private async Task LoadRoles()
    {
        _roles = await GetRolesMethod();
    }

    private async Task LoadRoleTypes()
    {
        IEnumerable<IRoleTypeResponse>? roleTypesOriginal = await GetRoleTypesMethod();
        IEnumerable<short>? roleTypesId = _roles!.Select(x => x.TypeId).Distinct();
        _roleTypes = roleTypesOriginal?.Where(x => roleTypesId.Contains(x.Id));
    }
    
    private void CheckedTypeChanged(short value)
    {
        _checkedType = value;
        _rolesVisible = _roles.Where(x => x.TypeId == value);
    }

    #endregion
}
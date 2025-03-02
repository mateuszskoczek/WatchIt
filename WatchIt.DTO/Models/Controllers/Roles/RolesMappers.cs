using WatchIt.Database.Model.Roles;
using WatchIt.DTO.Models.Controllers.Roles.Role.Request;
using WatchIt.DTO.Models.Controllers.Roles.Role.Response;
using WatchIt.DTO.Models.Controllers.Roles.RoleActorType;
using WatchIt.DTO.Models.Controllers.Roles.RoleCreatorType;
using WatchIt.DTO.Models.Generics.Rating;

namespace WatchIt.DTO.Models.Controllers.Roles;

public static class RolesMappers
{
    #region PUBLIC METHODS

    #region Roles

    public static RoleActor ToEntity(this RoleActorRequest request)
    {
        RoleActor roleActor = new RoleActor();
        roleActor.SetRoleEntityProperties(request);
        roleActor.ActorTypeId = request.TypeId;
        roleActor.Name = request.Name;
        return roleActor;
    }
    
    public static void UpdateWithRequest(this RoleActor entity, RoleActorRequest request)
    {
        entity.SetRoleEntityProperties(request);
        entity.ActorTypeId = request.TypeId;
        entity.Name = request.Name;
    }
    
    public static RoleCreator ToEntity(this RoleCreatorRequest request)
    {
        RoleCreator roleActor = new RoleCreator();
        roleActor.SetRoleEntityProperties(request);
        roleActor.CreatorTypeId = request.TypeId;
        return roleActor;
    }
    
    public static void UpdateWithRequest(this RoleCreator entity, RoleCreatorRequest request)
    {
        entity.SetRoleEntityProperties(request);
        entity.CreatorTypeId = request.TypeId;
    }

    public static RoleActorResponse ToResponse(this RoleActor entity)
    { 
        RoleActorResponse response = new RoleActorResponse();
        response.SetRoleResponseProperties(entity);
        response.Name = entity.Name;
        response.TypeId = entity.ActorTypeId;
        return response;
    }
    
    public static RoleCreatorResponse ToResponse(this RoleCreator entity)
    { 
        RoleCreatorResponse response = new RoleCreatorResponse();
        response.SetRoleResponseProperties(entity);
        response.TypeId = entity.CreatorTypeId;
        return response;
    }

    #endregion
    
    #region RoleRating

    public static RoleRating ToEntity(this RatingRequest request, Guid roleId, long userId)
    {
        RoleRating entity = new RoleRating
        {
            RoleId = roleId,
            AccountId = userId
        };
        entity.UpdateWithRequest(request);
        return entity;
    }
    
    #endregion
    
    #region RoleActorType
    
    public static RoleActorTypeResponse ToResponse(this Database.Model.Roles.RoleActorType entity) => new RoleActorTypeResponse
    {
        Id = entity.Id,
        Name = entity.Name,
    };

    public static Database.Model.Roles.RoleActorType ToEntity(this RoleActorTypeRequest request) => new Database.Model.Roles.RoleActorType
    {
        Name = request.Name,
    };
    
    #endregion
    
    #region RoleCreatorType
    
    public static RoleCreatorTypeResponse ToResponse(this Database.Model.Roles.RoleCreatorType entity) => new RoleCreatorTypeResponse
    {
        Id = entity.Id,
        Name = entity.Name,
    };

    public static Database.Model.Roles.RoleCreatorType ToEntity(this RoleCreatorTypeRequest request) => new Database.Model.Roles.RoleCreatorType
    {
        Name = request.Name,
    };
    
    #endregion
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    private static void SetRoleEntityProperties(this Database.Model.Roles.Role role, RoleRequest request)
    {
        role.MediumId = request.MediumId;
        role.PersonId = request.PersonId;
    }
    
    private static void SetRoleResponseProperties(this RoleResponse response, Database.Model.Roles.Role entity)
    {
        response.Id = entity.Id;
        response.PersonId = entity.PersonId;
        response.MediumId = entity.MediumId;
    }
    
    #endregion
}
using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using WatchIt.Database.Model.Accounts;
using WatchIt.Database.Model.Roles;
using WatchIt.DTO;
using WatchIt.DTO.Models.Controllers.Roles;
using WatchIt.DTO.Models.Controllers.Roles.Role.Query;
using WatchIt.DTO.Models.Controllers.Roles.Role.Request;
using WatchIt.DTO.Models.Controllers.Roles.Role.Response;
using WatchIt.DTO.Models.Controllers.Roles.RoleActorType;
using WatchIt.DTO.Models.Controllers.Roles.RoleCreatorType;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.Repositories.Roles;
using WatchIt.WebAPI.Services.User;

namespace WatchIt.WebAPI.BusinessLogic.Roles;

public class RolesBusinessLogic : IRolesBusinessLogic
{
    #region SERVICES

    private readonly IUserService _userService;
    private readonly IRolesRepository _rolesRepository;

    #endregion
    
    
    
    #region CONSTRUCTORS
    
    public RolesBusinessLogic(IUserService userService, IRolesRepository rolesRepository)
    {
        _userService = userService;
        _rolesRepository = rolesRepository;
    }
    
    #endregion



    #region PUBLIC METHODS
    
    #region Main - CRUD

    public async Task<Result<IEnumerable<RoleActorResponse>>> GetRoleActors(RoleActorFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery)
    {
        IEnumerable<RoleActor> entities = await _rolesRepository.GetAllActorsAsync(filterQuery, orderQuery, pagingQuery);
        return Result.Success(entities.Select(x => x.ToResponse()));
    }

    public async Task<Result<RoleActorResponse>> GetRoleActor(Guid roleId)
    {
        RoleActor? entity = await _rolesRepository.GetAsync<RoleActor>(roleId);
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse()),
        };
    }

    public async Task<Result<IEnumerable<RoleCreatorResponse>>> GetRoleCreators(RoleCreatorFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery)
    {
        IEnumerable<RoleCreator> entities = await _rolesRepository.GetAllCreatorsAsync(filterQuery, orderQuery, pagingQuery);
        return Result.Success(entities.Select(x => x.ToResponse()));
    }

    public async Task<Result<RoleCreatorResponse>> GetRoleCreator(Guid roleId)
    {
        RoleCreator? entity = await _rolesRepository.GetAsync<RoleCreator>(roleId);
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse()),
        };
    }

    public async Task<Result<RoleActorResponse>> PostRoleActor(RoleActorRequest body)
    {
        RoleActor entity = body.ToEntity();
        await _rolesRepository.AddAsync(entity);
        return Result.Created(entity.ToResponse());
    }

    public async Task<Result<RoleCreatorResponse>> PostRoleCreator(RoleCreatorRequest body)
    {
        RoleCreator entity = body.ToEntity();
        await _rolesRepository.AddAsync(entity);
        return Result.Created(entity.ToResponse());
    }

    public async Task<Result<RoleActorResponse>> PutRoleActor(Guid roleId, RoleActorRequest body)
    {
        return await _rolesRepository.UpdateAsync<RoleActor>(roleId, x => x.UpdateWithRequest(body)) switch
        {
            false => Result.NotFound(),
            true => Result.Success()
        };
    }

    public async Task<Result<RoleCreatorResponse>> PutRoleCreator(Guid roleId, RoleCreatorRequest body)
    {
        return await _rolesRepository.UpdateAsync<RoleCreator>(roleId, x => x.UpdateWithRequest(body)) switch
        {
            false => Result.NotFound(),
            true => Result.Success()
        };
    }

    public async Task<Result> DeleteRole(Guid roleId)
    {
        await _rolesRepository.DeleteAsync(roleId);
        return Result.NoContent();
    }
    
    #endregion
    
    #region Main - Rating
    
    public async Task<Result<RatingOverallResponse>> GetRoleRating(Guid roleId)
    {
        if (!await _rolesRepository.ExistsAsync(roleId))
        {
            return Result.NotFound();
        }
        
        IEnumerable<RoleRating> ratings = await _rolesRepository.GetRoleRatingsAsync(roleId);
        return Result.Success(ratings.ToOverallResponse());
    }

    public async Task<Result<RatingUserResponse>> GetRoleUserRating(Guid roleId, long accountId)
    {
        RoleRating? entity = await _rolesRepository.GetRoleRatingByUserAsync(roleId, accountId);
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToUserResponse())
        };
    }

    public async Task<Result> PutRoleRating(Guid roleId, RatingRequest body)
    {
        Account accountEntity = await _userService.GetAccountAsync();
        Role? roleEntity = await _rolesRepository.GetAsync(roleId);
        if (roleEntity is null)
        {
            return Result.NotFound();
        }
        await _rolesRepository.UpdateOrAddRoleRatingAsync(roleEntity.Id, accountEntity.Id, () => body.ToEntity(roleEntity.Id, accountEntity.Id), x => x.UpdateWithRequest(body));
        return Result.Success();
    }

    public async Task<Result> DeleteRoleRating(Guid roleId)
    {
        Account accountEntity = await _userService.GetAccountAsync();
        await _rolesRepository.DeleteRoleUserRatingAsync(roleId, accountEntity.Id);
        return Result.NoContent();
    }

    
    #endregion

    #region ActorTypes

    public async Task<Result<IEnumerable<RoleActorTypeResponse>>> GetRoleActorTypes(RoleActorTypeFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery)
    {
        IEnumerable<RoleActorType> entities = await _rolesRepository.GetAllActorTypesAsync(filterQuery, orderQuery, pagingQuery);
        return Result.Success(entities.Select(x => x.ToResponse()));
    }

    public async Task<Result<RoleActorTypeResponse>> GetRoleActorType(short roleActorTypeId)
    {
        RoleActorType? entity = await _rolesRepository.GetActorTypeAsync(roleActorTypeId);
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse())
        };
    }

    public async Task<Result<RoleActorTypeResponse>> PostRoleActorType(RoleActorTypeRequest body)
    {
        RoleActorType entity = body.ToEntity();
        await _rolesRepository.AddActorTypeAsync(body.ToEntity());
        return Result.Created(entity.ToResponse());
    }

    public async Task<Result> DeleteRoleActorType(short roleActorTypeId)
    {
        await _rolesRepository.DeleteActorTypeAsync(roleActorTypeId);
        return Result.NoContent();
    }

    #endregion

    #region CreatorTypes

    public async Task<Result<IEnumerable<RoleCreatorTypeResponse>>> GetRoleCreatorTypes(RoleCreatorTypeFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery)
    {
        IEnumerable<RoleCreatorType> entities = await _rolesRepository.GetAllCreatorTypesAsync(filterQuery, orderQuery, pagingQuery);
        return Result.Success(entities.Select(x => x.ToResponse()));
    }

    public async Task<Result<RoleCreatorTypeResponse>> GetRoleCreatorType(short roleCreatorTypeId)
    {
        RoleCreatorType? entity = await _rolesRepository.GetCreatorTypeAsync(roleCreatorTypeId);
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse())
        };
    }

    public async Task<Result<RoleCreatorTypeResponse>> PostRoleCreatorType(RoleCreatorTypeRequest body)
    {
        RoleCreatorType entity = body.ToEntity();
        await _rolesRepository.AddCreatorTypeAsync(body.ToEntity());
        return Result.Created(entity.ToResponse());
    }

    public async Task<Result> DeleteRoleCreatorType(short roleCreatorTypeId)
    {
        await _rolesRepository.DeleteActorTypeAsync(roleCreatorTypeId);
        return Result.NoContent();
    }

    #endregion
    
    #endregion
}
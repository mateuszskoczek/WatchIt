using WatchIt.Database.Model.People;
using WatchIt.DTO.Models.Controllers.Genders;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Models.Generics.ViewCount;

namespace WatchIt.DTO.Models.Controllers.People;

public static class PeopleMappers
{
    #region PUBLIC METHODS

    #region Person

    public static Database.Model.People.Person ToEntity(this PersonRequest request) => new Database.Model.People.Person
    {
        Name = request.Name,
        FullName = request.FullName,
        Description = request.Description,
        BirthDate = request.BirthDate,
        DeathDate = request.DeathDate,
        GenderId = request.GenderId,
    };

    public static void UpdateWithRequest(this Database.Model.People.Person entity, PersonRequest request)
    {
        entity.Name = request.Name;
        entity.FullName = request.FullName;
        entity.Description = request.Description;
        entity.BirthDate = request.BirthDate;
        entity.DeathDate = request.DeathDate;
        entity.GenderId = request.GenderId;
    }

    public static PersonResponse ToResponse(this Database.Model.People.Person entity)
    {
        PersonResponse response = new PersonResponse();
        response.SetPersonResponseProperties(entity);
        return response;
    }

    public static PersonUserRatedResponse ToResponse(this Database.Model.People.Person entity, long accountId)
    {
        PersonUserRatedResponse response = new PersonUserRatedResponse();
        response.SetPersonResponseProperties(entity);
        response.RatingUser = entity.Roles
                                    .SelectMany(x => x.Ratings
                                                      .Where(y => y.AccountId == accountId))
                                    .ToUserOverallResponse();
        return response;
    }

    public static PersonRequest ToRequest(this PersonResponse response) => new PersonRequest
    {
        Name = response.Name,
        FullName = response.FullName,
        Description = response.Description,
        BirthDate = response.BirthDate,
        DeathDate = response.DeathDate,
        GenderId = response.Gender?.Id,
    };

    #endregion

    #region PersonPicture

    public static Database.Model.People.PersonPicture ToEntity(this ImageRequest request, long personId) => new Database.Model.People.PersonPicture
    {
        PersonId = personId,
        Image = request.Image,
        MimeType = request.MimeType,
    };

    #endregion

    #region MediumViewCount

    public static PersonViewCount CreatePersonViewCountEntity(long personId) => new PersonViewCount
    {
        PersonId = personId,
        ViewCount = 1,
    };

    #endregion

    #endregion
    
    
    
    #region PRIVATE METHODS
    
    private static void SetPersonResponseProperties(this PersonResponse response, Database.Model.People.Person entity)
    {
        response.Id = entity.Id;
        response.Name = entity.Name;
        response.FullName = entity.FullName;
        response.Description = entity.Description;
        response.BirthDate = entity.BirthDate;
        response.DeathDate = entity.DeathDate;
        response.Gender = entity.Gender?.ToResponse();
        response.Rating = entity.Roles.SelectMany(x => x.Ratings).ToOverallResponse();
        response.ViewCount = entity.ViewCounts.ToResponse();
        response.Picture = entity.Picture?.ToResponse();
    }
    
    #endregion
}
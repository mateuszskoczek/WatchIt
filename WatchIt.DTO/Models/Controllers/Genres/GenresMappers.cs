using WatchIt.DTO.Models.Controllers.Genres.Genre;

namespace WatchIt.DTO.Models.Controllers.Genres;

public static class GenresMappers
{
    #region PUBLIC METHODS

    public static GenreResponse ToResponse(this Database.Model.Genres.Genre entity) => new GenreResponse
    {
        Id = entity.Id,
        Name = entity.Name,
    };

    public static Database.Model.Genres.Genre ToEntity(this GenreRequest request) => new Database.Model.Genres.Genre
    {
        Name = request.Name,
    };

    #endregion
}
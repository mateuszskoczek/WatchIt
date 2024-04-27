using Microsoft.EntityFrameworkCore;
using WatchIt.Common.Model.Genres;
using WatchIt.Database;
using WatchIt.Database.Model.Media;
using WatchIt.WebAPI.Services.Controllers.Common;
using WatchIt.WebAPI.Services.Utility.User;
using Genre = WatchIt.Database.Model.Common.Genre;

namespace WatchIt.WebAPI.Services.Controllers.Genres;

public class GenresControllerService(DatabaseContext database, IUserService userService) : IGenresControllerService
{
    #region PUBLIC METHODS

    public async Task<RequestResult> GetAll(GenreQueryParameters query)
    {
        IEnumerable<GenreResponse> data = await database.Genres.Select(x => new GenreResponse(x)).ToListAsync();
        data = query.PrepareData(data);
        return RequestResult.Ok(data);
    }

    public async Task<RequestResult> Get(short id)
    {
        Genre? item = await database.Genres.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        GenreResponse data = new GenreResponse(item);
        return RequestResult.Ok(data);
    }

    public async Task<RequestResult> Post(GenreRequest data)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }

        Genre item = data.CreateGenre();
        await database.Genres.AddAsync(item);
        await database.SaveChangesAsync();
        
        return RequestResult.Created($"genres/{item.Id}", new GenreResponse(item));
    }

    public async Task<RequestResult> Put(short id, GenreRequest data)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        Genre? item = await database.Genres.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NotFound();
        }
        
        data.UpdateGenre(item);
        await database.SaveChangesAsync();
        
        return RequestResult.Ok();
    }

    public async Task<RequestResult> Delete(short id)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        Genre? item = await database.Genres.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        database.MediaGenres.AttachRange(item.MediaGenres);
        database.MediaGenres.RemoveRange(item.MediaGenres);
        database.Genres.Attach(item);
        database.Genres.Remove(item);
        await database.SaveChangesAsync();
        
        return RequestResult.Ok();
    }
    
    #endregion 
}
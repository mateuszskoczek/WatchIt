﻿namespace WatchIt.Common.Model.Genres;

public class GenreRequest : Genre
{
    #region PUBLIC METHODS

    public Database.Model.Common.Genre CreateGenre() => new Database.Model.Common.Genre
    {
        Name = Name,
        Description = Description,
    };

    public void UpdateGenre(Database.Model.Common.Genre genre)
    {
        genre.Name = Name;
        genre.Description = Description;
    }

    #endregion
}
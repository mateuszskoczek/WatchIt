namespace WatchIt.Database.Model;

public interface IViewCountEntity
{
    #region PROPERTIES

    DateOnly Date { get; set; }
    long ViewCount { get; set; }

    #endregion
}
namespace WatchIt.Common.Model.Rating;

public class RatingResponseBuilder
{
    #region FIELDS

    private long _sum;
    private long _count;
    
    #endregion



    #region CONSTRUCTORS

    private RatingResponseBuilder() { }

    public static RatingResponseBuilder Initialize() => new RatingResponseBuilder();

    #endregion



    #region PUBLIC METHODS

    public RatingResponseBuilder Add<T>(IEnumerable<T> collection, Func<T, short> selector)
    {
        _sum += collection.Sum(x => selector(x));
        _count += collection.Count();
        return this;
    }
    public RatingResponse Build() => new RatingResponse(_sum, _count);

    #endregion
}
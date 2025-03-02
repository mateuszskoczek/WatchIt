namespace WatchIt.DTO.Query;

public interface IFilterQuery;

public interface IFilterQuery<T> : IFilterQuery
{
    internal abstract IEnumerable<Filter<T>> GetFilters();
}

public static class FilterQueryExtensions
{
    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> data, IFilterQuery<T> filterQuery)
    {
        foreach (Filter<T> filter in filterQuery.GetFilters())
        {
            data = data.Where(filter);
        }
        return data;
    }
}
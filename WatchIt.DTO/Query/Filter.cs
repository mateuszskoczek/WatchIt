using System.Linq.Expressions;

namespace WatchIt.DTO.Query;

public abstract record Filter<T>(Expression<Func<T, bool>> Condition)
{
    public static implicit operator Expression<Func<T, bool>>(Filter<T> filter) => filter.Condition;
}
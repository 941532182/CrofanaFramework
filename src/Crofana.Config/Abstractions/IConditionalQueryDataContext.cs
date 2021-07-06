using System;
using System.Collections.Generic;

namespace Crofana.Config.Abstractions
{
    public interface IConditionalQueryDataContext : IDataContext
    {
        ICollection<T> GetObjects<T>();
        ICollection<T> GetObjects<T>(IEnumerable<long> ids);
        ICollection<T> GetObjects<T>(Predicate<T> condition);
    }
}

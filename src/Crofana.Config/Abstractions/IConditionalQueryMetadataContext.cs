using System;
using System.Collections.Generic;

namespace Crofana.Config.Abstractions
{
    public interface IConditionalQueryMetadataContext : IMetadataContext
    {
        IDictionary<string, string> GetMetadatas<T>();
        IDictionary<string, string> GetMetadatas<T>(IEnumerable<string> keys);
        IDictionary<string, string> GetMetadatas<T>(Predicate<KeyValuePair<string, string>> condition);
    }
}

using System;

namespace Crofana.Config.Abstractions
{
    interface IAliasableDataContext : IDataContext
    {
        void RegisterAlias(Type type, long id, string alias);
        void UnregisterAlias(Type type, long id);
    }
}

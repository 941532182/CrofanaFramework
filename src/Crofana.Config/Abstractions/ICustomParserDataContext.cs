using System;

namespace Crofana.Config.Abstractions
{
    public interface ICustomParserDataContext : IDataContext
    {
        void RegisterParser(Type type, Func<string, object> parser);
        void UnregisterParser(Type type);
    }
}

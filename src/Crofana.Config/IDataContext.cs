using System;
using System.Collections.Generic;
using System.Text;

namespace Crofana.Config
{
    public interface IDataContext
    {
        void Load(string path);
        bool HasType<T>();
        bool HasObject<T>(long id);
        T GetObject<T>(long id);
        ICollection<T> GetObjects<T>();
        bool HasMetadata<T>(string key);
        string GetMetadata<T>(string key);
        ICollection<string> GetMetadatas<T>();
        void RegisterParser(Type type, Func<string, object> parser);
    }
}

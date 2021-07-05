using System;
using System.Collections.Generic;
using System.Text;

namespace Crofana.Config
{
    public interface IDataContext
    {
        void Load(IDataSourceReader reader);
        T GetObject<T>(long id);
        ICollection<T> GetObjects<T>();
        string GetMetadata<T>(string key);
        ICollection<string> GetMetadatas<T>();
    }
}

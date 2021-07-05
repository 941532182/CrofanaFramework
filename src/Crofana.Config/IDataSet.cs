using System;
using System.Collections.Generic;
using System.Text;

namespace Crofana.Config
{
    public interface IDataSet
    {
        object GetObject(long id);
        ICollection<object> GetObjects();
        string GetMetadata(string key);
        ICollection<string> GetMetadatas();
        void AddObject(long id, object obj);
        void AddMetadata(string key, string value);
    }
}

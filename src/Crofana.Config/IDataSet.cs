using System;
using System.Collections.Generic;
using System.Text;

namespace Crofana.Config
{
    public interface IDataSet
    {
        bool HasObject(long id);
        IConfig GetObject(long id);
        ICollection<IConfig> GetObjects();
        bool HasMetadata(string key);
        string GetMetadata(string key);
        ICollection<string> GetMetadatas();
        void AddObject(long id, IConfig obj);
        void AddMetadata(string key, string value);
    }
}

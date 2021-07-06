using System.Collections.Generic;

namespace Crofana.Config.Abstractions
{
    public interface IMetadataSet : IDataSet
    {
        bool HasMetadata(string key);
        string GetMetadata(string key);
        IDictionary<string, string> GetMetadatas();
        void AddMetadata(string key, string value);
    }
}

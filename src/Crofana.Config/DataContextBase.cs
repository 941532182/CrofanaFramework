using System;
using System.Collections.Generic;
using System.Linq;

namespace Crofana.Config
{
    public abstract class DataContextBase : IDataContext
    {

        private Dictionary<Type, IDataSet> m_dataSetMap;

        public void Load(IDataSourceReader reader)
        {
            while (reader.HasNext)
            {
                var pair = reader.Read();
                m_dataSetMap[pair.type] = pair.data;
            }
        }

        public T GetObject<T>(long id)
        {
            return (T)GetDataSet<T>()?.GetObject(id);
        }

        public ICollection<T> GetObjects<T>()
        {
            return GetDataSet<T>()?.GetObjects().Select(x => (T)x).ToList();
        }

        public string GetMetadata<T>(string key)
        {
            return GetDataSet<T>()?.GetMetadata(key);
        }

        public ICollection<string> GetMetadatas<T>()
        {
            return GetDataSet<T>()?.GetMetadatas();
        }

        private IDataSet GetDataSet<T>()
        {
#if UNSAFE_QUERY
            return m_dataSetMap[typeof(T)];
#else
            return m_dataSetMap.ContainsKey(typeof(T)) ? m_dataSetMap[typeof(T)] : null;
#endif
        }

    }
}

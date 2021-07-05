using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Crofana.Config
{
    public abstract class DataContextBase : IDataContext
    {
        private Dictionary<Type, IDataSet> m_dataSetMap;
        private Dictionary<Type, Func<string, object>> m_parserMap;

        #region
        public void Load(string path)
        {
            m_dataSetMap = new Dictionary<Type, IDataSet>();
            m_parserMap = new Dictionary<Type, Func<string, object>>();
            RegisterParser(typeof(int), value => int.Parse(value));
            RegisterParser(typeof(long), value => long.Parse(value));
            RegisterParser(typeof(float), value => float.Parse(value));
            RegisterParser(typeof(bool), value => bool.Parse(value));
            RegisterParser(typeof(string), value => value);

            ReadDataSourcesRecursively(path);
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

        public void RegisterParser(Type type, Func<string, object> parser)
        {
            m_parserMap[type] = parser;
        }
        #endregion

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

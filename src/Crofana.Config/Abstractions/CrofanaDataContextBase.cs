using System;
using System.Collections.Generic;
using System.Linq;

namespace Crofana.Config.Abstractions
{
    public abstract class CrofanaDataContextBase :
        IConditionalQueryDataContext,
        IConditionalQueryMetadataContext,
        ICustomParserDataContext
    {
        #region IDataContext interface
        public abstract void Load(string url);
        public bool HasType<T>() => DataSetMap.ContainsKey(typeof(T));
        public bool HasObject<T>(long id) => HasType<T>() && GetDataSet<T>().HasObject(id);
        public T GetObject<T>(long id) => (T)GetDataSet<T>()?.GetObject(id);
        #endregion

        #region IConditionalQueryDataContext interface
        // FIXME 减少迭代次数
        public ICollection<T> GetObjects<T>() => GetDataSet<T>()?.GetObjects().Select(x => (T) x).ToList();
        public ICollection<T> GetObjects<T>(IEnumerable<long> ids)
        {
            var list = new List<T>();
            foreach (var id in ids)
            {
                list.Add(GetObject<T>(id));
            }
            return list;
        }
        // FIXME 减少迭代次数
        public ICollection<T> GetObjects<T>(Predicate<T> condition) => GetDataSet<T>()?.GetObjects().Select(x => (T)x).Where(x => condition(x)).ToList();

        #endregion

        #region IMetadataContext interface
        public bool HasMetadata<T>(string key) => HasType<T>() && GetDataSet<T>().HasMetadata(key);
        public string GetMetadata<T>(string key) => GetDataSet<T>()?.GetMetadata(key);
        #endregion

        #region IConditionalQueryMetadataContext interface
        public IDictionary<string, string> GetMetadatas<T>() => GetDataSet<T>()?.GetMetadatas();
        public IDictionary<string, string> GetMetadatas<T>(IEnumerable<string> keys)
        {
            var dic = new Dictionary<string, string>();
            foreach (var key in keys)
            {
                dic[key] = GetMetadata<T>(key);
            }
            return dic;
        }
        public IDictionary<string, string> GetMetadatas<T>(Predicate<KeyValuePair<string, string>> condition)
        {
            var dic = new Dictionary<string, string>();
            foreach (var pair in GetDataSet<T>()?.GetMetadatas())
            {
                if (condition(pair))
                {
                    dic[pair.Key] = pair.Value;
                }
            }
            return dic;
        }
        #endregion

        #region ICustomParserDataContext interface
        public void RegisterParser(Type type, Func<string, object> parser) => ParserMap[type] = parser;
        public void UnregisterParser(Type type) => ParserMap.Remove(type);
        #endregion

        protected abstract IDictionary<Type, IMetadataSet> DataSetMap { get; }
        protected abstract IDictionary<Type, Func<string, object>> ParserMap { get; }

        private IMetadataSet GetDataSet<T>()
        {
#if UNSAFE_QUERY
            return DataSetMap[typeof(T)];
#else
            return DataSetMap.ContainsKey(typeof(T)) ? DataSetMap[typeof(T)] : null;
#endif
        }

    }
}

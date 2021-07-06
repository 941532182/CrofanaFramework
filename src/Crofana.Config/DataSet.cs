using System.Collections.Generic;
using System.Linq;

namespace Crofana.Config
{
    using Abstractions;

    internal class DataSet : IMetadataSet
    {
        private EDuplicatedStrategy m_duplicatedStrategy;
        private Dictionary<long, IConfig> m_objectMap;
        private Dictionary<string, string> m_metadataMap;

        public DataSet() : this(EDuplicatedStrategy.ThrowException) { }

        public DataSet(EDuplicatedStrategy duplicatedStrategy)
        {
            m_duplicatedStrategy = duplicatedStrategy;
            m_objectMap = new Dictionary<long, IConfig>();
            m_metadataMap = new Dictionary<string, string>();
        }

        public bool HasObject(long id)
        {
            return m_objectMap.ContainsKey(id);
        }

        public IConfig GetObject(long id)
        {
#if UNSAFE_QUERY
            return m_objectMap[id];
#else
            return m_objectMap.ContainsKey(id) ? m_objectMap[id] : null;
#endif
        }

        public ICollection<IConfig> GetObjects()
        {
            return m_objectMap.Values.ToList();
        }

        public bool HasMetadata(string key)
        {
            return m_metadataMap.ContainsKey(key);
        }

        public string GetMetadata(string key)
        {
#if UNSAFE_QUERY
            return m_metadataMap[key];
#else
            return m_metadataMap.ContainsKey(key) ? m_metadataMap[key] : null;
#endif
        }

        public IDictionary<string, string> GetMetadatas()
        {
            var dic = new Dictionary<string, string>();
            foreach (var pair in m_metadataMap)
            {
                dic[pair.Key] = pair.Value;
            }
            return dic;
        }

        public void AddObject(long id, IConfig obj)
        {
            if (m_objectMap.ContainsKey(id))
            {
                switch (m_duplicatedStrategy)
                {
                    case EDuplicatedStrategy.ThrowException:
                    {

                        break;
                    }
                    case EDuplicatedStrategy.Override:
                    {
                        break;
                    }
                    case EDuplicatedStrategy.Skip:
                    default:
                    {
                        return;
                    }
                }
            }
            m_objectMap[id] = obj;
        }

        public void AddMetadata(string key, string value)
        {
            if (m_metadataMap.ContainsKey(key))
            {
                switch (m_duplicatedStrategy)
                {
                    case EDuplicatedStrategy.ThrowException:
                    {

                        break;
                    }
                    case EDuplicatedStrategy.Override:
                    {
                        break;
                    }
                    case EDuplicatedStrategy.Skip:
                    default:
                    {
                        return;
                    }
                }
            }
            m_metadataMap[key] = value;
        }

        public enum EDuplicatedStrategy
        {
            ThrowException,
            Override,
            Skip,
        }
    }
}

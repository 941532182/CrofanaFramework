using System.Collections.Generic;

namespace Crofana.Config.Abstractions
{
    public interface IDataSet
    {
        bool HasObject(long id);
        IConfig GetObject(long id);
        ICollection<IConfig> GetObjects();
        void AddObject(long id, IConfig obj);
    }
}

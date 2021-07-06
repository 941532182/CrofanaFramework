namespace Crofana.Config.Abstractions
{
    public interface IDataContext
    {
        void Load(string url);
        bool HasType<T>();
        bool HasObject<T>(long id);
        T GetObject<T>(long id);
    }
}

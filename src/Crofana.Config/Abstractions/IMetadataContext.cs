namespace Crofana.Config.Abstractions
{
    public interface IMetadataContext : IDataContext
    {
        bool HasMetadata<T>(string key);
        string GetMetadata<T>(string key);
    }
}

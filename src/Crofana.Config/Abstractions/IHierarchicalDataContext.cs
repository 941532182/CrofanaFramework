namespace Crofana.Config.Abstractions
{
    public interface IHierarchicalDataContext : IDataContext
    {
        IDataContext Parent { get; }
    }
}

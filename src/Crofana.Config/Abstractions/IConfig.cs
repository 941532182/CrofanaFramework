namespace Crofana.Config.Abstractions
{
    public interface IConfig
    {
        public long Id { get; }
        public bool IsSetupProperly { get; }
    }
}

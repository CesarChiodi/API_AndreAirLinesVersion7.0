namespace MicroServicoLog.Configuracao
{
    public interface ILogAPI
    {
        string LogCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

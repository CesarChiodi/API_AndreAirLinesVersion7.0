namespace MicroServicoLog.Configuracao
{
    public class LogAPI : ILogAPI
    {
        public string LogCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

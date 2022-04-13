namespace MicroServicoAeronaveAPI.Configuracao
{
    public class AeronaveAPI : IAeronaveAPI
    {
        public string AeronaveCollectionName { get; set; }
        public string ConnectionString { get; set; } 
        public string DatabaseName { get; set; }
    }
}

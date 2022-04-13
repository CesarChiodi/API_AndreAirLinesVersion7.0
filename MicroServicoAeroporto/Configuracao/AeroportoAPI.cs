namespace MicroServicoAeroportoAPI.Configuracao
{
    public class AeroportoAPI : IAeroportoAPI
    {
        public string AeroportoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

namespace MicroServicoVooAPI.Configuracao
{
    public class VooAPI : IVooAPI
    {
        public string VooCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

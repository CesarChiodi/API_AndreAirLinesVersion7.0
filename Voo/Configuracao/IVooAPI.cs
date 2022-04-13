namespace MicroServicoVooAPI.Configuracao
{
    public interface IVooAPI
    {
        string VooCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

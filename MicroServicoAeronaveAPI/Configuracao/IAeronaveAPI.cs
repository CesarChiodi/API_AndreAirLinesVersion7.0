namespace MicroServicoAeronaveAPI.Configuracao
{
    public interface IAeronaveAPI
    {
        string AeronaveCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

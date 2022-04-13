namespace MicroServicoAeroportoAPI.Configuracao
{
    public interface IAeroportoAPI
    {
        string AeroportoCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

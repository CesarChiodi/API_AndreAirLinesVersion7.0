namespace MicroServicoPrecoBaseAPI.Configuraao
{
    public interface IPrecoBaseAPI
    {
        string PrecoBaseCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

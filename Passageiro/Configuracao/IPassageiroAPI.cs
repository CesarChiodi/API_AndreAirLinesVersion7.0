namespace MicroServicoPassageiroAPI.Configuracao
{
    public interface IPassageiroAPI
    {
        string PassageiroCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

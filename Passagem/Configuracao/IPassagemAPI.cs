namespace MicroServicoPassagemAereaAPI.Configuracao
{
    public interface IPassagemAPI
    {
        string PassagemAereaCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

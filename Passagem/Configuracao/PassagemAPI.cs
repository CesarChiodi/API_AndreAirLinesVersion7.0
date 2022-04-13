namespace MicroServicoPassagemAereaAPI.Configuracao
{
    public class PassagemAPI : IPassagemAPI
    {
        public string PassagemAereaCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

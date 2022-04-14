namespace MicroServicoPassagemAereaAPI.Configuracao
{
    public class PassagemAPI : IPassagemAPI
    {
        public string PassagemAereaCollectionName { get; set; } = "Passagem";
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public string DatabaseName { get; set; } = "MicroServicoPassagemAPI";
    }
}

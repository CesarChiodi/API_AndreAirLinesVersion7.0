namespace MicroServicoPassageiroAPI.Configuracao
{
    public class PassageiroAPI : IPassageiroAPI
    {
        public string PassageiroCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}


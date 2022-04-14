namespace ApiAndreAirlinesMongo.Configuracao
{
    public class InsereLogMongo : IInsereLogMongo
    {
        public string LogCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

namespace ApiAndreAirlinesMongo.Configuracao
{
    public interface IInsereLogMongo
    {
        string LogCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

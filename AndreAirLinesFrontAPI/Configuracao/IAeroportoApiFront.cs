namespace AndreAirLinesFrontAPI.Configuracao
{
    public interface IAeroportoApiFront
    {
        string AeroportoCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

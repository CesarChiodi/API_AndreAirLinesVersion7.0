namespace AndreAirLinesFrontAPI.Configuracao
{
    public class AeroportoApiFront : IAeroportoApiFront
    {
        public string AeroportoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

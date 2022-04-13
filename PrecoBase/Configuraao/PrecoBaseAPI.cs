namespace MicroServicoPrecoBaseAPI.Configuraao
{
    public class PrecoBaseAPI : IPrecoBaseAPI
    {
        public string PrecoBaseCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}

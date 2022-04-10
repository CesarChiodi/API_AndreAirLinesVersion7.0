namespace MicroServicoUsuarioAPI.Configuracao
{
    public class UsuarioAPI : IUsuarioAPI
    {

        public string UsuarioCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
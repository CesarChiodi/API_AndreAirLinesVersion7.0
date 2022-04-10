namespace MicroServicoUsuarioAPI.Configuracao
{
    public interface IUsuarioAPI
    {
        string UsuarioCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

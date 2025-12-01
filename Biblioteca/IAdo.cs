namespace Biblioteca;

public interface IAdo
{
    void AltaUsuario(Usuario usuario);
    Task AltaUsuarioAsync(Usuario usuario);
    void AltaCasa(Casa casa);
    Task AltaCasaAsync(Casa casa);
    void AltaConsumo(Consumo consumo);
    Task AltaConsumoAsync(Consumo consumo);
    void AltaHistorialRegistro(HistorialRegistro historialRegistro);
    Task AltaHistorialRegistroAsync(HistorialRegistro historialRegistro);
    void AltaElectrodomestico(Electrodomestico electrodomestico);
    Task AltaElectrodomesticoAsync(Electrodomestico electrodomestico);
    Electrodomestico? ObtenerElectrodomestico(int IdElectrodomestico);
    Task<Electrodomestico?> ObtenerElectrodomesticoAsync(int idElectrodomestico);
    Task<Electrodomestico?> EliminarElectrodomesticoAsync(int idElectrodomestico);
    Task<IEnumerable<Electrodomestico>> ObtenerTodosLosElectrodomesticosAsync();
    Task<Electrodomestico?> ObtenerElectroDetalleAsync(int idElectrodomestico);
    Task CambiarEstadoElectrodomesticoAsync(int id);
    Task<IEnumerable<Electrodomestico>> ObtenerElectrodomesticosPorUsuarioAsync(int idUsuario);
    Casa? ObtenerCasa(int IdCasa);
    Task<Casa?> ObtenerCasaAsync(int IdCasa);
    Task EliminarCasaAsync(int idCasa);
    Task<IEnumerable<Casa>> ObtenerTodasLasCasasAsync();
    Task<Casa?> ObtenerCasaDetalleAsync(int idCasa);
    Task<IEnumerable<Casa>> ObtenerCasasPorUsuarioAsync(int idUsuario);
    Usuario? UsuarioPorPass(string Correo, string Contrasenia);
    Task<Usuario?> UsuarioPorPassAsync(string Correo, string Contrasenia);
    Task<IEnumerable<Consumo>> ObtenerTodosLosConsumosAsync();
    Task<Consumo?> ObtenerConsumoAsync(int idConsumo);
    Task EliminarConsumoAsync(int idConsumo);
    Task<Consumo?> ObtenerConsumoConElectrodomesticoAsync(int idConsumo);
    Task<IEnumerable<Consumo>> ObtenerConsumosPorUsuarioAsync(int idUsuario);
    Task AltaCasaYAsociarUsuarioAsync(Casa casa, int idUsuario);
}

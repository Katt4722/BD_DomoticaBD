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
    Casa? ObtenerCasa(int IdCasa);
    Task<Casa?> ObtenerCasaAsync(int IdCasa);
    Task EliminarCasaAsync(int idCasa);
    Task<IEnumerable<Casa>> ObtenerTodasLasCasasAsync();
    Usuario? UsuarioPorPass(string Correo, string Contrasenia);
    Task<Usuario?> UsuarioPorPassAsync(string Correo, string Contrasenia);
    Task<IEnumerable<Consumo>> ObtenerTodosLosConsumosAsync();
    Task<Consumo?> ObtenerConsumoAsync(int idConsumo);
    Task EliminarConsumoAsync(int idConsumo);
}

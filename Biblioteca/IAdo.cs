namespace Biblioteca;

public interface IAdo
{
    void AltaUsuario(Usuario usuario);
    Task AltaUsuarioAsync(Usuario usuario); 
    void AltaCasa (Casa casa);
    Task AltaCasaAsync(Casa casa);
    void AltaConsumo(Consumo consumo);
    void AltaHistorialRegistro(HistorialRegistro historialRegistro);
    Task AltaHistorialRegistroAsync(HistorialRegistro historialRegistro);
    void AltaElectrodomestico(Electrodomestico electrodomestico);
    Task AltaElectrodomesticoAsync(Electrodomestico electrodomestico);
    Electrodomestico? ObtenerElectrodomestico (int IdElectrodomestico);
    Casa? ObtenerCasa (int IdCasa);
    Usuario? UsuarioPorPass (string Correo, string Contrasenia);
}

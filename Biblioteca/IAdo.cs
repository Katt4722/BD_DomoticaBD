namespace Biblioteca;

public interface IAdo
{
    void AltaUsuario(Usuario usuario);
    void AltaCasa (Casa casa);
    void AltaConsumo (Consumo consumo);
    void AltaHistorialRegistro (HistorialRegistro historialRegistro);
    void AltaElectrodomestico (Electrodomestico electrodomestico);
    Electrodomestico? ObtenerElectrodomestico (int IdElectrodomestico);
    Casa? ObtenerCasa (int IdCasa);
}

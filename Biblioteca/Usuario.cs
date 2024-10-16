namespace Biblioteca;
public class Usuario
{
    public int IdUsuario {get; set;}
    public required string Nombre {get; set;}
    public required string Correo {get; set;}
    public required string Contrasenia {get; set;}
    public required string Telefono {get; set;}
    public List<Casa> ListadoCasas { get; set; } = [];
}

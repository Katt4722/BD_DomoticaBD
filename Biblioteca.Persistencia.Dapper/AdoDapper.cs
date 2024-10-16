using System.Data;
using Dapper;

namespace Biblioteca.Persistencia.Dapper;
public class AdoDapper : IAdo
{
    IDbConnection Conexion;
    
    public AdoDapper(IDbConnection conexion)
        => Conexion = conexion;

    public void AltaUsuario(Usuario usuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidUsuario", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", usuario.Nombre);
        parametros.Add("@unCorreo", usuario.Correo);
        parametros.Add("@uncontrasenia",usuario.Contrasenia);
        parametros.Add("@unTelefono",usuario.Telefono);

        Conexion.Execute("altaUsuario", parametros);

        usuario.IdUsuario = parametros.Get<int>("@unidUsuario");
    }

    public void AltaCasa (Casa casa)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidCasa", direction: ParameterDirection.Output);
        parametros.Add("@unDireccion", casa.Direccion);

        Conexion.Execute("altaCasa", parametros); // Carga el sp y los parametros desde dapper.

        casa.IdCasa = parametros.Get<int>("@unidCasa");
    }

    public void AltaElectrodomestico (Electrodomestico electrodomestico)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidElectrodomestico", direction: ParameterDirection.Output);
        parametros.Add("@unidCasa", electrodomestico.IdCasa);
        parametros.Add("@unNombre", electrodomestico.Nombre);
        parametros.Add("@unTipo", electrodomestico.Tipo);
        parametros.Add("@unUbicacion", electrodomestico.Ubicacion);
        parametros.Add("@unEncendido", electrodomestico.Encendido);
        parametros.Add("@unApagado", electrodomestico.Apagado);

        Conexion.Execute("altaElectrodomestico", parametros);

        electrodomestico.IdElectrodomestico = parametros.Get<int>("@unidElectrodomestico");
    }

    public void AltaHistorialRegistro (HistorialRegistro historialRegistro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidElectrodomestico", historialRegistro.IdElectrodomestico);
        parametros.Add("@unFechaHoraRegistro", historialRegistro.FechaHoraRegistro);

        Conexion.Execute("altaHistorialRegistro", parametros , commandType: CommandType.StoredProcedure);

    }

    public void AltaConsumo (Consumo consumo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidConsumo", direction: ParameterDirection.Output);
        parametros.Add("@unidElectrodomestico", consumo.IdElectrodomestico);
        parametros.Add("@uninicio", consumo.Inicio);
        parametros.Add("@unDuracion", consumo.Duracion);
        parametros.Add("@unConsumoTotal", consumo.ConsumoTotal);

        Conexion.Execute("altaConsumo", parametros);

        consumo.IdConsumo = parametros.Get<int>("@unidConsumo");
    }
}

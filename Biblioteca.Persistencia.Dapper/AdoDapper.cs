using System.Collections.Immutable;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Biblioteca.Persistencia.Dapper;

public class AdoDapper : IAdo
{
    readonly IDbConnection _conexion;
    private readonly string _queryElectrodomestico
    = @"SELECT  *
    FROM    Electrodomestico
    WHERE   idElectrodomestico = @id;

    SELECT  *
    FROM    HistorialRegistro
    WHERE   idElectrodomestico = @id;";

    private readonly string _queryElectrodomesticos
    = @"SELECT * FROM Electrodomestico;
      SELECT * FROM HistorialRegistro;";

    private readonly string _queryElectroDetalle
    = @"SELECT  *
        FROM    Electrodomestico
        WHERE   idElectrodomestico = @id;

        SELECT  *
        FROM    Consumo
        WHERE   idElectrodomestico = @id;
        ORDER BY Inicio DESC";
    private readonly string _queryCasa
    = @"SELECT *
        FROM Casa
        WHERE idCasa = @id;
        
        SELECT  *
        FROM    Electrodomestico
        WHERE   idCasa = @id;";

    private readonly string _queryCasas
    = @"SELECT * FROM Casa;
      SELECT * FROM Electrodomestico;";

    private readonly string _deleteCasaQuery
    = @"DELETE FROM Casa 
        WHERE idCasa = @id;";

    private readonly string _queryCasaDetalle
    = @"SELECT *
        FROM Casa
        WHERE idCasa = @id;
        
        SELECT * 
        FROM Electrodomestico 
        WHERE idCasa = @id;";

    private readonly string _deleteElectrodomesticoQuery
    = @"DELETE FROM Electrodomestico 
        WHERE idElectrodomestico = @id;";

    private readonly string _queryUsuario
    = @"SELECT idUsuario AS IdUsuario,
            Nombre,
            Correo,
            contrasenia AS Contrasenia,
            Telefono
        FROM Usuario
        WHERE Correo = @correo AND Contrasenia = SHA2(@contrasenia, 256);";


    private readonly string _queryConsumo
    = @"SELECT * FROM Consumo
        WHERE IdConsumo = @id;";
    private readonly string _queryConsumos
    = @"SELECT * 
        FROM Consumo;";
    private readonly string _deleteConsumoQuery
    = @"DELETE FROM Consumo 
        WHERE IdConsumo = @id;";
    public AdoDapper(IDbConnection conexion)
    => _conexion = conexion;

    public void AltaUsuario(Usuario usuario)
    {
        var parametros = ParametrosAlta(usuario);

        _conexion.Execute("altaUsuario", parametros);
        usuario.IdUsuario = parametros.Get<int>("@unidUsuario");
    }

    private static DynamicParameters ParametrosAlta(Usuario usuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidUsuario", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", usuario.Nombre);
        parametros.Add("@unCorreo", usuario.Correo);
        parametros.Add("@uncontrasenia", usuario.Contrasenia);
        parametros.Add("@unTelefono", usuario.Telefono);
        return parametros;
    }

    public async Task AltaUsuarioAsync(Usuario usuario)
    {
        var parametros = ParametrosAlta(usuario);
        await _conexion.ExecuteAsync("altaUsuario", parametros);
        usuario.IdUsuario = parametros.Get<int>("@unidUsuario");
    }

    public void AltaCasa(Casa casa)
    {
        var parametros = AltaParametros(casa);

        _conexion.Execute("altaCasa", parametros); // Carga el sp y los parametros desde dapper.
        casa.IdCasa = parametros.Get<int>("@unidCasa");
    }

    private static DynamicParameters AltaParametros(Casa casa)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidCasa", direction: ParameterDirection.Output);
        parametros.Add("@unDireccion", casa.Direccion);
        return parametros;
    }

    public async Task AltaCasaAsync(Casa casa)
    {
        var parametros = AltaParametros(casa);
        await _conexion.ExecuteAsync("altaCasa", parametros);
        casa.IdCasa = parametros.Get<int>("@unidCasa");
    }


    public void AltaElectrodomestico(Electrodomestico electrodomestico)
    {
        var parametros = ParametrosAltaElectro(electrodomestico);
        _conexion.Execute("altaElectrodomestico", parametros);
        electrodomestico.IdElectrodomestico = parametros.Get<int>("@unidElectrodomestico");
    }

    private static DynamicParameters ParametrosAltaElectro(Electrodomestico electrodomestico)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidElectrodomestico", direction: ParameterDirection.Output);
        parametros.Add("@unidCasa", electrodomestico.IdCasa);
        parametros.Add("@unNombre", electrodomestico.Nombre);
        parametros.Add("@unTipo", electrodomestico.Tipo);
        parametros.Add("@unUbicacion", electrodomestico.Ubicacion);
        parametros.Add("@unEncendido", electrodomestico.Encendido);
        parametros.Add("@unApagado", electrodomestico.Apagado);
        return parametros;
    }

    public async Task AltaElectrodomesticoAsync(Electrodomestico electrodomestico)
    {
        var parametros = ParametrosAltaElectro(electrodomestico);
        await _conexion.ExecuteAsync("altaElectrodomestico", parametros);
        electrodomestico.IdElectrodomestico = parametros.Get<int>("@unidElectrodomestico");
    }


    public void AltaHistorialRegistro(HistorialRegistro historialRegistro)
    {
        var parametros = ParametrosAltaHistorial(historialRegistro);
        _conexion.Execute("altaHistorialRegistro", parametros, commandType: CommandType.StoredProcedure);
    }

    private static DynamicParameters ParametrosAltaHistorial(HistorialRegistro historialRegistro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidElectrodomestico", historialRegistro.IdElectrodomestico);
        parametros.Add("@unFechaHoraRegistro", historialRegistro.FechaHoraRegistro);
        return parametros;
    }

    public async Task AltaHistorialRegistroAsync(HistorialRegistro historialRegistro)
    {
        var parametros = ParametrosAltaHistorial(historialRegistro);
        await _conexion.ExecuteAsync("altaHistorialregistro", parametros);
    }

    public void AltaConsumo(Consumo consumo)
    {
        var parametros = ParametrosAltaConsumo(consumo);
        _conexion.Execute("altaConsumo", parametros);
    }

    private static DynamicParameters ParametrosAltaConsumo(Consumo consumo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidConsumo", direction: ParameterDirection.Output);
        parametros.Add("@unidElectrodomestico", consumo.IdElectrodomestico);
        parametros.Add("@uninicio", consumo.Inicio);
        parametros.Add("@unDuracion", consumo.Duracion);
        parametros.Add("@unConsumoTotal", consumo.ConsumoTotal);

        // consumo.IdConsumo = parametros.Get<int>("@unidConsumo");
        return parametros;
    }
    public async Task AltaConsumoAsync(Consumo consumo)
    {
        var parametros = ParametrosAltaConsumo(consumo);
        await _conexion.ExecuteAsync("altaConsumo", parametros);
        consumo.IdConsumo = parametros.Get<int>("@unidConsumo");
    }

    // query de Electrodomestico
    public Electrodomestico? ObtenerElectrodomestico(int idElectrodomestico)
    {
        using (var registro = _conexion.QueryMultiple(_queryElectrodomestico, new { id = idElectrodomestico }))
        {
            var electrodomestico = registro.ReadSingleOrDefault<Electrodomestico>();
            if (electrodomestico is not null)
            {
                electrodomestico.ConsumoMensual = registro.Read<HistorialRegistro>().ToList();
            }
            return electrodomestico;
        }
    }

    public async Task<Electrodomestico?> ObtenerElectrodomesticoAsync(int idElectrodomestico)
    {
        using (var registro = await _conexion.QueryMultipleAsync(_queryElectrodomestico, new { id = idElectrodomestico }))
        {
            var electrodomestico = await registro.ReadSingleOrDefaultAsync<Electrodomestico>();

            if (electrodomestico is not null)
            {
                electrodomestico.ConsumoMensual = (await registro.ReadAsync<HistorialRegistro>()).ToList();
            }
            return electrodomestico;
        }
    }

    public async Task<Electrodomestico?> EliminarElectrodomesticoAsync(int idElectrodomestico)
    {
        await _conexion.ExecuteAsync("DELETE FROM HistorialRegistro WHERE idElectrodomestico = @id", new { id = idElectrodomestico });
        await _conexion.ExecuteAsync(_deleteElectrodomesticoQuery, new { id = idElectrodomestico });
        return null;
    }
    
    public async Task<Electrodomestico?> ObtenerElectroDetalleAsync(int idElectrodomestico)
    {
        using (var registro = await _conexion.QueryMultipleAsync(_queryElectroDetalle, new { id = idElectrodomestico }))
        {
            var electrodomestico = await registro.ReadSingleOrDefaultAsync<Electrodomestico>();
            if (electrodomestico is not null)
            {
                electrodomestico.ConsumoMensual = (await registro.ReadAsync<HistorialRegistro>()).ToList();
            }
            return electrodomestico;
        }
    }

    public async Task<IEnumerable<Electrodomestico>> ObtenerTodosLosElectrodomesticosAsync()
    {
        using (var registro = await _conexion.QueryMultipleAsync(_queryElectrodomesticos))
        {
            var electros = (await registro.ReadAsync<Electrodomestico>()).ToList();
            var historiales = (await registro.ReadAsync<HistorialRegistro>()).ToList();

            // Asocia los historiales a cada electrodoméstico
            foreach (var electro in electros)
            {
                electro.ConsumoMensual = historiales
                    .Where(h => h.IdElectrodomestico == electro.IdElectrodomestico)
                    .ToList();
            }

            return electros;
        }
    }

    // query de Casa
    public Casa? ObtenerCasa(int idCasa)
    {
        using (var registro = _conexion.QueryMultiple(_queryCasa, new { id = idCasa }))
        {
            var casa = registro.ReadSingleOrDefault<Casa>();
            if (casa is not null)
            {
                casa.Electros = registro.Read<Electrodomestico>();
            }
            return casa;
        }
    }
    public async Task<Casa?> ObtenerCasaAsync(int idCasa)
    {
        using (var registro = await _conexion.QueryMultipleAsync(_queryCasa, new { id = idCasa }))
        {
            var casa = await registro.ReadSingleOrDefaultAsync<Casa>();
            if (casa is not null)
            {
                casa.Electros = (await registro.ReadAsync<Electrodomestico>()).ToList();
            }
            return casa;
        }
    }
    public async Task EliminarCasaAsync(int idCasa)
    {
        await _conexion.ExecuteAsync(_deleteCasaQuery, new { id = idCasa });
    }

    public async Task<IEnumerable<Casa>> ObtenerTodasLasCasasAsync()
    {
        using (var registro = await _conexion.QueryMultipleAsync(_queryCasas))
        {
            var casas = (await registro.ReadAsync<Casa>()).ToList();
            var electros = (await registro.ReadAsync<Electrodomestico>()).ToList();

            foreach (var casa in casas)
            {
                casa.Electros = electros.Where(e => e.IdCasa == casa.IdCasa).ToList();
            }
            return casas;
        }
    }

    public async Task<Casa?> ObtenerCasaDetalleAsync(int idCasa)
    {
        using (var registro = await _conexion.QueryMultipleAsync(_queryCasaDetalle, new { id = idCasa }))
        {
            var casa = await registro.ReadSingleOrDefaultAsync<Casa>();
            if (casa is not null)
            {
                casa.Electros = (await registro.ReadAsync<Electrodomestico>()).ToList();
            }
            return casa;
        }
    }

    // query de Usuario
    public Usuario? UsuarioPorPass(string Correo, string Contrasenia)
    {
        return _conexion.QueryFirstOrDefault<Usuario>(_queryUsuario, new { correo = Correo, contrasenia = Contrasenia });
    }

    public async Task<Usuario?> UsuarioPorPassAsync(string Correo, string Contrasenia)
    {
        return await _conexion.QueryFirstOrDefaultAsync<Usuario>(_queryUsuario, new { correo = Correo, contrasenia = Contrasenia });

    }

    // query de Consumo
    public async Task<IEnumerable<Consumo>> ObtenerTodosLosConsumosAsync()
    {
        return await _conexion.QueryAsync<Consumo>(_queryConsumos);
    }

    public async Task<Consumo?> ObtenerConsumoAsync(int idConsumo)
    {
        return await _conexion.QueryFirstOrDefaultAsync<Consumo>(_queryConsumo, new { id = idConsumo });
    }
    public async Task EliminarConsumoAsync(int idConsumo)
    {
        await _conexion.ExecuteAsync(_deleteConsumoQuery, new { id = idConsumo });
    }
}


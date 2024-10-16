using MySqlConnector;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Biblioteca.Persistencia.Dapper.Test;
public class TestBase
{
    private readonly IDbConnection Conexion;
    protected readonly IAdo Ado;

    public TestBase()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
            .Build();
        string cadena = config.GetConnectionString("MySQL")!;
        Conexion = new MySqlConnection(cadena);

        Ado = new AdoDapper(Conexion);
    }
}

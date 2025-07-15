using System.Threading.Tasks;

namespace Biblioteca.Persistencia.Dapper.Test;

public class CasaTest : TestBase
{
    public CasaTest() : base() { }

    [Fact]
    public void AltaCasaOK()
    {
        var CasaRetiro = new Casa()
        {
            Direccion = "Colibri 111"
        };

        Ado.AltaCasa(CasaRetiro);

        Assert.NotEqual(0, CasaRetiro.IdCasa);
    }

    [Fact]
    public void ObtenerCasaOK()
    {
        var Casa =  Ado.ObtenerCasa(2);

        Assert.NotNull(Casa);
        Assert.Equal(2, Casa.IdCasa);
        Assert.Equal("Libertador 284", Casa.Direccion);
    }
    
    [Fact]
    public async Task ObtenerCasaOkAsync()
    {
        var Casa = await Ado.ObtenerCasaAsync(2);

        Assert.NotNull(Casa);
        Assert.Equal(2, Casa.IdCasa);
        Assert.Equal("Libertador 284", Casa.Direccion);
    }
}

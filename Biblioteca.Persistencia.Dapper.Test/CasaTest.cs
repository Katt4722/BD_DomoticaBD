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
        var CasaBarracas = new Casa()
        {
            Direccion = "Barracas 1789"
        };
        Ado.AltaCasa(CasaBarracas);
        Assert.NotEqual(0, CasaBarracas.IdCasa);
        
    }
}
//hola :V
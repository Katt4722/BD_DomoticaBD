namespace Biblioteca.Persistencia.Dapper.Test;

public class ElectrodomesticoTest : TestBase
{
    public ElectrodomesticoTest() : base() { }

    [Fact]
    public void AltaElectrodomesticoOK ()
    {
        var Lavarropa = new Electrodomestico()
        {
            IdCasa = 1,
            Nombre = "AGH123",
            Tipo = "Lavaropa",
            Ubicacion = "Lavanderia",
            Encendido = false,
            Apagado = true
        };
        Ado.AltaElectrodomestico(Lavarropa);

        Assert.NotEqual(0, Lavarropa.IdElectrodomestico);
        
    }

    [Fact]
    public void ObtenerElectrodomesticoOK()
    {
        var lavarropa = Ado.ObtenerElectrodomestico(1);  
        
        Assert.NotNull(lavarropa);
        Assert.Equal("Lavarropa", lavarropa.Nombre);
        Assert.Equal(1, lavarropa.IdElectrodomestico);
        Assert.NotEmpty(lavarropa.ConsumoMensual);
    }
}

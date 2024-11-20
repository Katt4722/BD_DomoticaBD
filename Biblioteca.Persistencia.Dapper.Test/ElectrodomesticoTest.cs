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
        var Electrodomestico = Ado.ObtenerElectrodomestico(4);  
        
        Assert.NotNull(Electrodomestico);
        Assert.Equal(2, Electrodomestico.IdElectrodomestico);

    }
}

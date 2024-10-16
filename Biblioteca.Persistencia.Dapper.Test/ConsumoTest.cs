namespace Biblioteca.Persistencia.Dapper.Test;

public class ConsumoTest : TestBase
{
    public ConsumoTest() : base() { }

    [Fact]
    public void AltaConsumoOK ()
    {
        
        var Lunes = new Consumo ()
        {
            Inicio = DateTime.Now,
            Duracion = new TimeSpan(2, 0, 0), 
            ConsumoTotal = 9.3F,
            IdElectrodomestico = 1,
        };

        Ado.AltaConsumo(Lunes);

        Assert.NotEqual(0, Lunes.IdConsumo);
    }
}
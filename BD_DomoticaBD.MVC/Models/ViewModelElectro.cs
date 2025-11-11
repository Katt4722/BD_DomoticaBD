using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteca;

namespace BD_DomoticaBD.MVC.ViewModels
{
    public class ViewModelElectrodomestico

    {
        public SelectList? DireccionesList { get; set; }
        public Electrodomestico Electrodomestico { get; set; }
        public int? IdCasaSeleccionado { get; set; }
        public bool Encendido { get; set; }
        public  ViewModelElectrodomestico() { }
        public ViewModelElectrodomestico(IEnumerable<Casa> casas, Electrodomestico? electrodomestico = null)
        {
            DireccionesList = new SelectList(casas, nameof(Casa.IdCasa), nameof(Casa.Direccion));
            if (electrodomestico is null)
            {
                Electrodomestico = new Electrodomestico
                {
                    Nombre = "",
                    Tipo = "",
                    Ubicacion = "",
                    Encendido = false
                };
                IdCasaSeleccionado = 0;
            }
            else
            {
                Electrodomestico = electrodomestico;
                IdCasaSeleccionado = electrodomestico.IdCasa;
            }
        }

    }
}

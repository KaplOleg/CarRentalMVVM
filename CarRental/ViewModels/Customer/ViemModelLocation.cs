using CarRental.ViewModels.Base;
using System.Data.Entity.Spatial;

namespace CarRental.ViewModels.Customer
{
    public class ViemModelLocation : ViewModel
    {
        private static DbGeography locationCar;
        public static DbGeography LocationCar { get => locationCar; set => locationCar = value; }
    }
}

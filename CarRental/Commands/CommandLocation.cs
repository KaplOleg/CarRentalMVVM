using CarRental.Commands.Base;
using CarRental.ViewModels.Customer;
using CarRental.Views.Windows.Customer;
using System.Data.Entity.Spatial;

namespace CarRental.Commands
{
    public class CommandLocation : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            ViemModelLocation.LocationCar = parameter as DbGeography;
            WindowLocation windowLocation = new WindowLocation();
            windowLocation.Show();
        }
    }
}

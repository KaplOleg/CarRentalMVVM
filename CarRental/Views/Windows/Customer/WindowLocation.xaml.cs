using CarRental.ViewModels;
using CarRental.ViewModels.Customer;
using System.Diagnostics;
using System.Windows;

namespace CarRental.Views.Windows.Customer
{
    public partial class WindowLocation : Window
    {
        public WindowLocation()
        {
            InitializeComponent();
        }

        private void hyperlink(object sender, RoutedEventArgs e) //Переход в браузер(Карты Google)
        {
            var LatitideUri = ViemModelLocation.LocationCar.Latitude.ToString().Replace(',', '.');
            var LongitudeUri = ViemModelLocation.LocationCar.Longitude.ToString().Replace(',','.');           
            Process.Start($"https://www.google.ru/maps/dir//{LatitideUri},{LongitudeUri}/@{LatitideUri},{LongitudeUri},18.26z");
        }
    }
}

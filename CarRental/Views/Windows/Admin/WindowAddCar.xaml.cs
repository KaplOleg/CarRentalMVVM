using System;
using System.Diagnostics;
using System.Windows;

namespace CarRental.Views.Windows.Admin
{
    public partial class WindowAddCar : Window
    {
        public WindowAddCar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.google.ru/maps/@55.7517972,37.6239844,15.08z");
        }

        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0))
            {
                e.Handled = false;
            }
            else if (e.Text == "." || e.Text == ",") 
            {
                e.Handled = false;
            } 
            else
            {
                e.Handled = true;
            }
        }
    }
}

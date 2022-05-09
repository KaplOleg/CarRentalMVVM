using CarRental.Commands.Base;
using CarRental.Views.Windows.Customer;

namespace CarRental.Commands
{
    public class CommandRentalCar : Command
    {
        public override bool CanExecute(object parameter)
        {
            if(parameter is null)
            {
                return true;
            }

            var parameters = (object[])parameter;

            if (parameters[0] == null) //Проверка на SelectedItem
            {
                return false;
            }
            else
            {       
                if (parameters[1].ToString() == "Занят") return false;  //Проверка на CarStatus
                return true;
            }
        }

        public override void Execute(object parameter)
        {
                WindowRental windowRental = new WindowRental();
                windowRental.Show();
        }
    }
}

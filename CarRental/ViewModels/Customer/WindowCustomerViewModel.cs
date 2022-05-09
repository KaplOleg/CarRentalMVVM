using CarRental.Commands.Base;
using CarRental.Model;
using CarRental.ViewModels.Base;
using CarRental.Views.Pages.Customer;
using CarRental.Views.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarRental.ViewModels.Customer
{
    public class WindowCustomerViewModel : ViewModel
    {
        #region login(FullName Customer)
        private static string FullNameCustomer;
        public static string loginCustomer;
        public static string LoginCustomer
        {
            get => FullNameCustomer;
            set 
            {
                using (CarRentalEntities db = new CarRentalEntities())
                {
                    var customer = (from sqlCustomer in db.Сustomers.ToList()
                                    where sqlCustomer.CustomerLogin == value
                                    select sqlCustomer).FirstOrDefault();

                    if(customer != null)
                    {
                        if (string.IsNullOrWhiteSpace(customer.Firstname) || string.IsNullOrWhiteSpace(customer.Surname))
                        {
                            FullNameCustomer = "Пользователь";
                        }
                        else
                        {
                            FullNameCustomer = customer.Firstname + " " + customer.Lastname;

                        }
                        loginCustomer = customer.CustomerLogin;
                    }
                }
            }
        }
        #endregion

        #region Страницы
        private Page _currentPage;
        public Page CurrentPage { get => _currentPage; set => Set(ref _currentPage, value); }
        #endregion

        #region Команды
        public Command ExitCommand { get; }
        private bool CanExitCommandExecute(object parameters) => true;
        private void OnExitCommandExecuted(object parameters)
        {
            LoginWindow loginWindow = new LoginWindow();
            (parameters as Window).Close();
            loginWindow.Show();
        }

        public Command SelectRentalCarCommand { get; }
        private bool CanSelectRentalCarCommandExecute(object parameters) => true;
        private void OnSelectRentalCarCommandExecuted(object parameters) => CurrentPage = new PageRentalCar();

        public Command SelectContactsCommand { get; }
        private bool CanSelectContactsCommandExecute(object parameters) => true;
        private void OnSelectContactsCommandExecuted(object parameters) => CurrentPage = new PageContacts();

        public Command SelectOrderDocumentsCommand { get; }
        private bool CanSelectOrderDocumentsCommandExecute(object parameters) => true;
        private void OnSelectOrderDocumentsCommandExecuted(object parameters) => CurrentPage = new PageOrderDocument();
        #endregion

        public WindowCustomerViewModel()
        {
            #region Комманды
            ExitCommand = new LambdaCommand(OnExitCommandExecuted, CanExitCommandExecute);
            SelectRentalCarCommand = new LambdaCommand(OnSelectRentalCarCommandExecuted, CanSelectRentalCarCommandExecute);
            SelectContactsCommand = new LambdaCommand(OnSelectContactsCommandExecuted, CanSelectContactsCommandExecute);
            SelectOrderDocumentsCommand = new LambdaCommand(OnSelectOrderDocumentsCommandExecuted, CanSelectOrderDocumentsCommandExecute);
            #endregion
        }
    }
}

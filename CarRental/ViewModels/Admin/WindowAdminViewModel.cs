using CarRental.Commands.Base;
using CarRental.Model;
using CarRental.ViewModels.Base;
using CarRental.Views.Pages;
using CarRental.Views.Pages.Admin;
using CarRental.Views.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarRental.ViewModels.Admin
{
    public class WindowAdminViewModel : ViewModel
    {
        #region login(FullName admin)
        public static string loginAdmin;
        public static string LoginAdmin
        {
            set
            {
                using (CarRentalEntities db = new CarRentalEntities())
                {
                    var admin = (from sqlAdmin in db.Admins.ToList()
                                    where sqlAdmin.AdminLogin == value
                                    select sqlAdmin).FirstOrDefault();

                    loginAdmin = admin.AdminLogin;
                }
            }
        }
        #endregion

        #region Страницы
        private readonly Page pageWorkTable = new PageWorkTable();
        private readonly Page pageOrders = new PageOrders();

        private Page _currentPage;
        public Page CurrentPage { get => _currentPage; set => Set(ref _currentPage, value); }
        #endregion

        #region Команды
        public Command ExitCommand { get; }
        private bool CanExitCommandExecute(object p) => true;
        private void OnExitCommandExecuted(object p)
        {
            LoginWindow loginWindow = new LoginWindow();
            (p as Window).Close();
            loginWindow.Show();
        }

        public Command SelectWorkTableCommand { get; }
        private bool CanSelectWorkTableCommandExecute(object p) => true;
        private void OnSelectWorkTableCommandExecuted(object p) => CurrentPage = pageWorkTable;

        public Command SelectOrdersCommand { get; }
        private bool CanSelectOrdersCommandExecute(object p) => true;
        private void OnSelectOrdersCommandExecuted(object p) => CurrentPage = pageOrders;
        #endregion

        public WindowAdminViewModel()
        {
            ExitCommand = new LambdaCommand(OnExitCommandExecuted, CanExitCommandExecute);
            SelectWorkTableCommand = new LambdaCommand(OnSelectWorkTableCommandExecuted, CanSelectWorkTableCommandExecute);
            SelectOrdersCommand = new LambdaCommand(OnSelectOrdersCommandExecuted, CanSelectOrdersCommandExecute);
        }
    }
}

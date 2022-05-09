using CarRental.Commands.Base;
using CarRental.Model;
using CarRental.ViewModels.Admin;
using CarRental.ViewModels.Customer;
using CarRental.Views.Windows.Customer;
using CarRental.Windows.Admin;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CarRental.ViewModels.LoginAndRegistration
{
    public class LoginWindowViewModel
    {
        #region Окна
        private readonly MainWindowAdmin windowAdmin = new MainWindowAdmin();
        private readonly MainWindowCustomer windowCustomer = new MainWindowCustomer();
        #endregion

        #region Проверки на пустые Поля
        private bool CheckInputLogin(TextBox login, PasswordBox password, TextBlock errorMessage)
        {
            errorMessage.Text = "";

            if (string.IsNullOrWhiteSpace(login.Text))
            {
                errorMessage.Text += "Введите логин";
                return false;
            }
            else if (string.IsNullOrWhiteSpace(password.Password))
            {
                errorMessage.Text += "Введите пароль";
                return false;
            }
            return true;
        }

        private bool CheckInputRegistration(TextBox login, PasswordBox password, PasswordBox checkPassword, TextBlock errorMessage)
        {
            if (CheckInputLogin(login, password, errorMessage))
            {
                if (string.IsNullOrWhiteSpace(checkPassword.Password) && string.IsNullOrEmpty(errorMessage.Text))
                {
                    errorMessage.Text = "";
                    errorMessage.Text += "Введите повторно пароль";
                    return false;
                }
                return true;
            }
            return false;
        }
        #endregion

        #region Вход
        public ICommand LoginCommand { get; }

        public bool CanLoginCommandExecute(object parameters) => true;
        public void OnLoginCommandExecuted(object parameters)
        {
            #region Параметры
            var param = (object[])parameters;

            var role = (ComboBox)param[0];
            var login = (TextBox)param[1];
            var password = (PasswordBox)param[2];
            var errorMessage = (TextBlock)param[3];
            var mainWindow = (Window)param[4];
            #endregion

            #region Вход в главное меню
            using (CarRentalEntities db = new CarRentalEntities())
            {
                if(role.Text == "Администратор")
                {
                    if(CheckInputLogin(login, password, errorMessage)) 
                    {
                        var sql = (from admin in db.Admins.ToList()
                                   where admin.AdminLogin == login.Text && admin.AdminPassword == password.Password
                                   select admin).FirstOrDefault();
                        if(sql != null)
                        {
                            WindowAdminViewModel.loginAdmin = login.Text;
                            windowAdmin.Show();
                            mainWindow.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неправильно введен логин или пароль!");
                        }
                    }   
                }
                else if(role.Text == "Пользователь")
                {
                    if (CheckInputLogin(login, password, errorMessage))
                    {
                        var sql = (from customer in db.Сustomers.ToList()
                                   where customer.CustomerLogin == login.Text && customer.CustomerPassword == password.Password
                                   select customer).FirstOrDefault();
                        if (sql != null)
                        {
                            WindowCustomerViewModel.LoginCustomer = login.Text;
                            windowCustomer.tbNameUser.Text = WindowCustomerViewModel.LoginCustomer;
                            windowCustomer.Show();
                            mainWindow.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неправильно введен логин или пароль!");
                        }
                    }
                }
                else
                {
                    errorMessage.Text = "Выберите роль для входа!";
                }
            }
            #endregion
        }
        #endregion

        #region Регистрация
        public ICommand RegistrationCommand { get; }
        public bool CanRegistrationCommandExecute(object parameters) => true;
        public void OnRegistrationCommandExecuted(object parameters)
        {
            #region Параметры
            var param = (object[])parameters;

            var role = (ComboBox)param[0];
            var login = (TextBox)param[1];
            var password = (PasswordBox)param[2];
            var checkPassword = (PasswordBox)param[3];
            var errorMessage = (TextBlock)param[4];
            #endregion

            #region Регистрация
            using (CarRentalEntities db = new CarRentalEntities())
            {
                #region Новый Админ и пользователь
                Admins admin = new Admins()
                {
                    AdminLogin = login.Text,
                    AdminPassword = password.Password
                };

                Сustomers customer = new Сustomers()
                {
                    CustomerLogin = login.Text,
                    CustomerPassword = password.Password
                };
                #endregion

                if (role.Text == "Администратор")
                {
                    if (CheckInputRegistration(login, password, checkPassword, errorMessage))
                    {
                        var sql = (from adminCheck in db.Admins.ToList()
                                   where adminCheck.AdminLogin == admin.AdminLogin
                                   select adminCheck).FirstOrDefault();

                        if(sql is null)
                        {
                            if (checkPassword.Password == admin.AdminPassword)
                            {
                                db.Admins.Add(admin);
                                db.SaveChanges();
                                MessageBox.Show("Регистрация прошла успешна");
                            }
                            else
                            {
                                MessageBox.Show("Регистрация прошла неуспешно");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с данным логином уже существует!");
                        }

                    }
                }
                else if (role.Text == "Пользователь") 
                {
                    if (CheckInputRegistration(login, password, checkPassword, errorMessage))
                    {
                        var sql = (from customCheck in db.Сustomers.ToList()
                                   where customCheck.CustomerLogin == customer.CustomerLogin
                                   select customCheck).FirstOrDefault();

                        if (sql is null)
                        {
                            if (checkPassword.Password == customer.CustomerPassword)
                            {
                                db.Сustomers.Add(customer);
                                db.SaveChanges();
                                MessageBox.Show("Регистрация прошла успешна", "", MessageBoxButton.OK);
                            }
                            else
                            {
                                MessageBox.Show("Регистрация прошла неуспешно", "Ошибка", MessageBoxButton.OK);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с данным логином уже существует!");
                        }
                    }
                }
                else
                {
                    errorMessage.Text = "Выберите роль для регистрации!";
                }
            }
            #endregion
        }
        #endregion

        #region Команда закрытия приложения
        public ICommand CloseCommand { get; }
        public bool CanCloseCommandExecute(object parameters) => true;
        public void OnCloseCommandExecuted(object parameters) => Application.Current.Shutdown();
        #endregion

        public LoginWindowViewModel()
        {
            #region Комманды
            LoginCommand = new LambdaCommand(OnLoginCommandExecuted, CanLoginCommandExecute);
            CloseCommand = new LambdaCommand(OnCloseCommandExecuted, CanCloseCommandExecute);
            RegistrationCommand = new LambdaCommand(OnRegistrationCommandExecuted, CanRegistrationCommandExecute);
            #endregion
        }
    }
}

using CarRental.Commands.Base;
using CarRental.Model;
using CarRental.Views.Windows.Customer;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CarRental.ViewModels.Customer
{
    public class PageOrderDocViewModel
    {
        #region Коллекции
        public List<Orders> CollectionOrdersCustomer { get; set; }

        public static Orders SelectedOrders { get; set; }

        public double? OrderAmountCustomer { get; set; }
        #endregion

        private void GetPriceOrders()
        {
            if (SelectedOrders != null)
            {
                CarRentalEntities db = new CarRentalEntities();
                var orderAmount = (from orders in db.OrderAmountCustomer
                                   where orders.Car_id == SelectedOrders.Car_id &&
                                           orders.DateOfIssue == SelectedOrders.DateOfIssue &&
                                           orders.DateReturn == SelectedOrders.DateReturn
                                   select orders.AmountOrder).FirstOrDefault(); //Сумма заказа
                OrderAmountCustomer =  orderAmount;
            }
        }

        #region Комманды
        public ICommand ClickBtnDocumentViewCommand { get; }
        private bool CanClickBtnDocumentViewCommandExecute(object p) //Проверка на возможность нажатия на кнопку
        {
            if (p is null) return false;
            return true;
        }
        private void OnClickBtnDocumentViewCommandExecuted(object p) //Дествия которые выполняет кнопка при нажатие
        {
            var windowDocument = new WindowDocument();
            windowDocument.Show();
        }
        #endregion

        public PageOrderDocViewModel()
        {
            CarRentalEntities db = new CarRentalEntities();
            CollectionOrdersCustomer = db.Orders.ToList();
            int customer_id = (from customer in db.Сustomers.ToList()
                               where customer.CustomerLogin == WindowCustomerViewModel.loginCustomer
                               select customer.Customer_id).SingleOrDefault();
            var orders = CollectionOrdersCustomer.Where(t => t.Customer_id == customer_id).ToList();
            CollectionOrdersCustomer = orders;

            GetPriceOrders();

            ClickBtnDocumentViewCommand = new LambdaCommand(OnClickBtnDocumentViewCommandExecuted, CanClickBtnDocumentViewCommandExecute);
        }
    }
}

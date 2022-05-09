using CarRental.Commands.Base;
using CarRental.Model;
using CarRental.ViewModels;
using CarRental.ViewModels.Customer;
using System;
using System.Linq;
using System.Windows;

namespace CarRental.Commands
{
    public class CommandOrder : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            var dataPickers = (object[])parameter;
            var dataOfIssuePick = (DateTime)dataPickers[0]; //дата Заказа веденная пользователем
            var dataOfReturnPick = (DateTime)dataPickers[1]; //дата Возврата веденная пользователем

            using (CarRentalEntities db = new CarRentalEntities())
            {
                var sqlIdCustomer = (from sqlCustomerId in db.Сustomers.ToList()
                                     where sqlCustomerId.CustomerLogin == WindowCustomerViewModel.loginCustomer
                                     select sqlCustomerId).FirstOrDefault();     //Получае айди пользователя по его логину


                Orders newOrder = new Orders
                {
                    Customer_id = sqlIdCustomer.Customer_id,
                    Car_id = PageRentalCarViewModel.SelectedCars.Car_id,
                    DateOfIssue = dataOfIssuePick,
                    DateReturn = dataOfReturnPick
                };

                db.Orders.Add(newOrder); //Добавление
                db.SaveChanges();

                var orderAmount = (from orders in db.OrderAmountCustomer
                                where orders.Car_id == PageRentalCarViewModel.SelectedCars.Car_id &&
                                      orders.DateOfIssue == dataOfIssuePick &&
                                      orders.DateReturn == dataOfReturnPick
                                select orders.AmountOrder).FirstOrDefault();    //Сумма заказа

                if(orderAmount != null)
                {
                    var resultPassenger = MessageBox.Show($"К оплате: {orderAmount} руб. Арендуем?", "Оплата", MessageBoxButton.YesNo);

                    switch (resultPassenger)
                    {            
                        case MessageBoxResult.Yes:
                            break;
                        case MessageBoxResult.No:
                            db.Orders.Remove(newOrder);     //Удаление заказа из БД, если пользователь отказался
                            db.SaveChanges();
                            break;
                    }
                }
            }
        }
    }
}
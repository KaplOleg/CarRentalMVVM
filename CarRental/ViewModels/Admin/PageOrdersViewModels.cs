using CarRental.Commands.Base;
using CarRental.Model;
using CarRental.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarRental.ViewModels.Admin
{
    public class PageOrdersViewModels : ViewModel
    {
        private ObservableCollection<Orders> orders;
        public ObservableCollection<Orders> Orders { get => orders; set => Set(ref orders, value); }

        #region Комманды
        public Command BtnClickRemove { get; }
        private bool CanBtnClickRemoveExecute(object p)
        {
            if (p is null) return false;
            var dataGrid = (object[])p;
            if(dataGrid[0] is null) return false;
            return true;
        }
        private void OnBtnClickRemoveExecuted(object p)
        {
            var dataGrid = (object[])p;
            var removeOrder = (dataGrid[0] as Orders).Order_id;
            var dataGridItems = (dataGrid[1] as DataGrid);
            using(CarRentalEntities db = new CarRentalEntities())
            {
                var res = MessageBox.Show("Вы действительно хотите удалить заказ?", "", MessageBoxButton.YesNo);
                switch (res)
                {
                    case MessageBoxResult.Yes:
                        var sql = (from orders in db.Orders.ToList()
                                   where orders.Order_id == removeOrder
                                   select orders).FirstOrDefault();
                        if(sql != null){
                            db.Orders.Remove(sql);
                            db.SaveChanges();
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                };
            }
            CarRentalEntities db1 = new CarRentalEntities();
            dataGridItems.ItemsSource = db1.Orders.ToList();
            dataGridItems.Items.Refresh();
        }
        #endregion

        public PageOrdersViewModels()
        {
            CarRentalEntities db = new CarRentalEntities();
            Orders = new ObservableCollection<Orders>(db.Orders.ToList());
            BtnClickRemove = new LambdaCommand(OnBtnClickRemoveExecuted, CanBtnClickRemoveExecute);
        }
    }
}

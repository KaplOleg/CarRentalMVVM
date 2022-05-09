using CarRental.Model;
using System.Linq;
using System.Windows.Controls;

namespace CarRental.Views.Pages.Admin
{
    public partial class PageOrders : Page
    {
        public PageOrders()
        {
            InitializeComponent();
        }

        private void Search_Customer(object sender, System.Windows.RoutedEventArgs e)
        {
            CarRentalEntities db = new CarRentalEntities();
            var tb = sender as TextBox;
            if (tb.Text != "")
            {
                var filteredList = db.Orders.ToList().Where(t => t.Сustomers.Surname.ToLower().Contains(tb.Text.ToLower()));  //Получаем список по введенному тексту в TextBox(Поиск)
                dataGrid.ItemsSource = null; //Обнуляем список
                dataGrid.ItemsSource = filteredList; //Обновляем список
            }
            else
            {
                dataGrid.ItemsSource = db.Orders.ToList(); //Первоначальный список
            }
        }
    }
}

using CarRental.Model;
using CarRental.Views.Windows.Admin;
using System.Linq;
using System.Windows.Controls;

namespace CarRental.Views.Pages
{
    public partial class PageWorkTable : Page
    {
        public PageWorkTable()
        {
            InitializeComponent();
        }

        private void Search_Cars(object sender, System.Windows.RoutedEventArgs e)
        {
            CarRentalEntities db = new CarRentalEntities();
            var tb = sender as TextBox;
            if (tb.Text != "")
            {
                var filteredList = db.Cars.ToList().Where(t => t.CarBrands.BrandName.ToLower().Contains(tb.Text.ToLower()) ||
                                                          t.CarModels.ModelName.ToLower().Contains(tb.Text.ToLower()));  //Получаем список по введенному тексту в TextBox(Поиск)
                dataGrid.ItemsSource = null; //Обнуляем список
                dataGrid.ItemsSource = filteredList; //Обновляем список
            }
            else
            {
                dataGrid.ItemsSource = db.Cars.ToList(); //Первоначальный список
            }
        }

        private void ClickAddCars(object sender, System.Windows.RoutedEventArgs e)
        {
            WindowAddCar windowAddCar = new WindowAddCar();
            windowAddCar.Show();
        }
    }
}

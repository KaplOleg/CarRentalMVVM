using CarRental.Commands.Base;
using CarRental.Model;
using CarRental.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarRental.ViewModels.Admin
{
    public class PageWorkTableViewModel : ViewModel
    {
        #region Поля
        private ObservableCollection<Cars> cars;
        private ObservableCollection<CarSpecifications> specifications;
        private Cars selectedCars;
        #endregion

        #region Свойства
        public ObservableCollection<Cars> Cars { get => cars; set => Set(ref cars, value); }
        public ObservableCollection<CarSpecifications> Specifications { get => specifications; set => Set(ref specifications, value); }
        public Cars SelectedCars { get => selectedCars; set => Set(ref selectedCars, value); }
        #endregion

        public Command UpdateClickCommand { get; }
        private bool CanUpdateClickCommandExecute(object p)
        {
            if (p is null) return false;
            return true;
        }
        private void OnUpdateClickCommandExecuted(object p)
        {
            var paramCar = (p as Cars);
            using (CarRentalEntities db = new CarRentalEntities())
            {
                var car = db.Cars.FirstOrDefault(c => c.Car_id == paramCar.Car_id);

                car.CarBrands.BrandName = paramCar.CarBrands.BrandName;
                car.CarModels.ModelName = paramCar.CarModels.ModelName;
                car.CarSpecifications.Transmission = paramCar.CarSpecifications.Transmission;
                car.CarSpecifications.EngineVolume = paramCar.CarSpecifications.EngineVolume;
                car.CarSpecifications.EnginePower = paramCar.CarSpecifications.EnginePower;
                car.CarSpecifications.MaxSpeed = paramCar.CarSpecifications.MaxSpeed;
                car.CarSpecifications.FuelConsumption = paramCar.CarSpecifications.FuelConsumption;
                car.CarSpecifications.CarColor = paramCar.CarSpecifications.CarColor;
                car.CarSpecifications.CarPhoto = paramCar.CarSpecifications.CarPhoto;
                car.RentalPrice = paramCar.RentalPrice;
                car.CarStatus = paramCar.CarStatus;

                db.SaveChanges();
            }
            MessageBox.Show("Данные успешно обновлены!", "", MessageBoxButton.OK);
        }

        public Command DeleteClickCommand { get; }
        private bool CanDeleteClickCommandExecute(object p)
        {
            if (p is null) return false;
            var dataGrid = (object[])p;
            if (dataGrid[0] is null) return false;
            return true;
        }
        private void OnDeleteClickCommandExecuted(object p)
        {
            var dataGrid = (object[])p;
            var dataGridItems = (dataGrid[1] as DataGrid);
            var paramCar = (dataGrid[0] as Cars).Car_id;
            using (CarRentalEntities db = new CarRentalEntities())
            {
                var car = db.Cars.FirstOrDefault(c => c.Car_id == paramCar);
                var res = MessageBox.Show("Вы действительно хотите удалить запись?", "", MessageBoxButton.YesNo);
                switch (res)
                {
                    case MessageBoxResult.Yes:
                        db.Cars.Remove(car);
                        db.SaveChanges();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            CarRentalEntities db1 = new CarRentalEntities();
            dataGridItems.ItemsSource = db1.Cars.ToList();
            dataGridItems.Items.Refresh();
        }
        public PageWorkTableViewModel()
        {
            CarRentalEntities db = new CarRentalEntities();
            Cars = new ObservableCollection<Cars>(db.Cars.ToList());
            UpdateClickCommand = new LambdaCommand(OnUpdateClickCommandExecuted, CanUpdateClickCommandExecute);
            DeleteClickCommand = new LambdaCommand(OnDeleteClickCommandExecuted, CanDeleteClickCommandExecute);
        }       
    }
}
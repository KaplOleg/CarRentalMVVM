using CarRental.Model;
using CarRental.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace CarRental.ViewModels.Customer
{
    public class PageRentalCarViewModel : ViewModel
    {
        #region Свойства
        private List<Cars> cars;
        public List<Cars> Cars { get => cars; set => Set(ref cars, value); } //Список машин в ListView

        public ObservableCollection<CarBrands> BrandsCars { get; set; } //Список машин в ComboBox Brands

        public static Cars SelectedCars { get; set; }  //Выбранный автомобиль в ListView
        #endregion

        #region Поиск с использ. ComboBox
        private List<Cars> searchCarBrandList = new List<Cars>(); //Новый список от свойства SearchBrand

        private CarBrands valueSearchBrandList; //значение value от свойства SearchBrandCar
        public CarBrands SearchBrandCar
        {
            set
            {
                valueSearchBrandList = value;

                CarRentalEntities db = new CarRentalEntities();
                Cars = new List<Cars>(db.Cars.ToList());    //Получаем полный список машин из БД
                if(valueSearchBrandList.BrandName == "Все автомобили" && valueSearchStatusList is null) //Вывод всех машин
                {
                    Cars = new List<Cars>(db.Cars.ToList());
                }
                else if(valueSearchBrandList.BrandName == "Все автомобили" && valueSearchStatusList != null)
                {
                    var searchCars = cars.Where(car => car.CarStatus.Contains(valueSearchStatusList.Text)).ToList(); //Поиск всех машин только по CarStatus
                    if (searchCars != null) Cars = searchCars;
                }
                else if (valueSearchStatusList != null && valueSearchBrandList.BrandName != null)
                {
                    searchCarBrandList = cars.Where(car => car.CarBrands.BrandName.Contains(value.BrandName) && 
                                                            car.CarStatus.Contains(valueSearchStatusList.Text)).ToList(); //Поиск машины по BrandName && CarStatus
                    if (searchCarBrandList != null) Cars = searchCarBrandList;
                }
                else if (valueSearchBrandList.BrandName != null)
                {
                    searchCarBrandList = cars.Where(car => car.CarBrands.BrandName.Contains(value.BrandName)).ToList(); //Поиск машины по BrandName
                    if (searchCarBrandList != null) Cars = searchCarBrandList;
                }
            }
        }
        private TextBlock valueSearchStatusList; //значение value от свойства SearchBrandCar
        public TextBlock SearchListStatus
        {
            set
            {
                valueSearchStatusList = value;

                CarRentalEntities db = new CarRentalEntities();
                Cars = new List<Cars>(db.Cars.ToList()); //Получаем полный список машин из БД
                if (valueSearchBrandList is null || valueSearchBrandList.BrandName == "Все автомобили")
                {
                    var searchCars = cars.Where(car => car.CarStatus.Contains(value.Text)).ToList(); //Поиск машин только по CarStatus
                    if (searchCars != null) Cars = searchCars;
                }
                else if (valueSearchStatusList != null && valueSearchBrandList.BrandName != null)
                {
                    var searchCars = cars.Where(car => car.CarBrands.BrandName.Contains(valueSearchBrandList.BrandName) &&
                                                            car.CarStatus.Contains(valueSearchStatusList.Text)).ToList(); //Поиск машины по BrandName && CarStatus
                    if (searchCars != null) Cars = searchCars;
                }
            }
        }
        #endregion

        public PageRentalCarViewModel()
        {
            CarRentalEntities db = new CarRentalEntities();              
            Cars = new List<Cars>(db.Cars.ToList());
            BrandsCars = new ObservableCollection<CarBrands>(db.CarBrands.ToList());               
            BrandsCars.Add(new CarBrands() { BrandName = "Все автомобили"});
        }
    }
}

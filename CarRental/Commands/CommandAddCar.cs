using CarRental.Commands.Base;
using CarRental.Model;
using CarRental.ViewModels.Admin;
using System.Linq;
using System.Windows;

namespace CarRental.Commands
{
    public class CommandAddCar : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            var infoCar = (object[])parameter;

            using (CarRentalEntities db = new CarRentalEntities())
            {
                var sqlAdminCode = (from admin in db.Admins.ToList()
                                    where admin.AdminLogin == WindowAdminViewModel.loginAdmin
                                    select admin.Admin_id).FirstOrDefault();

                var sqlBrand = (from sbrand in db.CarBrands.ToList()
                                where sbrand.BrandName == infoCar[0].ToString()
                                select sbrand.Brand_id).FirstOrDefault();

                var sqlModel= (from smodel in db.CarModels.ToList()
                                where smodel.ModelName == infoCar[1].ToString()
                                select smodel.Model_id).FirstOrDefault();

                if(sqlAdminCode != 0)
                {
                    var newCar = new Cars();
                    newCar.Admin_id = sqlAdminCode;
                    db.Cars.Add(newCar);

                    if (sqlBrand == 0)
                    {
                        CarBrands brand = new CarBrands()
                        {
                            BrandName = infoCar[0].ToString()
                        };
                        db.CarBrands.Add(brand);
                        db.SaveChanges();
                        newCar.Brand_id = brand.Brand_id;
                    }
                    else
                    {
                        newCar.Brand_id = sqlBrand;
                    }
               
                    if(sqlModel == 0)
                    {
                        CarModels model = new CarModels()
                        {
                            ModelName = infoCar[1].ToString()
                        };
                        db.CarModels.Add(model);
                        db.SaveChanges();
                        newCar.Model_id = model.Model_id;
                    }
                    else
                    {
                        newCar.Model_id = sqlModel;
                    }

                    CarSpecifications specification = new CarSpecifications()
                    {
                        CarBody = infoCar[3].ToString(),
                        ReleaseYear = int.Parse(infoCar[2].ToString()),
                        Transmission = infoCar[7].ToString(),
                        EngineVolume = double.Parse(infoCar[4].ToString().Replace('.',',')),
                        EnginePower = double.Parse(infoCar[8].ToString().Replace('.', ',')),
                        MaxSpeed = double.Parse(infoCar[5].ToString().Replace('.', ',')),
                        FuelConsumption = double.Parse(infoCar[9].ToString().Replace('.', ',')),
                        CarColor = infoCar[6].ToString(),
                        CarPhoto = infoCar[10].ToString()
                    };
                    db.CarSpecifications.Add(specification);
                    db.SaveChanges();
                    

                    newCar.Specification_id = specification.Specification_id;

                    CarLocations location = new CarLocations()
                    {
                       Longitude = double.Parse(infoCar[11].ToString().Replace('.', ',')),
                       Latitude = double.Parse(infoCar[12].ToString().Replace('.', ','))
                    };
                    db.CarLocations.Add(location);
                    db.SaveChanges();

                    newCar.Location_id = location.Location_id;
                    db.SaveChanges();
                }
            }
            (infoCar[13] as Window).Close();
        }
    }
}

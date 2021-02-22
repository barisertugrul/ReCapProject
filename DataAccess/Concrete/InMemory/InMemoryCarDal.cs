using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car { CarId = 1, BrandId = 1, ColorId = 1, ModelYear = 2018, DailyPrice = 250, Description = "Kiralık beyaz Honda" },
                new Car { CarId = 2, BrandId = 2, ColorId = 2, ModelYear = 2014, DailyPrice = 200, Description = "Kiralık Mavi Toyota" },
                new Car { CarId = 3, BrandId = 3, ColorId = 3, ModelYear = 2019, DailyPrice = 350, Description = "Kiralık siyah Mercedes" },
                new Car { CarId = 4, BrandId = 4, ColorId = 4, ModelYear = 2020, DailyPrice = 450, Description = "Kiralık kırmızı BMW" },
                new Car { CarId = 5, BrandId = 5, ColorId = 5, ModelYear = 2018, DailyPrice = 150, Description = "Kiralık gri Audi" },
                new Car { CarId = 6, BrandId = 9, ColorId = 9, ModelYear = 2012, DailyPrice = 100, Description = "Kiralık mor Tofaş" },
        };
        }
        public void Add(Car car)
        {
            car.CarId = LastIndex() + 1;
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public Car GetById(int carId)
        {
            Car carToResult = _cars.SingleOrDefault(c => c.CarId == carId);
            return carToResult;
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.Description = car.Description;
        }

        public int LastIndex()
        {
            if (_cars.Count > 0)
            {
                return _cars[_cars.Count - 1].CarId;
            }
            else
            {
                return 0;
            }
        }
    }
}

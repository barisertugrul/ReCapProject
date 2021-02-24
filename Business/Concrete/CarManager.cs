using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        ICarValidationService _carValidationService;

        public CarManager(ICarDal carDal, ICarValidationService carValidationService)
        {
            _carDal = carDal;
            _carValidationService = carValidationService;
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public void Add(Car car)
        {
            if (_carValidationService.Validate(car))
            {
                _carDal.Add(car);
            }
            else
            {
                Console.WriteLine("Adding could not be performed because some fields do not conform to the valid data rules.");
            }
        }

        public void Update(Car car)
        {
            if (_carValidationService.Validate(car))
            {
                _carDal.Update(car);
            }
            else
            {
                Console.WriteLine("Adding could not be performed because some fields do not conform to the valid data rules.");
            }
        }

        public List<Car> GetCarsByBrandId(int branId)
        {
            return _carDal.GetAll(c => c.BrandId == branId);
        }

        public List<Car> GetCarsByColorId(int colorId)
        {
            return _carDal.GetAll(c => c.ColorId == colorId);
        }

        public Car GetById(int id)
        {
            return _carDal.Get(c => c.CarId == id);
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }
    }
}

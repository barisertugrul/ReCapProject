using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarValidationManager : ICarValidationService
    {
        public bool Validate(Car car)
        {
            //
            return NameValidate(car)
                && DailyPriceValidate(car);
        }

        private bool DailyPriceValidate(Car car)
        {
            if (car.DailyPrice > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("The daily rental fee of the car must be greater than 0");
                return false;
            }
        }

        private bool NameValidate(Car car)
        {
            if (car.Name.Length >= 2)
            {
                return true;
            }
            else
            {
                Console.WriteLine("The car name must be at least 2 characters");
                return false;
            }
        }


    }
}

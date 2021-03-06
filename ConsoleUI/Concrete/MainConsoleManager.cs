﻿using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI.Concrete
{
    public class MainConsoleManager
    {
        /****************************************************************************
         * 
         * Console işlemleri merkezi
         * 
         ***************************************************************************/
        /* 
         * Performans açısından manager sınıflarını burada atamak mı
         * Yoksa yeri geldikçe, fonksiyonlar içinde tekrar tekrar newlemek mi 
         * daha mantıklı bilemiyorum. Şimdilik newlemeyi burada yapıyorum
         */

        CarManager _carManager = new CarManager(new EfCarDal(), new CarValidationManager());
        BrandManager _brandManager = new BrandManager(new EfBrandDal());
        ColorManager _colorManager = new ColorManager(new EfColorDal());

        //string[] brandsList = { "1-Honda", "2-Toyota", "3-Mercedes",
        //        "4-BMW", "5-Audi", "6-Wolksvagen", "7-Renault", "8-Ford", "9-Tofaş" };
        //string[] colorsList = { "1-WHITE", "2-BLUE", "3-BLACK",
        //        "4-RED", "5-GREY", "6-GREEN", "7-YELLOW", "8-ORANGE", "9-PURPLE" };

        public void MainMenu()
        {
            string consoleVal;
            string[] menuItems = new string[] { "1-Car Manager", "2-Brand Manager", "3-Color Manager", "4-Settings", "5-EXIT" };
            ConsoleTexts.WriteMenuConsoleTexts("RENT A CAR MAIN MENU", menuItems);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select number of menu item");
            if (consoleVal == "") consoleVal = "0";
            int selected = Convert.ToInt32(consoleVal);
            switch (selected)
            {
                case 1:
                    CarMenu();
                    MainMenu();
                    break;
                case 2:
                    BrandMenu();
                    MainMenu();
                    break;
                case 3:
                    ColorMenu();
                    MainMenu();
                    break;
                case 4:
                    //ListMenu();
                    //MainMenu();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("You made the wrong choice. Please try again.");
                    MainMenu();
                    break;
            }
        }

        private void ColorMenu()
        {
            string consoleVal;
            string[] menuItems = new string[] { "1-Add New Color", "2-Update a Color", "3-Delete a Color", "4-View Colors List", "5-RETURN MAIN MENU" };
            ConsoleTexts.WriteMenuConsoleTexts("COLOR MANAGER", menuItems);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select number of menu item");
            if (consoleVal == "") consoleVal = "0";
            int selected = Convert.ToInt32(consoleVal);
            switch (selected)
            {
                case 1:
                    AddColor();
                    ColorMenu();
                    break;
                case 2:
                    UpdateColor();
                    ColorMenu();
                    break;
                case 3:
                    DeleteColor();
                    ColorMenu();
                    break;
                case 4:
                    ListColors();
                    ColorMenu();
                    break;
                case 5:
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("You made the wrong choice. Please try again.");
                    ColorMenu();
                    break;
            }
        }

        private void BrandMenu()
        {
            string consoleVal;

            string[] menuItems = new string[] { "1-Add New Brand", "2-Update a Brand", "3-Delete a Brand", "4-View Brands List", "5-RETURN MAIN MENU" };
            ConsoleTexts.WriteMenuConsoleTexts("BRAND MANAGER", menuItems);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select number of menu item");
            if (consoleVal == "") consoleVal = "0";
            int selected = Convert.ToInt32(consoleVal);
            switch (selected)
            {
                case 1:
                    AddBrand();
                    BrandMenu();
                    break;
                case 2:
                    UpdateBrand();
                    BrandMenu();
                    break;
                case 3:
                    DeleteBrand();
                    BrandMenu();
                    break;
                case 4:
                    ListBrands();
                    BrandMenu();
                    break;
                case 5:
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("You made the wrong choice. Please try again.");
                    BrandMenu();
                    break;
            }
        }

        private void CarMenu()
        {
            string consoleVal;
            string[] menuItems = new string[] { "1-Add New Car", "2-Update a Car", "3-Delete a Car", "4-View Cars List",
                "5-Cars by Brand", "6-Cars by Color", "7-RETURN MAIN MENU" };

            ConsoleTexts.WriteMenuConsoleTexts("CAR MANAGER", menuItems);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select number of menu item");
            if (consoleVal == "") consoleVal = "0";
            int selected = Convert.ToInt32(consoleVal);
            switch (selected)
            {
                case 1:
                    AddCar();
                    CarMenu();
                    break;
                case 2:
                    UpdateCar();
                    CarMenu();
                    break;
                case 3:
                    DeleteCar();
                    CarMenu();
                    break;
                case 4:
                    ListCars();
                    CarMenu();
                    break;
                case 5:
                    CarsByBrand();
                    CarMenu();
                    break;
                case 6:
                    CarsByColor();
                    CarMenu();
                    break;
                case 7:
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("You made the wrong choice. Please try again.");
                    CarMenu();
                    break;
            }
        }

        private void AddCar()
        {
            Car car = new Car();
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("ADD NEW CAR");
            ConsoleTexts.HeaderFooterLine();

            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Type Car Name");
            car.Name = consoleVal;

            ConsoleTexts.WriteMenuConsoleTexts("BRANDS", StrBrandList());
            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Brand ID");
            car.BrandId = Convert.ToInt32(consoleVal);

            ConsoleTexts.WriteMenuConsoleTexts("COLORS", StrColorList());
            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Color ID");
            car.ColorId = Convert.ToInt32(consoleVal);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Type Model Year");
            car.ModelYear = Convert.ToInt16(consoleVal);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Type Daily Price");
            car.DailyPrice = Convert.ToDecimal(consoleVal);

            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Type Description");
            car.Description = consoleVal;

            _carManager.Add(car);
        }

        private void UpdateCar()
        {
            Car car;
            string consoleVal;
            List<Car> cars = _carManager.GetAll();

            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("CAR UPDATE FORM");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("CARS", StrCarList(cars));
            if (StrCarList(cars) != null)
            {
                consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Car ID to Update");
                car = _carManager.GetById(Convert.ToInt32(consoleVal));

                consoleVal = ConsoleTexts.ConsoleWriteReadLine("Type Car Name");
                car.Name = consoleVal;
                if (consoleVal != "")
                {
                    car.Name = consoleVal;
                }

                ConsoleTexts.WriteMenuConsoleTexts("BRANDS", StrBrandList());
                consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Brand ID (Leave blank if you do not want to change)");
                if (consoleVal != "")
                {
                    car.BrandId = Convert.ToInt32(consoleVal);
                }

                ConsoleTexts.WriteMenuConsoleTexts("COLORS", StrColorList());
                consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Color ID (Leave blank if you do not want to change)");
                if (consoleVal != "")
                {
                    car.ColorId = Convert.ToInt32(consoleVal);
                }

                consoleVal = ConsoleTexts.ConsoleWriteReadLine("Type Model Year (Leave blank if you do not want to change)");
                if (consoleVal != "")
                {
                    car.ModelYear = Convert.ToInt16(consoleVal);
                }

                consoleVal = ConsoleTexts.ConsoleWriteReadLine("Type Daily Price (Leave blank if you do not want to change)");
                if (consoleVal != "")
                {
                    car.DailyPrice = Convert.ToDecimal(consoleVal);
                }

                consoleVal = ConsoleTexts.ConsoleWriteReadLine("Type Description (Leave blank if you do not want to change)");
                if (consoleVal != "")
                {
                    car.Description = consoleVal;
                }

                _carManager.Update(car);
            }
            else
            {
                CarMenu();
            }
        }

        private void DeleteCar()
        {
            Car car;
            string consoleVal;
            List<Car> cars = _carManager.GetAll();

            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("CAR DELETE FORM");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("CARS", StrCarList(cars));
            if (StrCarList(cars) != null)
            {
                consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Car ID to Delete");
                if ( consoleVal != "")
                {
                    car = _carManager.GetById(Convert.ToInt32(consoleVal));
                    if (ConsoleTexts.ConfirmDelete()) _carManager.Delete(car);
                }
            }
        }

        private  void ListCars()
        {
            List<Car> cars = _carManager.GetAll();
            ConsoleTexts.WriteMenuConsoleTexts("CARS", StrCarList(cars));
        }

        private void CarsByColor()
        {
            string consoleVal;
            ConsoleTexts.WriteMenuConsoleTexts("COLORS", StrColorList());
            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Color ID");
            int colordId = Convert.ToInt32(consoleVal);

            string color = _colorManager.GetById(colordId).Name;
            List<Car> cars = _carManager.GetCarsByColorId(colordId);
            ConsoleTexts.WriteMenuConsoleTexts(color + " Cars", StrCarList(cars));
        }

        private void CarsByBrand()
        {
            string consoleVal;
            ConsoleTexts.WriteMenuConsoleTexts("BRANDS", StrColorList());
            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Brand ID");
            int brandId = Convert.ToInt32(consoleVal);

            string brand = _brandManager.GetById(brandId).Name;
            List<Car> cars = _carManager.GetCarsByColorId(brandId);
            ConsoleTexts.WriteMenuConsoleTexts(brand + " Branded Cars", StrCarList(cars));
        }

        private string[] StrCarList(List<Car> cars)
        {

            //List<Car> cars = _carManager.GetAll();
            if (cars.Count > 0)
            {
                List<Brand> brands = _brandManager.GetAll();
                List<Color> colors = _colorManager.GetAll();
                string[] carList = new string[cars.Count];
                int i = 0;
                //Burada bu şekilde kullanım uygun değil ama, doğrusunu bilmediğim için, görüntüleme amaçlı geçici olarak kullandım
                List<CarDto> carsDto = _carManager.GetAllDto(cars, brands, colors);
                foreach (CarDto car in carsDto)
                {
                    carList[i] = "ID: " + car.CarId + " - Name: " + car.Name + " Brand: " + car.Brand + " - Color: " + car.Color + " - (Daily Price: " + car.DailyPrice + ") " + car.Description;
                    i++;
                }
                return carList;
            }
            else
            {
                return null;
            }
        }

        private void AddColor()
        {
            Color color = new Color();
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("ADD NEW COLOR");
            ConsoleTexts.HeaderFooterLine();

            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Type Color Name");
            color.Name = consoleVal;

            _colorManager.Add(color);
        }

        private void UpdateColor()
        {
            Color color;
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("COLOR UPDATE FORM");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("COLORS", StrColorList());
            if (StrColorList() != null)
            {
                consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Color ID to Update");
                if (consoleVal != "")
                { 
                    color = _colorManager.GetById(Convert.ToInt32(consoleVal));

                    consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Color Name (Leave blank if you do not want to change)");
                    if (consoleVal != "")
                    {
                        color.Name = consoleVal;
                    }
                    _colorManager.Update(color);
                }
            }
        }

        private void DeleteColor()
        {
            Color color;
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("COLOR DELETE FORM");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("COLORS", StrColorList());
            if (StrColorList() != null)
            {
                consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Color ID to Delete");
                if (consoleVal != "")
                {
                    color = _colorManager.GetById(Convert.ToInt32(consoleVal));
                    if (ConsoleTexts.ConfirmDelete()) _colorManager.Delete(color);
                }
            }
        }

        private void ListColors()
        {
            ConsoleTexts.WriteMenuConsoleTexts("COLORS LIST", StrColorList());
        }

        private string[] StrColorList()
        {
            string[] colorList = new string[_colorManager.GetAll().Count];
            int i = 0;
            foreach (Color color in _colorManager.GetAll())
            {
                colorList[i] = "ID: " + color.Id + " - " + color.Name;
                i++;
            }
            return colorList;
        }

        private void AddBrand()
        {
            Brand brand = new Brand();
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("ADD NEW BRAND");
            ConsoleTexts.HeaderFooterLine();

            consoleVal = ConsoleTexts.ConsoleWriteReadLine("Type Brand Name");
            brand.Name = consoleVal;
            _brandManager.Add(brand);
        }

        private void UpdateBrand()
        {
            Brand brand;
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("BRAND UPDATE FORM");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("BRANDS", StrBrandList());
            if (StrBrandList() != null)
            {
                consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Brand ID to Update");
                if ( consoleVal != "" )
                {
                    brand = _brandManager.GetById(Convert.ToInt32(consoleVal));
                    consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Brand Name (Leave blank if you do not want to change)");
                    if (consoleVal != "")
                    {
                        brand.Name = consoleVal;
                    }
                    _brandManager.Update(brand);
                }
            }
        }

        private void DeleteBrand()
        {
            Brand brand;
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("BRAND DELETE FORM");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("BRANDS", StrBrandList());
            if (StrBrandList() != null)
            {
                consoleVal = ConsoleTexts.ConsoleWriteReadLine("Select Brand ID to Delete");
                if ( consoleVal != "")
                {
                    brand = _brandManager.GetById(Convert.ToInt32(consoleVal));
                    if (ConsoleTexts.ConfirmDelete()) _brandManager.Delete(brand);
                }
            }
        }

        private void ListBrands()
        {
            ConsoleTexts.WriteMenuConsoleTexts("BRANDS LIST", StrBrandList());
        }

        private string[] StrBrandList()
        {
            string[] brandList = new string[_brandManager.GetAll().Count];
            int i = 0;
            foreach (Brand brand in _brandManager.GetAll())
            {
                brandList[i] = "ID: " + brand.Id + " - " + brand.Name;
                i++;
            }
            return brandList;
        }
    }
}

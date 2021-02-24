using Business.Concrete;
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

        string[] brandsList = { "1-Honda", "2-Toyota", "3-Mercedes",
                "4-BMW", "5-Audi", "6-Wolksvagen", "7-Renault", "8-Ford", "9-Tofaş" };
        string[] colorsList = { "1-WHITE", "2-BLUE", "3-BLACK",
                "4-RED", "5-GREY", "6-GREEN", "7-YELLOW", "8-ORANGE", "9-PURPLE" };

        public void MainMenu()
        {
            string[] menuItems = new string[] { "1-Car Manager", "2-Brand Manager", "3-Color Manager", "4-Settings", "5-EXIT" };
            ConsoleTexts.WriteMenuConsoleTexts("RENT A CAR MAIN MENU", menuItems);
            Console.Write("\nSelect number of menu item: ");
            string val;
            val = Console.ReadLine();
            if (val == "") val = "0";
            int selected = Convert.ToInt32(val);
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
            string[] menuItems = new string[] { "1-Add New Color", "2-Update a Color", "3-Delete a Color", "4-View Colors List", "5-RETURN MAIN MENU" };
            ConsoleTexts.WriteMenuConsoleTexts("COLOR MANAGER", menuItems);
            Console.Write("\nSelect number of menu item: ");
            string val;
            val = Console.ReadLine();
            if (val == "") val = "0";
            int selected = Convert.ToInt32(val);
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

            string[] menuItems = new string[] { "1-Add New Brand", "2-Update a Brand", "3-Delete a Brand", "4-View Brands List", "5-RETURN MAIN MENU" };
            ConsoleTexts.WriteMenuConsoleTexts("BRAND MANAGER", menuItems);
            Console.Write("\nSelect number of menu item: ");
            string val;
            val = Console.ReadLine();
            if (val == "") val = "0";
            int selected = Convert.ToInt32(val);
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
            string[] menuItems = new string[] { "1-Add New Car", "2-Update a Car", "3-Delete a Car", "4-View Cars List", "5-RETURN MAIN MENU" };
            ConsoleTexts.WriteMenuConsoleTexts("CAR MANAGER", menuItems);
            Console.Write("\nSelect number of menu item: ");
            string val;
            val = Console.ReadLine();
            if (val == "") val = "0";
            int selected = Convert.ToInt32(val);
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
            Console.Write("\nType Car Name: ");
            consoleVal = Console.ReadLine();
            car.Name = consoleVal;
            ConsoleTexts.WriteMenuConsoleTexts("BRANDS", StrBrandList());
            Console.Write("\nSelect Brand ID: ");
            consoleVal = Console.ReadLine();
            car.BrandId = Convert.ToInt32(consoleVal);
            ConsoleTexts.WriteMenuConsoleTexts("COLORS", StrColorList());
            Console.Write("\nSelect Color ID: ");
            consoleVal = Console.ReadLine();
            car.ColorId = Convert.ToInt32(consoleVal);
            Console.Write("\nType Model Year: ");
            consoleVal = Console.ReadLine();
            car.ModelYear = Convert.ToInt16(consoleVal);
            Console.Write("\nType Daily Price: ");
            consoleVal = Console.ReadLine();
            car.DailyPrice = Convert.ToDecimal(consoleVal);
            Console.Write("\nType Description: ");
            consoleVal = Console.ReadLine();
            car.Description = consoleVal;
            _carManager.Add(car);
        }

        private void UpdateCar()
        {
            Car car;
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("CAR UPDATE FORM");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("CARS", StrCarList());
            if (StrCarList() != null)
            {
                Console.Write("\nSelect Car ID to Update: ");
                consoleVal = Console.ReadLine();
                car = _carManager.GetById(Convert.ToInt32(consoleVal));

                Console.Write("\nType Car Name: ");
                consoleVal = Console.ReadLine();
                car.Name = consoleVal;
                if (consoleVal != "")
                {
                    car.Name = consoleVal;
                }

                ConsoleTexts.WriteMenuConsoleTexts("BRANDS", StrBrandList());
                Console.Write("\nSelect Brand ID (Leave blank if you do not want to change): ");
                consoleVal = Console.ReadLine();
                if (consoleVal != "")
                {
                    car.BrandId = Convert.ToInt32(consoleVal);
                }

                ConsoleTexts.WriteMenuConsoleTexts("COLORS", StrColorList());
                Console.Write("\nSelect Color ID (Leave blank if you do not want to change): ");
                consoleVal = Console.ReadLine();
                if (consoleVal != "")
                {
                    car.ColorId = Convert.ToInt32(consoleVal);
                }
                Console.Write("\nType Model Year (Leave blank if you do not want to change): ");
                consoleVal = Console.ReadLine();
                if (consoleVal != "")
                {
                    car.ModelYear = Convert.ToInt16(consoleVal);
                }
                Console.Write("\nType Daily Price (Leave blank if you do not want to change): ");
                consoleVal = Console.ReadLine();
                if (consoleVal != "")
                {
                    car.DailyPrice = Convert.ToDecimal(consoleVal);
                }
                Console.Write("\nType Description (Leave blank if you do not want to change): ");
                consoleVal = Console.ReadLine();
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
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("CAR DELETE FORM");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("CARS", StrCarList());
            Console.Write("\nSelect Car ID to Delete: ");
            consoleVal = Console.ReadLine();
            car = _carManager.GetById(Convert.ToInt32(consoleVal));
            _carManager.Delete(car);
        }

        private  void ListCars()
        {
            ConsoleTexts.WriteMenuConsoleTexts("CARS", StrCarList());
        }

        private string[] StrCarList()
        {

            List<Car> cars = _carManager.GetAll();
            if (cars.Count > 0)
            {
                List<Brand> brands = _brandManager.GetAll();
                List<Color> colors = _colorManager.GetAll();
                string[] carList = new string[cars.Count];
                int i = 0;
                List<CarDto> carsDto = (List<CarDto>)(from c in cars
                                                      join b in brands
                                                      on c.BrandId equals b.Id
                                                      join cl in colors
                                                      on c.ColorId equals cl.Id
                                                      select new CarDto
                                                      {
                                                          CarId = c.CarId,
                                                          BrandId = c.BrandId,
                                                          ColorId = c.ColorId,
                                                          Name = c.Name,
                                                          ModelYear = c.ModelYear,
                                                          DailyPrice = c.DailyPrice,
                                                          Brand = b.Name,
                                                          Color = cl.Name
                                                      }).ToList();
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
            Console.Write("\nType Color Name: ");
            consoleVal = Console.ReadLine();
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
            Console.Write("\nSelect Color ID to Update: ");
            consoleVal = Console.ReadLine();
            color = _colorManager.GetById(Convert.ToInt32(consoleVal));
            Console.Write("\nSelect Color Name (Leave blank if you do not want to change): ");
            consoleVal = Console.ReadLine();
            if (consoleVal != "")
            {
                color.Name = consoleVal;
            }
            _colorManager.Update(color);
        }

        private void DeleteColor()
        {
            Color color;
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("COLOR DELETE FORM");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("COLORS", StrColorList());
            Console.Write("\nSelect Color ID to Delete: ");
            consoleVal = Console.ReadLine();

            color = _colorManager.GetById(Convert.ToInt32(consoleVal));
            _colorManager.Delete(color);
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
            Console.Write("\nType Brand Name: ");
            consoleVal = Console.ReadLine();
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
            Console.Write("\nSelect Brand ID to Update: ");
            consoleVal = Console.ReadLine();
            brand = _brandManager.GetById(Convert.ToInt32(consoleVal));
            Console.Write("\nSelect Brand Name (Leave blank if you do not want to change): ");
            consoleVal = Console.ReadLine();
            if (consoleVal != "")
            {
                brand.Name = consoleVal;
            }
            _brandManager.Update(brand);
        }

        private void DeleteBrand()
        {
            Brand brand;
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("BRAND DELETE FORM");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("BRANDS", StrBrandList());
            Console.Write("\nSelect Brand ID to Delete: ");
            consoleVal = Console.ReadLine();
            brand = _brandManager.GetById(Convert.ToInt32(consoleVal));
            _brandManager.Delete(brand);
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

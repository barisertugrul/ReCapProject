using Business.Concrete;
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

        CarManager _carManager;

        string[] brandsList = { "1-Honda", "2-Toyota", "3-Mercedes",
                "4-BMW", "5-Audi", "6-Wolksvagen", "7-Renault", "8-Ford", "9-Tofaş" };
        string[] colorsList = { "1-WHITE", "2-BLUE", "3-BLACK",
                "4-RED", "5-GREY", "6-GREEN", "7-YELLOW", "8-ORANGE", "9-PURPLE" };

        public MainConsoleManager(CarManager carManager)
        {
            _carManager = carManager;
        }

        public void MainMenu()
        {
            string[] menuItems = new string[] { "1-Add New Car", "2-Update Cars", "3-Delete Cars", "4-View Cars List", "5-EXIT" };
            ConsoleTexts.WriteMenuConsoleTexts("CAR MANAGER", menuItems);
            Console.Write("\nSelect number of menu item: ");
            string val;
            val = Console.ReadLine();
            int selected = Convert.ToInt32(val);
            switch (selected)
            {
                case 1:
                    AddMenu();
                    MainMenu();
                    break;
                case 2:
                    UpdateMenu();
                    MainMenu();
                    break;
                case 3:
                    DeleteMenu();
                    MainMenu();
                    break;
                case 4:
                    ListMenu();
                    MainMenu();
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

        private void AddMenu()
        {
            Car car = new Car();
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("ADD NEW CAR");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("BRANDS", brandsList);
            Console.Write("\nSelect Brand ID: ");
            consoleVal = Console.ReadLine();
            car.BrandId = Convert.ToInt32(consoleVal);
            ConsoleTexts.WriteMenuConsoleTexts("COLORS", colorsList);
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

        private void UpdateMenu()
        {
            Car car;
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("UPDATE FORM");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("CARS", StrCarList());
            Console.Write("\nSelect Car ID to Update: ");
            consoleVal = Console.ReadLine();
            car = _carManager.GetById(Convert.ToInt32(consoleVal));

            ConsoleTexts.WriteMenuConsoleTexts("BRANDS", brandsList);
            Console.Write("\nSelect Brand ID (Leave blank if you do not want to change): ");
            consoleVal = Console.ReadLine();
            if(consoleVal != "")
            {
                car.BrandId = Convert.ToInt32(consoleVal);
            }
            
            ConsoleTexts.WriteMenuConsoleTexts("COLORS", colorsList);
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

        private void DeleteMenu()
        {
            Car car;
            string consoleVal;
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.Header("DELETE FORM");
            ConsoleTexts.HeaderFooterLine();
            ConsoleTexts.WriteMenuConsoleTexts("CARS", StrCarList());
            Console.Write("\nSelect Car ID to Delete: ");
            consoleVal = Console.ReadLine();
            car = _carManager.GetById(Convert.ToInt32(consoleVal));
            _carManager.Delete(car);
        }

        private  void ListMenu()
        {
            ConsoleTexts.WriteMenuConsoleTexts("CARS", StrCarList());
        }

        private string[] StrCarList()
        {
            string[] carList = new string[_carManager.GetAll().Count];
            int i = 0;
            foreach (Car car in _carManager.GetAll())
            {
                carList[i] = "ID: " + car.CarId + " - Brand: " + brandsList[car.BrandId - 1] + " - Color: " + colorsList[car.ColorId - 1] + " - (Daily Price: " + car.DailyPrice + ") " + car.Description;
                i++;
            }
            return carList;
        }
    }
}

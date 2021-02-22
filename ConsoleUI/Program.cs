using Business.Concrete;
using ConsoleUI.Concrete;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());
            MainConsoleManager mainManager = new MainConsoleManager(carManager);
            mainManager.MainMenu();
        }
    }
}

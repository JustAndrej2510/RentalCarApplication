using RentalCarApplication.Core.Model;
using RentalCarApplication.EntityFramework;
using System;

namespace ConsoleApp1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DateTime RentDate = DateTime.Today;
            DateTime ReturnDate = DateTime.Today.AddDays(2);
            string OrderPrice;
            double CarPrice = 40.20;

            OrderPrice = (Convert.ToDouble(CarPrice) * Convert.ToDouble((ReturnDate - RentDate).Duration().Days)).ToString();
            Console.WriteLine(OrderPrice);
        }
    }
}

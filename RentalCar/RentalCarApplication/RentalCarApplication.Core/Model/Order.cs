using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;

namespace RentalCarApplication.Core.Model
{
    public class Order : Entity<int>
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Введите адресс заказа")]
        public string City { get; set; }
        [Required(ErrorMessage = "Выберите дату заказа")]
        public DateTime RentDate { get; set; }

        [Required(ErrorMessage = "Выберите дату возврата")]
        public DateTime ReturnDate { get; set; }
        public bool? Status { get; set; }
        public double Price { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }

        public string Email { get; set; }
        public User User { get; set; }

    }

}

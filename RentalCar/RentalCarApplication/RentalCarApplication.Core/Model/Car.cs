using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalCarApplication.Core.Model
{
    public class Car : Entity<int>
    {
        [Required(ErrorMessage = "Введите номер автомобиля")]
        [RegularExpression(@"^(\d{4,8})$", ErrorMessage = "Неверный формат номера")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Введите марку и модель автомобиля")]
        [RegularExpression(@"^([а-яА-Яa-zA-Z]{2,30})$", ErrorMessage = "Неверный формат марки автомобиля")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Введите объем двигателя автомобиля")]
        [RegularExpression(@"^([1-9](\d{1})?(\,\d{1,2})?)$", ErrorMessage = "Неверный формат расхода топлива")]
        public double EngineCapacity { get; set; }

        [Required(ErrorMessage = "Выберите тип КПП автомобиля")]
        public string GearBox { get; set; }

        [Required(ErrorMessage = "Выберите тип кузова автомобиля")]
        public string BodyType { get; set; }

        [Required(ErrorMessage = "Выберите количество мест в автомобиле")]
        [RegularExpression(@"^([1-9](\d{1})?)$", ErrorMessage = "Выберите количество сидений")]
        public int Seats { get; set; }
        
        [Required(ErrorMessage = "Введите расход топлива автомобиля")]
        [RegularExpression(@"^([1-9](\d{1})?(\,\d{1,2})?)$", ErrorMessage = "Неверный формат расхода топлива")]
        public double Consumption { get; set; }

        [Required(ErrorMessage = "Введите цену автомобиля")]
        [RegularExpression(@"^([1-9](\d{1,4})?(\,\d{1,2})?)$", ErrorMessage = "Неверный формат цены")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Загрузите фото автомобиля")]
        public string PhotoPath { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

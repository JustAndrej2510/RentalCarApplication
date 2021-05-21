using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalCarApplication.Core.Model
{
    public class User : Entity<string>
    {
        [Required(ErrorMessage = "Email | Введите ваш Email\n")]
        [RegularExpression(@"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@([a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(aero|arpa|asia|biz|by|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|ru|tel|travel|[a-z][a-z])$", ErrorMessage = "Email | Формат неверный. \n")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль | Введите пароль \n")]
        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Пароль | Формат неверный. \n")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Пароль | Минимальная длина 4 символа, максимальная 15 \n")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Имя | Введите ваше Имя\n")]
        [RegularExpression(@"^([a-zA-Zа-яА-ЯёЁ]{2,15})$", ErrorMessage = "Имя | Формат неверный. \n")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Фамилия | Введите вашу Фамилию\n")]
        [RegularExpression(@"^([a-zA-Zа-яА-ЯёЁ]{2,20}(\-[a-zA-Zа-яА-ЯёЁ]{2,20})?)$", ErrorMessage = "Фамилия | Формат неверный. \n")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Паспорт | Введите номер паспорта\n")]
        [RegularExpression(@"^([A-Z][A-Z][0-9]{7})$", ErrorMessage = "Паспорт | Формат неверный. \n")]
        public string Passport { get; set; }

        [Required(ErrorMessage = "Водительское удостоверение | Введите номер водительского удостоверения\n")]
        [RegularExpression(@"^([A-Z][A-Z]([0-9]){7})$", ErrorMessage = "Водительское удостоверение | Формат неверный.\n")]
        public string DriverLicense { get; set; }

        [Required(ErrorMessage = "Телефон | Введите номер телефона \n")]
        [RegularExpression(@"^\+375(29|33|44|25|17)[0-9]{7}$", ErrorMessage = "Телефон | Формат неверный.\n")]
        public string TelNumber { get; set; }

        public bool IsAdmin { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}

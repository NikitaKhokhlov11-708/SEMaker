using System;
using System.ComponentModel.DataAnnotations;

namespace SEMaker.ViewModels
{
    public class ProfileModel
    {

        [Required(ErrorMessage = "Не указана фамилия")]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указано отчество")]
        [DataType(DataType.Text)]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Не указана дата рожденияя")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Не указан логин")]
        [DataType(DataType.Text)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Неправильный номер")]
        public string PhoneNum { get; set; }
    }
}

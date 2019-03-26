using System;
using System.ComponentModel.DataAnnotations;

namespace SEMaker.ViewModels
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указано отчество")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Не указана дата рожденияя")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}

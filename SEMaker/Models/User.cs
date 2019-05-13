using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel.DataAnnotations;


namespace SEMaker.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhoneNum { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public int Premium { get; set; }
        public DateTime EndDate { get; set; }
    }
}

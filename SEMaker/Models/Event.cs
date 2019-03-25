using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEMaker.Models
{
    public class Event
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Sport { get; set; }
        [Required]
        public string City { get; set; }
        public string Author { get; set; }
    }
}

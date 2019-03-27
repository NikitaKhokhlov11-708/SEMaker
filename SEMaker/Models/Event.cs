using System;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public int Places { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Author { get; set; }
    }
}

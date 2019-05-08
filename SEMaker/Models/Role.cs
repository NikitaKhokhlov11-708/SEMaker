using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEMaker.Models
{
    public class Role
    {
        // 0 - user
        // 1 - admin
        public int Id { get; set; }
        public string Name { get; set; }
        public Role()
        {
        }
    }
}

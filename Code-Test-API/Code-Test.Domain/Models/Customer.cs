using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Test.Domain.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public int DeathBenefit { get; set; }

        public Customer() { }

    }
}

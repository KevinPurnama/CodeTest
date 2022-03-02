using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Test.Domain.Models
{
    public class Occupations
    {
        public List<string>? OccupationNames { get; set; }
        public List<string>? Errors { get; set; }

        public Occupations() { }
    }
}

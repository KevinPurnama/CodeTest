using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Test.Domain.Models
{
    public class MonthlyPremium
    {
        public double? Premium { get; set; }
        public List<string>? Errors { get; set; }

        public MonthlyPremium() { }
    }
}

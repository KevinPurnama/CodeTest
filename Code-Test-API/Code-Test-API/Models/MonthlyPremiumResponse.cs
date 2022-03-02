using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Test_API.Models
{
    public class MonthlyPremiumResponse
    {
        public double? Premium { get; set; }
        public List<string>? Errors { get; set; }

        public MonthlyPremiumResponse() { }
    }
}

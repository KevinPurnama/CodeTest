using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Test_API.Models
{
    public class GetOccupationsResponse
    {
        public List<string>? Occupations { get; set; }
        public List<string>? Errors { get; set; }

        public GetOccupationsResponse() { }
    }
}

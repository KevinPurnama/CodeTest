using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Test.Domain.DataModels
{
    public class OccupationRating
    {
        [System.ComponentModel.DataAnnotations.Key] 
        public string Rating { get; set; }
        public double Factor { get; set; }

    }
}

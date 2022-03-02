using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Code_Test.Domain.DataModels
{
    public class Occupation
    {
        public string Name { get; set; }
        public string Rating {get; set;}
    }
}

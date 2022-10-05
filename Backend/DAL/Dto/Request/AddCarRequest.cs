using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto.Request
{
    public class AddCarRequest
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    }
}

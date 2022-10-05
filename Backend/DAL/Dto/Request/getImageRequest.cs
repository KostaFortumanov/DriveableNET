using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto.Request
{
    public class getImageRequest
    {
        public Guid Id { get; set; }
        public int image { get; set; }
    }
}

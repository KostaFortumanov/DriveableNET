using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto.Request
{
    public class ImageRequest
    {
       public List<IFormFile> image { get; set; }
    }
}

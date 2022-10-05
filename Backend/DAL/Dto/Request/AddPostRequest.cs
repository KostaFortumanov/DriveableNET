using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto.Request
{
    public class AddPostRequest
    {
        public string title { get; set; }
        public string description { get; set; }
        public List<string> images { get; set; }
        public int horsepower { get; set; }
        public int mileage { get; set; }
        public int manufacturingYear { get; set; }
        public int price { get; set; }
        public bool isNew { get; set; }
        public string color { get; set; }
        public List<string> carType { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
    }
}

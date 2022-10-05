using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto.Response
{
    public class AllReportPostsResponse
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int numOfImages { get; set; }
        public DateTime date { get; set; }
        public int horsepower { get; set; }
        public int mileage { get; set; }
        public int manufacturingYear { get; set; }
        public int price { get; set; }
        public bool isNew { get; set; }
        public string color { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phoneNumber { get; set; }
        public Car car { get; set; }
        public IEnumerable<string> carType { get; set; }
    }
}

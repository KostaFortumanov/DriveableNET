using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto.Request
{
    public class GetPostsRequest
    {
        public IEnumerable<String>? carTypes { get; set; }
        public bool? isNew { get; set; }
        public string? manufacturer { get; set; }
        public string? model { get; set; }
        public int? priceFrom { get; set; }
        public int? priceTo { get; set; }
        public int? yearFrom { get; set; }
        public int? yearTo { get; set; }
        public string? sortBy { get; set; }
        public string? color { get; set; }
        public int? mileageBelow { get; set; }
        public int? Page { get; set; }
        public int? Size { get; set; }
    }
}

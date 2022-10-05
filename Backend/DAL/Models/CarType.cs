using DAL.Models.Relations;
namespace DAL.Models
{
    public class CarType
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<PostCarType> PostCarTypes { get; set; }
    }
}

using DAL.Models.Relations;

namespace DAL.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public ICollection<Images> Images { get; set; }
        public DateTime Date { get; set; }
        public int Horsepower { get; set; }
        public int Mileage { get; set; }
        public int ManufacturingYear { get; set; }
        public int Price { get; set; }
        public bool IsNew { get; set; }
        public string Color { get; set; }
        public Guid CreatorId { get; set; }
        public virtual AppUser Creator { get; set; }
        public Guid CarId { get; set; }
        public Car car { get; set; }
        public virtual ICollection<PostCarType> PostCarTypes { get; set; }
    }
}

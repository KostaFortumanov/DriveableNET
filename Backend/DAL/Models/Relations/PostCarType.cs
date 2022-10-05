namespace DAL.Models.Relations
{
    public class PostCarType
    {
      
        public Guid PostId { get; set; }
        public virtual Post post { get; set; }
        public Guid CarTypeId { get; set; }
        public virtual CarType CarType { get; set; }

    }
}

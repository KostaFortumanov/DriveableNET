namespace DAL.Models
{
    public class Images
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; }
        public Guid PostId { get; set; }
        public virtual Post post { get;set; }
    }
}

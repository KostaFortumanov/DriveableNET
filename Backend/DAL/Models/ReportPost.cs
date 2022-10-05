namespace DAL.Models
{
    public class ReportPost
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public virtual Post post { get; set; }
        public ICollection<Reason> Reasons { get; set; }
    }
}

namespace DAL.Models
{
    public class Reason
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser appUser { get; set; }
       // public Guid ReportPostId { get; set; }
       // public ReportPost reportPost { get; set; }
    }
}

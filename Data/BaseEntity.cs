namespace LMS.Web.Data
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }

        public DateTime DeteCreated { get; set; }
        public DateTime? DeteModified { get; set; }
    }
}

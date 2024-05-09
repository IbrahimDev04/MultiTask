namespace MultiShop.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool isDelete { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set;}
        public DateTime DeletedTime { get; set; }
    }
}

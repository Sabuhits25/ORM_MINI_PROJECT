namespace ORM_MINI_PROJECT.Models
{
    public class Payment:BaseEntity
    {
       
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get;  set; }
    }
}

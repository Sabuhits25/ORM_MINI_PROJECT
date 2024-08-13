using ORM_MINI_PROJECT.Enum;

namespace ORM_MINI_PROJECT.Models
{
    public class Order:BaseEntity
    {

        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public object OrderDetails { get;  set; }
        public List<OrderDetail> Details { get; set; }
    }
}

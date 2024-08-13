using ORM_MINI_PROJECT.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM_MINI_PROJECT.Models
{
    public class Order:BaseEntity
    {

        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        [NotMapped]
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}

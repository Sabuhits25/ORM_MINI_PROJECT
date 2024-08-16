using ClosedXML.Excel;
using ORM_MINI_PROJECT.Enum;
using ORM_MINI_PROJECT.Models;

namespace ORM_PROJECT.DTO_s
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public XLCellValue OrderId { get;  set; }
        public XLCellValue CustomerName { get;  set; }
        public XLCellValue ProductName { get; set; }
        public XLCellValue Quantity { get; set; }
        public XLCellValue Price { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }   
}

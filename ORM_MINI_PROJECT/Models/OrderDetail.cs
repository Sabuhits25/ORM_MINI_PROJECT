namespace ORM_MINI_PROJECT.Models
{
    public class OrderDetail:BaseEntity
    {
        
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public object UnitPrice { get;  set; }
        public int Price { get;  set; }
    }
}

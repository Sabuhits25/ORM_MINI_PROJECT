using ORM_MINI_PROJECT.Enum;
using ORM_MINI_PROJECT.Exceptions;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Services.Interfaces;
using ORM_PROJECT.DTO_s;

namespace ORM_MINI_PROJECT.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private List<Order> _orders = new List<Order>();

        public void CreateOrder(Order order)
        {
            if (order.TotalAmount < 0)
                throw new InvalidOrderException("Sifariş məbləği mənfi ola bilməz.");

            order.Status = OrderStatus.Pending;
            _orders.Add(order);
        }

        public void CancelOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                throw new NotFoundException("Ləğv edilmək istənilən sifariş tapılmadı.");
            if (order.Status == OrderStatus.Cancelled)
                throw new OrderAlreadyCompletedException("Sifariş artıq ləğv edilib.");

            order.Status = OrderStatus.Cancelled;
        }

        public void CompleteOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                throw new NotFoundException("Bitirmək istənilən sifariş tapılmadı.");
            if (order.Status == OrderStatus.Completed)
                throw new OrderAlreadyCompletedException("Sifariş artıq bitirilib.");

            order.Status = OrderStatus.Completed;
        }

        public List<Order> GetOrders()
        {
            return _orders;
        }

        public void AddOrderDetail(int orderId, OrderDetail detail)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                throw new NotFoundException("Mövcud olmayan sifarişə sifariş detalı əlavə edilə bilməz.");
            if (detail.Quantity <= 0 || detail.Price < 0)
                throw new InvalidOrderDetailException("Məhsulun miqdarı sıfırdan az və ya qiyməti mənfi ola bilməz.");

            order.OrderDetails.Add(detail);
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                throw new OrderNotFoundException("Verilmiş ID ilə sifariş tapılmadı.");

            return order.OrderDetails.ToList();
        }

        public void CreateOrder(OrderDTO orderDto)
        {
            
        }

        public IEnumerable<object> GetOrdersByUserId(int id)
        {
            throw new NotImplementedException();
        }
    }
}

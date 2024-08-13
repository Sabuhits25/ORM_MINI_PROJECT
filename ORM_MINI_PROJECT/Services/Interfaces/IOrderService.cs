using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.Services.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(Order order);
        void CancelOrder(int orderId);
        void CompleteOrder(int orderId);
        List<Order> GetOrders();
        void AddOrderDetail(int orderId, OrderDetail detail);
        List<OrderDetail> GetOrderDetailsByOrderId(int orderId);
    }
}

using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.Services.Interfaces
{
    public interface IOrderDetailService
    {
        void CreateOrderDetail(OrderDetail orderDetail);
        OrderDetail GetOrderDetailById(int id);
        IEnumerable<OrderDetail> GetAllOrderDetails();
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(int id);
    }
}

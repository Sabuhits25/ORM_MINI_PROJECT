using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Services.Interfaces;

namespace ORM_MINI_PROJECT.Services.Implementations
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly List<OrderDetail> _orderDetails = new List<OrderDetail>();

        public void CreateOrderDetail(OrderDetail orderDetail)
        {
            if (orderDetail == null)
            {
                throw new ArgumentNullException(nameof(orderDetail));
            }
            _orderDetails.Add(orderDetail);
        }

        public OrderDetail GetOrderDetailById(int id)
        {
            return _orderDetails.FirstOrDefault(od => od.Id == id);
        }

        public IEnumerable<OrderDetail> GetAllOrderDetails()
        {
            return _orderDetails;
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            var existingOrderDetail = GetOrderDetailById(orderDetail.Id);
            if (existingOrderDetail == null)
            {
                throw new ArgumentException("OrderDetail not found.");
            }
            existingOrderDetail.ProductId = orderDetail.ProductId;
            existingOrderDetail.Quantity = orderDetail.Quantity;
            existingOrderDetail.Price = orderDetail.Price;
        }

        public void DeleteOrderDetail(int id)
        {
            var orderDetail = GetOrderDetailById(id);
            if (orderDetail == null)
            {
                throw new ArgumentException("OrderDetail not found.");
            }
            _orderDetails.Remove(orderDetail);
        }
    }

}

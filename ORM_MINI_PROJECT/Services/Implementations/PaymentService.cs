using ORM_MINI_PROJECT.DTO_s;
using ORM_MINI_PROJECT.Enum;
using ORM_MINI_PROJECT.Exceptions;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Services.Interfaces;

namespace ORM_MINI_PROJECT.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private List<Payment> _payments = new List<Payment>();
        private List<Order> _orders = new List<Order>();

        public void MakePayment(Payment payment)
        {
            if (payment.Amount <= 0)
                throw new InvalidPaymentException("Ödəniş məbləği sıfırdan az ola bilməz.");

            var order = _orders.FirstOrDefault(o => o.Id == payment.OrderId);
            if (order == null)
                throw new NotFoundException("Ödəniş ediləcək sifariş tapılmadı.");

            _payments.Add(payment);
            order.Status = OrderStatus.Completed;
        }

        public List<Payment> GetPayments()
        {
            return _payments;
        }

        public IEnumerable<object> GetPaymentsByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public void AddPayment(PaymentDTO paymentDTO)
        {
            throw new NotImplementedException();
        }
    }
}

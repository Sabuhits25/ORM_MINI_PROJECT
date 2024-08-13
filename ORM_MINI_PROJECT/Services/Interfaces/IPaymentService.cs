using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.Services.Interfaces
{
    public interface IPaymentService
    {
        void MakePayment(Payment payment);
        List<Payment> GetPayments();
        IEnumerable<object> GetPaymentsByOrderId(int orderId);
    }

}

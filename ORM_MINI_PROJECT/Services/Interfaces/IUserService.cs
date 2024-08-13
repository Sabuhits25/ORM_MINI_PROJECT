using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(User user);
        void LoginUser(string username, string password);
        void UpdateUserInfo(User user);
        List<Order> GetUserOrders(int userId);
        void ExportUserOrdersToExcel(int userId);
    }
}

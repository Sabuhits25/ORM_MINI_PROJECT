using ORM_MINI_PROJECT.DTO_s;
using ORM_MINI_PROJECT.Models;
using ORM_PROJECT.DTO_s;

namespace ORM_MINI_PROJECT.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(User user);
        void LoginUser(string username, string password);
        void UpdateUserInfo(User user);
        List<Order> GetUserOrders(int userId);
        void ExportUserOrdersToExcel(int userId);
        void Register(UserDTO userDTO);
       
        void UpdateUser(UserDTO userDTO);
        void ExportOrdersToExcel(Task<List<OrderDTO>> orders);
        UserDTO Login(string? email, string password);
    }
}

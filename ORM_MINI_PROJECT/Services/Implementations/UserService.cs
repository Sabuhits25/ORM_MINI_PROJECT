using ORM_MINI_PROJECT.Exceptions;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Services.Interfaces;

namespace ORM_MINI_PROJECT.Services.Implementations
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>();
        private List<Order> _orders = new List<Order>();

        public void RegisterUser(User user)
        {
            if (string.IsNullOrWhiteSpace((string?)user.Username) || string.IsNullOrWhiteSpace(user.Password))
                throw new InvalidUserInformationException("Qeydiyyat məlumatları tam deyil.");

            _users.Add(user);
        }

        public void LoginUser(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
                throw new UserAuthenticationException("İstifadəçi adı və ya şifrə yanlışdır.");
        }

        public void UpdateUserInfo(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
                throw new NotFoundException("Yeniləmək istədiyiniz istifadəçi tapılmadı.");

            existingUser.Username = user.Username;
            existingUser.Password = user.Password;
        }

        public List<Order> GetUserOrders(int userId)
        {
            return _orders.Where(o => o.UserId == userId).ToList();
        }

        public void ExportUserOrdersToExcel(int userId)
        {
            var orders = GetUserOrders(userId);
            // Excel export prosesi burda implementasiya olunacaq
        }
    }
}

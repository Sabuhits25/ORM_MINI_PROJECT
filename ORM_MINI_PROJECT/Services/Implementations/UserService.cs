using ClosedXML.Excel;
using ORM_MINI_PROJECT.DTO_s;
using ORM_MINI_PROJECT.Exceptions;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repositories.Interfaces;
using ORM_PROJECT.DTO_s;
using ORM_PROJECT.Repositories.Implementations;
using System.Security.Authentication;

namespace ORM_MINI_PROJECT.Services.Implementations
{
    public class UserService 
    {
      
        private readonly IUserRepository _repository;

        public UserService()
        {
            _repository = new UserRepository();
        }


        public async Task LoginUser(string username, string password)
        {
            var user =await _repository.GetSingleAsync(u => u.Username == username && u.Password == password);
            if (user == null)
                throw new UserAuthenticationException("İstifadeçi adı və ya şifre yanlışdır.");
        }

        public async Task UpdateUserInfo(User user)
        {
            var existingUser =await _repository.GetSingleAsync(u => u.Id == user.Id);
            if (existingUser == null)
                throw new NotFoundException("Yenilemek istediyiniz istifadeçi tapılmadı.");

            existingUser.Username = user.Username;
            existingUser.Password = user.Password;

            _repository.Update(existingUser);
          await  _repository.SaveChangesAsync();
        }

        public async Task<List<Order>> GetUserOrders(int userId)
        {
            var user = await _repository.GetSingleAsync(x => x.Id == userId,"Orders");

            if (user is null)
                throw new NotFoundException("User is not found");

            return user.Orders.ToList();
        }

       
        public async Task Register(UserDTO userDto)
        {
            if (string.IsNullOrEmpty(userDto.FullName) || string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
            {
                throw new InvalidUserInformationException("İstifadəçi məlumatları yanlışdır.");
            }

            if (await _repository.IsExistAsync(u => u.Email == userDto.Email))
            {
                throw new InvalidUserInformationException("Bu e-poçt artıq istifadə olunur.");
            }


            User user = new()
            {
                Email = userDto.Email,
                FullName = userDto.FullName,
                Address = userDto.Address,
                Password = userDto.Password,
                Username = userDto.Username,

            };

            await _repository.CreateAsync(user);
            await _repository.SaveChangesAsync();
           
        }

        public async Task UpdateUser(UserDTO userDto)
        {
            var existingUser = await _repository.GetSingleAsync(u => u.Id == userDto.Id);
            if (existingUser == null)
            {
                throw new NotFoundException("İstifadəçi tapılmadı.");
            }

            if (!string.IsNullOrEmpty(userDto.FullName))
            {
                existingUser.FullName = userDto.FullName;
            }
            if (!string.IsNullOrEmpty(userDto.Email))
            {
                existingUser.Email = userDto.Email;
            }
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                existingUser.Password = userDto.Password;
            }
        }


     

        public async Task<UserDTO> Login(string email, string password)
        {
            var user =await _repository.GetSingleAsync(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                throw new AuthenticationException("E-poçt və ya şifrə yanlışdır.");
            }


            Console.WriteLine($"Welcome {user.FullName}");

            UserDTO dto = new()
            {
                 FullName=user.FullName,
                 Email=user.Email,
                 Address=user.Address,
                 Password=user.Password,
                 Username=user.Username
            };

            return dto;
        }

        public async Task ExportOrdersToExcel(Task<List<OrderDTO>> ordersTask)
        {
            
            var orders = await ordersTask;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Orders");

               
                worksheet.Cell(1, 1).Value = "Order ID";
                worksheet.Cell(1, 2).Value = "Customer Name";
                worksheet.Cell(1, 3).Value = "Product Name";
                worksheet.Cell(1, 4).Value = "Quantity";
                worksheet.Cell(1, 5).Value = "Price";
                worksheet.Cell(1, 6).Value = "Order Date";

               
                for (int i = 0; i < orders.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = orders[i].OrderId;
                    worksheet.Cell(i + 2, 2).Value = orders[i].CustomerName;
                    worksheet.Cell(i + 2, 3).Value = orders[i].ProductName;
                    worksheet.Cell(i + 2, 4).Value = orders[i].Quantity;
                    worksheet.Cell(i + 2, 5).Value = orders[i].Price;
                    worksheet.Cell(i + 2, 6).Value = orders[i].OrderDate.ToString("yyyy-MM-dd");
                }

               
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    await File.WriteAllBytesAsync("Orders.xlsx", stream.ToArray());
                }
            }
        }
    }
}

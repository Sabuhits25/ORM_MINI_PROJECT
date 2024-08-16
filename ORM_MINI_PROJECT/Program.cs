using ORM_MINI_PROJECT.DTO_s;
using ORM_MINI_PROJECT.Enum;
using ORM_MINI_PROJECT.Exceptions;
using ORM_MINI_PROJECT.Services.Implementation;
using ORM_MINI_PROJECT.Services.Implementations;
using ORM_MINI_PROJECT.Services.Interfaces;
using ORM_PROJECT.DTO_s;
using System.Security.Authentication;

class Program
{
    static async Task Main(string[] args)
    {

        var productService = new ProductService();
        var userService = new UserService();
        var orderService = new OrderService();
        var paymentService = new PaymentService();

        await UserMenu(productService, userService, orderService, paymentService);
        //await MainMenu(productService, userService, orderService, paymentService);

    }

    private static async Task MainMenu(ProductService productService, UserService userService, IOrderService orderService, IPaymentService paymentService)
    {

        while (true)
        {
            Console.WriteLine("Mini E-commerce Tetbiqine Xoş Geldiniz");
            Console.WriteLine("1. Mehsulları idare et");
            Console.WriteLine("2. İstifadeçileri idare et");
            Console.WriteLine("3. Sifarişleri idare et");
            Console.WriteLine("4. Ödenişleri idare et");
            Console.WriteLine("0. Çıxış");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ProductMenu(productService);
                    break;
                case "2":
                    await UserMenu(productService, userService, orderService, paymentService);
                    break;
                case "3":
                    OrderMenu(orderService);
                    break;
                case "4":
                    PaymentMenu(paymentService);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Yanlış seçim, yeniden cehd edin.");
                    break;
            }
        }
    }

    private static async void ProductMenu(ProductService productService)
    {
        while (true)
        {
            Console.WriteLine("Mehsul İdareetme");
            Console.WriteLine("1. Yeni Mehsul Elave Et");
            Console.WriteLine("2. Mehsul Yenile");
            Console.WriteLine("3. Mehsul Sil");
            Console.WriteLine("4. Bütün Mehsulları Göster");
            Console.WriteLine("5. Mehsul Axtar");
            Console.WriteLine("6. Mehsulun Detallarını Göster");
            Console.WriteLine("0. Esas Menyuya Qayıt");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddProduct(productService);
                    break;
                case "2":
                    UpdateProduct(productService);
                    break;
                case "3":
                    await DeleteProduct(productService);
                    break;
                case "4":
                    await GetProducts(productService);
                    break;
                case "5":
                  await  SearchProducts(productService);
                    break;
                case "6":
                  await  GetProductById(productService);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Yanlış seçim, yeniden cehd edin.");
                    break;
            }
        }
    }

    private static async void AddProduct(ProductService productService)
    {
        Console.WriteLine("Mehsulun adı:");
        var name = Console.ReadLine();

        Console.WriteLine("Mehsulun qiymeti:");
        if (!decimal.TryParse(Console.ReadLine(), out var price))
        {
            Console.WriteLine("Yanlış qiymet formatı.");
            return;
        }

        Console.WriteLine("Mehsulun anbar sayı:");
        if (!int.TryParse(Console.ReadLine(), out var stock))
        {
            Console.WriteLine("Yanlış anbar sayı formatı.");
            return;
        }

        Console.WriteLine("Mehsul haqqında elave melumat:");
        var description = Console.ReadLine();

        try
        {

            await productService.AddProduct(new ProductDTO
            {

                Name = name,
                Price = price,
                Stock = stock,
                Description = description
            });
            Console.WriteLine("Mehsul uğurla elave edildi.");
        }
        catch (InvalidProductException)
        {
            Console.WriteLine("Mehsul elave edilerken sehv baş verdi.");
        }
    }

    private static async void UpdateProduct(ProductService productService)
    {
        Console.WriteLine("Yenilemek istediyiniz mehsulun ID-si:");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Yanlış ID formatı.");
            return;
        }

        Console.WriteLine("Yeni mehsul adı (boş buraxmaqla deyişilmeyecek):");
        var name = Console.ReadLine();

        Console.WriteLine("Yeni mehsul qiymeti (boş buraxmaqla deyişilmeyecek):");
        var priceInput = Console.ReadLine();
        decimal? price = null;
        if (!string.IsNullOrEmpty(priceInput) && decimal.TryParse(priceInput, out var parsedPrice))
        {
            price = parsedPrice;
        }

        Console.WriteLine("Yeni anbar sayı (boş buraxmaqla deyişilmeyecek):");
        var stockInput = Console.ReadLine();
        int? stock = null;
        if (!string.IsNullOrEmpty(stockInput) && int.TryParse(stockInput, out var parsedStock))
        {
            stock = parsedStock;
        }

        Console.WriteLine("Yeni mehsul haqqında melumat (boş buraxmaqla dyişilmeyecek):");
        var description = Console.ReadLine();

        try
        {
           await productService.UpdateProduct(new ProductDTO
            {
                Id = id,
                Name = name,
                Price = (decimal)price,
                Stock = (int)stock,
                Description = description
            });
            Console.WriteLine("Mehsul uğurla yenilendi.");
        }
        catch (NotFoundException)
        {
            Console.WriteLine("Mehsul tapılmadı.");
        }
    }

    private static async Task DeleteProduct(ProductService productService)
    {
        Console.WriteLine("Silinmek istediyiniz mehsulun ID-si:");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Yanlış ID formatı.");
            return;
        }

        try
        {
         await    productService.DeleteProduct(id);
            Console.WriteLine("Mehsul uğurla silindi.");
        }
        catch (NotFoundException)
        {
            Console.WriteLine("Mehsul tapılmadı.");
        }
    }

    private static async Task GetProducts(ProductService productService)
    {
        var products =await productService.GetProducts();
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id}. {product.Name} - {product.Price} - {product.Stock} eded");
        }
    }

    private static async Task SearchProducts(ProductService productService)
    {
        Console.WriteLine("Axtarış üçün mehsul adı daxil edin:");
        var searchName = Console.ReadLine();
        var products =await productService.SearchProducts(searchName);

        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id}. {product.Name} - {product.Price} - {product.Stock} eded");
        }
    }

    private static async Task GetProductById(ProductService productService)
    {
        Console.WriteLine("Göstermek istediyiniz mehsulun ID-si:");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Yanlış ID formatı.");
            return;
        }

        try
        {
            var product = await productService.GetProductById(id);
            Console.WriteLine($"Adı: {product.Name}, Qiymet: {product.Price}, Anbar Sayı: {product.Stock}, Melumat: {product.Description}");
        }
        catch (NotFoundException)
        {
            Console.WriteLine("Mehsul tapılmadı.");
        }
    }

    private static async Task UserMenu(ProductService productService, UserService userService, IOrderService orderService, IPaymentService paymentService)
    {
        while (true)
        {
        restart:
            Console.WriteLine("İstifadeçi İdareetme");
            Console.WriteLine("1. Yeni İstifadeçi Qeydiyyatı");
            Console.WriteLine("2. İstifadeçi Girişi");
            Console.WriteLine("3. İstifadeçi Melumatlarını Yenile");
            Console.WriteLine("4. İstifadeçi Sifarişlerini Göster");
            Console.WriteLine("5. İstifadeçi Sifarişlerini Excel-e Export Et");
            Console.WriteLine("0. Esas Menyuya Qayıt");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    RegisterUser(userService);
                    goto restart;
                case "2":
                    await LoginUser(productService, userService, orderService, paymentService);
                    break;
                case "3":
                    UpdateUser(userService);
                    goto restart;
                case "4":
                    ShowUserOrders(userService, orderService);
                    goto restart;
                case "5":
                    ExportUserOrdersToExcel(userService, orderService);
                    goto restart;
                case "0":
                    goto restart;
                default:
                    Console.WriteLine("Yanlış seçim, yeniden cehd edin.");
                    goto restart;
            }
        }
    }

    private static async void RegisterUser(UserService userService)
    {
        Console.WriteLine("Yeni istifadeçi üçün melumatlar daxil edin:");

        string name;
        string email;
        string password;
        string address;
        string username;

        Console.WriteLine("Ad:");
        name = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Ad boş ola bilmez. Yeniden daxil edin:");
            name = Console.ReadLine();
        }

        Console.WriteLine("Username:");
        username = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Username boş ola bilmez. Yeniden daxil edin:");
            username = Console.ReadLine();
        }

        Console.WriteLine("E-poçt:");
        email = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            Console.WriteLine("Düzgün e-poçt ünvanı daxil edin:");
            email = Console.ReadLine();
        }

        Console.WriteLine("Address:");
        address = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(address))
        {
            Console.WriteLine("Address boş ola bilmez. Yeniden daxil edin:");
            address = Console.ReadLine();
        }

        Console.WriteLine("Şifre:");
        password = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
        {
            Console.WriteLine("Şifre en azı 8 simvol, böyük ve kiçik herf, hemçinin reqemden ibaret olmalıdır. Yeniden daxil edin:");
            password = Console.ReadLine();
        }

        try
        {
            await userService.Register(new UserDTO
            {
                FullName = name,
                Email = email,
                Password = password,
                Username = username,
                Address = address
            });
            Console.WriteLine("İstifadeçi uğurla qeydiyyatdan keçdi.");
        }
        catch (InvalidUserInformationException)
        {
            Console.WriteLine("İstifadeçi qeydiyyatı zamanı sehv baş verdi.");
        }
    }

    private static bool IsValidPassword(string password)
    {
        if (password.Length < 8) return false;

        bool hasUpperCase = false;
        bool hasLowerCase = false;
        bool hasDigit = false;

        foreach (char c in password)
        {
            if (char.IsUpper(c)) hasUpperCase = true;
            if (char.IsLower(c)) hasLowerCase = true;
            if (char.IsDigit(c)) hasDigit = true;
        }

        return hasUpperCase && hasLowerCase && hasDigit;
    }

    private static async Task LoginUser(ProductService productService, UserService userService, IOrderService orderService, IPaymentService paymentService)
    {
        Console.WriteLine("E-poçt:");
        var email = Console.ReadLine();

        Console.WriteLine("Şifre:");
        var password = Console.ReadLine();

        try
        {
            var user = await userService.Login(email, password);
            Console.WriteLine($"Xoş gelmisiniz, {user.FullName}!");


            await MainMenu(productService, userService, orderService, paymentService);

            UserDashboard(user, orderService);

        }
        catch (AuthenticationException ex)
        {
            Console.WriteLine(ex.Message);

        }
    }

    private static void UpdateUser(UserService userService)
    {
        Console.WriteLine("İstifadeçi ID-si:");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Yanlış ID formatı.");
            return;
        }

        Console.WriteLine("Yeni ad (boş buraxmaqla deyişilmeyecek):");
        var name = Console.ReadLine();

        Console.WriteLine("Yeni e-poçt (boş buraxmaqla deyişilmeyecek):");
        var email = Console.ReadLine();

        Console.WriteLine("Yeni şifre (boş buraxmaqla deyişilmeyecek):");
        var password = Console.ReadLine();

        try
        {
            userService.UpdateUser(new UserDTO
            {
                Id = id,
                FullName = name,
                Email = email,
                Password = password
            });
            Console.WriteLine("İstifadeçi melumatları uğurla yenilendi.");
        }
        catch (NotFoundException)
        {
            Console.WriteLine("İstifadeçi tapılmadı.");
        }
    }

    private static async void ShowUserOrders(UserService userService, IOrderService orderService)
    {
        Console.WriteLine("İstifadeçi ID-si:");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Yanlış ID formatı.");
            return;
        }

        try
        {
            var orders = await orderService.GetOrdersByUserId(id);
            foreach (var order in orders)
            {
                Console.WriteLine($"Sifariş ID: {order.Id}, Mebleğ: {order.TotalAmount}, Status: {order.Status}");
            }
        }
        catch (NotFoundException)
        {
            Console.WriteLine("İstifadeçi və ya sifarişler tapılmadı.");
        }
    }

    private static void ExportUserOrdersToExcel(UserService userService, IOrderService orderService)
    {
        Console.WriteLine("İstifadeçi ID-si:");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Yanlış ID formatı.");
            return;
        }

        try
        {
            var orders = orderService.GetOrdersByUserId(id);
            userService.ExportOrdersToExcel(orders);
            Console.WriteLine("Sifarişler uğurla Excel-e export edildi.");
        }
        catch (NotFoundException)
        {
            Console.WriteLine("İstifadeçi ve ya sifarişler tapılmadı.");
        }
    }

    private static void UserDashboard(UserDTO user, IOrderService orderService)
    {
        while (true)
        {
            Console.WriteLine($"İstifadeçi Dashboard - {user.FullName}");
            Console.WriteLine("1. Yeni Sifariş Yarat");
            Console.WriteLine("2. Sifarişleri Görüntüle");
            Console.WriteLine("0. Çıxış");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateOrder(orderService, user.Id);
                    break;
                case "2":
                    ShowUserOrders(orderService, user.Id);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Yanlış seçim, yeniden cehd edin.");
                    break;
            }
        }
    }

    private static void CreateOrder(IOrderService orderService, int userId)
    {
        try
        {
        Console.WriteLine("Sifariş mebleğini daxil edin:");
        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Yanlış mebleğ formatı.");
            return;
        }

        
            orderService.CreateOrder(new OrderDTO
            {
                UserId = userId,
                TotalAmount = amount,
                Status = OrderStatus.Pending
            });
            Console.WriteLine("Sifariş uğurla yaradıldı.");
        }
        
        catch (NotImplementedException ex)
        {
            Console.WriteLine("Sifariş yaradılarken sehv baş verdi.");
        }
    }

    private static async Task ShowUserOrders(IOrderService orderService, int userId)
    {
        var orders = await orderService.GetOrdersByUserId(userId);
        foreach (var order in orders)
        {
            Console.WriteLine($"Sifariş ID: {order.Id}, Mebleğ: {order.TotalAmount}, Status: {order.Status}");
        }
    }

    private static void OrderMenu(IOrderService orderService)
    {
        while (true)
        {
            Console.WriteLine("Sifariş İdareetme");
            Console.WriteLine("1. Sifarişleri Göster");
            Console.WriteLine("2. Sifarişin Statusunu Yenile");
            Console.WriteLine("3. Sifarişi Sil");
            Console.WriteLine("0. Esas Menyuya Qayıt");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    GetOrders(orderService);
                    break;
                case "2":
                    UpdateOrderStatus(orderService);
                    break;
                case "3":
                    DeleteOrder(orderService);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Yanlış seçim, yeniden cehd edin.");
                    break;
            }
        }
    }

    private static void GetOrders(IOrderService orderService)
    {
        try
        {
        var orders = orderService.GetOrders();
        foreach (var order in orders)
        {
            Console.WriteLine($"Sifariş ID: {order.Id}, Mebleğ: {order.TotalAmount}, Status: {order.Status}");
        }

        }
        catch (NotImplementedException )
        {
            Console.WriteLine("Sifaris tapilmadi");
           
        }
    }

    private static void UpdateOrderStatus(IOrderService orderService)
    {
        Console.WriteLine("Sifariş ID-si:");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Yanlış ID formatı.");
            return;
        }

        Console.WriteLine("Yeni Status:");
        var status = Console.ReadLine();

        try
        {
            orderService.UpdateOrderStatus(id, status);
            Console.WriteLine("Sifarişin statusu uğurla yenilendi.");
        }
        catch (NotFoundException)
        {
            Console.WriteLine("Sifariş tapılmadı.");
        }
    }

    private static void DeleteOrder(IOrderService orderService)
    {
        try
        {
        Console.WriteLine("Silinmek istediyiniz sifarişin ID-si:");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Yanlış ID formatı.");
            return;
        }

            orderService.DeleteOrder(id);
            Console.WriteLine("Sifariş uğurla silindi.");
        }
        catch (NotFoundException)
        {
            Console.WriteLine("Sifariş tapılmadı.");
        }
        catch (NotImplementedException)
        {
            Console.WriteLine("Sifariş tapılmadı");
        }
    }

    private static void PaymentMenu(IPaymentService paymentService)
    {
        while (true)
        {
            Console.WriteLine("Ödeniş İdareetme");
            Console.WriteLine("1. Ödenişleri Göster");
            Console.WriteLine("2. Yeni Ödeniş elave Et");
            Console.WriteLine("0. Esas Menyuya Qayıt");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    GetPayments(paymentService);
                    break;
                case "2":
                    AddPayment(paymentService);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Yanlış seçim, yeniden cehd edin.");
                    break;
            }
        }
    }

    private static void GetPayments(IPaymentService paymentService)
    {
        var payments = paymentService.GetPayments();
        foreach (var payment in payments)
        {
            Console.WriteLine($"Ödeniş ID: {payment.Id}, Mebleğ: {payment.Amount}, Status: {payment.Status}");
        }
    }

    private static void AddPayment(IPaymentService paymentService)
    {
        Console.WriteLine("Sifariş ID-si:");
        if (!int.TryParse(Console.ReadLine(), out var orderId))
        {
            Console.WriteLine("Yanlış ID formatı.");
            return;
        }

        Console.WriteLine("Ödeniş mebleğini daxil edin:");
        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Yanlış mebleğ formatı.");
            return;
        }

        try
        {
            paymentService.AddPayment(new PaymentDTO
            {
                OrderId = orderId,
                Amount = amount,

            });
            Console.WriteLine("Ödeniş uğurla elave edildi.");
        }
        catch (Exception)
        {
            Console.WriteLine("Ödəniş elave edilerken sehv baş verdi.");
        }
    }
}




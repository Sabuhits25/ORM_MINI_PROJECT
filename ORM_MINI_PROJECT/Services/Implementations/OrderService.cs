using ORM_MINI_PROJECT.Enum;
using ORM_MINI_PROJECT.Exceptions;
using ORM_MINI_PROJECT.IRepositories.Implementation;
using ORM_MINI_PROJECT.IRepositories.Interfaces;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repositories.Interfaces;
using ORM_MINI_PROJECT.Services.Interfaces;
using ORM_PROJECT.DTO_s;
using ORM_PROJECT.Repositories.Implementations;

namespace ORM_MINI_PROJECT.Services.Implementations;

//public class OrderService : IOrderService
//{
//    private List<Order> _orders = new List<Order>();

//    public void CreateOrder(Order order)
//    {
//        if (order.TotalAmount < 0)
//            throw new InvalidOrderException("Sifariş məbləği mənfi ola bilməz.");

//        order.Status = OrderStatus.Pending;
//        _orders.Add(order);
//    }

//    public void CancelOrder(int orderId)
//    {
//        var order = _orders.FirstOrDefault(o => o.Id == orderId);
//        if (order == null)
//            throw new NotFoundException("Ləğv edilmək istənilən sifariş tapılmadı.");
//        if (order.Status == OrderStatus.Cancelled)
//            throw new OrderAlreadyCompletedException("Sifariş artıq ləğv edilib.");

//        order.Status = OrderStatus.Cancelled;
//    }

//    public void CompleteOrder(int orderId)
//    {
//        var order = _orders.FirstOrDefault(o => o.Id == orderId);
//        if (order == null)
//            throw new NotFoundException("Bitirmək istənilən sifariş tapılmadı.");
//        if (order.Status == OrderStatus.Completed)
//            throw new OrderAlreadyCompletedException("Sifariş artıq bitirilib.");

//        order.Status = OrderStatus.Completed;
//    }

//    public List<Order> GetOrders()
//    {
//        return _orders;
//    }

//    public void AddOrderDetail(int orderId, OrderDetail detail)
//    {
//        var order = _orders.FirstOrDefault(o => o.Id == orderId);
//        if (order == null)
//            throw new NotFoundException("Mövcud olmayan sifarişə sifariş detalı əlavə edilə bilməz.");
//        if (detail.Quantity <= 0 || detail.Price < 0)
//            throw new InvalidOrderDetailException("Məhsulun miqdarı sıfırdan az və ya qiyməti mənfi ola bilməz.");

//        order.OrderDetails.Add(detail);
//    }

//    public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
//    {
//        var order = _orders.FirstOrDefault(o => o.Id == orderId);
//        if (order == null)
//            throw new OrderNotFoundException("Verilmiş ID ilə sifariş tapılmadı.");

//        return order.OrderDetails.ToList();
//    }

//    public void CreateOrder(OrderDTO orderDto)
//    {

//    }

//    public IEnumerable<object> GetOrdersByUserId(int id)
//    {
//        throw new NotImplementedException();
//    }

//    Task<OrderDTO> IOrderService.GetOrdersByUserId(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public void DeleteOrder(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public void UpdateOrderStatus(int id, string status)
//    {
//        throw new NotImplementedException();
//    }
//}


public class OrderService:IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IUserRepository _userRepository;
    public OrderService()
    {
        _repository = new OrderRepository();
        _userRepository = new UserRepository();
    }


    public async Task<List<OrderDTO>> GetOrdersByUserId(int id)
    {
        var user = await _userRepository.GetSingleAsync(x => x.Id == id,"Orders");

        if (user is null)
            throw new NotFoundException("User is not found");


        List<OrderDTO> dtos = new();
        foreach (var item in user.Orders)
        {
            OrderDTO dto = new()
            {
                  OrderDate=item.OrderDate,
                   TotalAmount=item.TotalAmount,    
                    Status=item.Status, 
                     UserId=item.UserId
                      
            };

            dtos.Add(dto);
        }

        return dtos;
    }



    public async Task CreateOrder(Order order)
    {
        if (order.TotalAmount < 0)
            throw new InvalidOrderException("Sifariş məbləği mənfi ola bilməz.");

        order.Status = OrderStatus.Pending;
        
        await _repository.CreateAsync(order);
    }

    public async void CancelOrder(int orderId)
    {
        var order =await  _repository.GetSingleAsync(o => o.Id == orderId);
        if (order == null)
            throw new NotFoundException("Ləğv edilmək istənilən sifariş tapılmadı.");
        if (order.Status == OrderStatus.Cancelled)
            throw new OrderAlreadyCompletedException("Sifariş artıq ləğv edilib.");

        order.Status = OrderStatus.Cancelled;

        _repository.Update(order);
        await _repository.SaveChangesAsync();
    }

    public async Task CompleteOrder(int orderId)
    {
        var order =await _repository.GetSingleAsync(o => o.Id == orderId);
        if (order == null)
            throw new NotFoundException("Bitirmək istənilən sifariş tapılmadı.");
        if (order.Status == OrderStatus.Completed)
            throw new OrderAlreadyCompletedException("Sifariş artıq bitirilib.");

        order.Status = OrderStatus.Completed;

        _repository.Update(order);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<Order>> GetOrders()
    {
        return await _repository.GetAllAsync();
    }

    public async void AddOrderDetail(int orderId, OrderDetail detail)
    {
        var order =await _repository.GetSingleAsync(o => o.Id == orderId,"OrderDetails");
        if (order == null)
            throw new NotFoundException("Mövcud olmayan sifarişə sifariş detalı əlavə edilə bilməz.");
        if (detail.Quantity <= 0 || detail.Price < 0)
            throw new InvalidOrderDetailException("Məhsulun miqdarı sıfırdan az və ya qiyməti mənfi ola bilməz.");

        order.OrderDetails.Add(detail);

       await _repository.SaveChangesAsync();
    }

    public async Task<List<OrderDetail>> GetOrderDetailsByOrderId(int orderId)
    {
        var order = await _repository.GetSingleAsync(o => o.Id == orderId,"OrderDetails");
        if (order == null)
            throw new OrderNotFoundException("Verilmiş ID ilə sifariş tapılmadı.");

        return order.OrderDetails.ToList();
    }

    void IOrderService.CreateOrder(Order order)
    {
        if (order.TotalAmount < 0)
            throw new InvalidOrderException("Sifariş məbləği mənfi ola bilməz.");

        order.Status = OrderStatus.Pending;

        _repository.CreateAsync(order).Wait();
    }

    void IOrderService.CompleteOrder(int orderId)
    {
        var order = _repository.GetSingleAsync(o => o.Id == orderId).Result;
        if (order == null)
            throw new NotFoundException("Bitirmək istənilən sifariş tapılmadı.");
        if (order.Status == OrderStatus.Completed)
            throw new OrderAlreadyCompletedException("Sifariş artıq bitirilib.");

        order.Status = OrderStatus.Completed;

        _repository.Update(order);
        _repository.SaveChangesAsync().Wait(); 
    }

    List<Order> IOrderService.GetOrders()
    {
        return _repository.GetAllAsync().Result;
    }

    List<OrderDetail> IOrderService.GetOrderDetailsByOrderId(int orderId)
    {
        var order = _repository.GetSingleAsync(o => o.Id == orderId, "OrderDetails").Result;
        if (order == null)
            throw new OrderNotFoundException("Verilmiş ID ilə sifariş tapılmadı.");

        return order.OrderDetails.ToList();
    }

    public void CreateOrder(OrderDTO orderDto)
    {
        var order = new Order
        {
            UserId = orderDto.UserId,
            OrderDate = DateTime.Now,
            Status = OrderStatus.Pending,
            TotalAmount = orderDto.TotalAmount,
            OrderDetails = orderDto.OrderDetails.Select(od => new OrderDetail
            {
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                Price = od.Price
            }).ToList()
        };

        _repository.CreateAsync(order).Wait(); 
    }

    public void DeleteOrder(int id)
    {
        var order = _repository.GetSingleAsync(o => o.Id == id).Result;
        if (order == null)
            throw new NotFoundException("Sifariş tapılmadı.");

        _repository.Delete(order);
        _repository.SaveChangesAsync().Wait(); 
    }

   

    public void UpdateOrderStatus(int id, string? status)
    {

        var order = _repository.GetSingleAsync(o => o.Id == id).Result;
        if (order == null)
        {
            throw new NotFoundException("Sifariş tapılmadı.");
        }

            order.Status = Enum.OrderStatus.Cancelled;
            _repository.Update(order);

        _repository.SaveChangesAsync().Wait();
    }
}

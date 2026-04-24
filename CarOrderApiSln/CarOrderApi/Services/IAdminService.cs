using CarOrderApi.Dtos.Admin;
using CarOrderApi.Model;

namespace CarOrderApi.Services
{
    public interface IAdminService
    {//Car info:
        Task<IEnumerable<Car>> GetCarAsync();
        Task<Car> AddCarAsync(CarDto car);
        Task<Car> DeleteCar(int id);
        Task<Car> UpdateCar(int id, CarDto car);

        //Order :
        Task<IEnumerable<Order>> GetOrderAsync();
        Task<bool> UpdateOrderStatusAsync(int id, string status);

        //User Info:
        Task<IEnumerable<ApplicationUser>> GetUserAsync();
        Task<bool> ToggleUserStatusAsync(string userId);

        //DashBoard:
        Task<object> GetDashboardAsync();

    }
}

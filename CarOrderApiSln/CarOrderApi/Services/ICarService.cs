using CarOrderApi.Dtos;
using CarOrderApi.Model;

namespace CarOrderApi.Services
{
    public interface ICarService
    {
        Task<CarDto> AddNewCar(CarDto dto);
        Task<CarDto> UpdateCar(CarDto dto,int id);
        Task<bool> DeleteCar(int id);
        Task<IEnumerable<Car>> GetAllCars();
        Task<Car> GetCarById(int id);
        Task<List<Car>> GetCarByName(string query);
    }
}

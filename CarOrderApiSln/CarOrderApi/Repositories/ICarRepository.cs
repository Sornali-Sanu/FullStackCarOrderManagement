using CarOrderApi.Dtos;
using CarOrderApi.Model;

namespace CarOrderApi.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCarAsync();
        Task<Car> GetCarById(int id);
        Task<CarDto> AddNewCar(CarDto car);
        Task<CarDto> UpdateCar(CarDto newCar,int id);
        Task<Car> DeleteCar(int id);

    }
}

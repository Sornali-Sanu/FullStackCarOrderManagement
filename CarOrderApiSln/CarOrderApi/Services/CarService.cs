using CarOrderApi.Dtos;
using CarOrderApi.Model;
using CarOrderApi.Repositories;

namespace CarOrderApi.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repo;

        public CarService(ICarRepository repo)
        {
            _repo = repo;
        }

        public async Task<CarDto> AddNewCar(CarDto dto)
        {
            return await _repo.AddNewCar(dto);
        }

        public async Task<bool> DeleteCar(int id)
        {
            var car = await _repo.DeleteCar(id);
            return car != null;
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await _repo.GetAllCarAsync();
        }

        public async Task<Car> GetCarById(int id)
        {
            return await _repo.GetCarById(id);
        }

        public async Task<CarDto> UpdateCar(CarDto dto,int id)
        {
            return await _repo.UpdateCar(dto, id);
        }
    }
}

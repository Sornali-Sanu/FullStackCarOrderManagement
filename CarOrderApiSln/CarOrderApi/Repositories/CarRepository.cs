using CarOrderApi.Data;
using CarOrderApi.Dtos;
using CarOrderApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CarOrderApi.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CarDto> AddNewCar(CarDto car)
        {
            var newcar = new Car { 
            Name= car.Name,
            Description= car.Description,
            Brand= car.Brand,
            Price= car.Price,
            ImageUrl= car.ImageUrl,
            
            };
            _context.Cars.Add(newcar);
            await _context.SaveChangesAsync();
            return car;


        }

        public async Task<Car> DeleteCar(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(x => x.CarId == id);
            if (car != null) {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                return car;

            }
            return null;

        }

        public async Task<IEnumerable<Car>> GetAllCarAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarById(int id)
        {
            if (id < 0) {
                return null;
            }
            return await _context.Cars.FirstOrDefaultAsync(x => x.CarId == id); 
        }

        public async Task<CarDto> UpdateCar(CarDto newCar,int id)
        {
            var existCar = await _context.Cars.FirstOrDefaultAsync(x=>x.CarId==id);
            if (existCar == null) { return null; }
            existCar.Description = newCar.Description;
            existCar.Name = newCar.Name;
            existCar.Brand  = newCar.Brand;
            existCar.Price= newCar.Price;
            existCar.ImageUrl= newCar.ImageUrl;
            await _context.SaveChangesAsync();
            return newCar;
        }
    }
}

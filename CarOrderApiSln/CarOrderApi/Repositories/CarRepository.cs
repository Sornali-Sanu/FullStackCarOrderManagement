using CarOrderApi.Data;
using CarOrderApi.Dtos;
using CarOrderApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CarOrderApi.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

      
        public async Task<CarDto> AddNewCar(CarDto carModel)
        {
            string imageFileName = null;
            imageFileName = await GetImageFileName(carModel.ProfileImage);
           

            Car newCar= new Car();
            newCar.Name= carModel.Name;
            newCar.Description= carModel.Description;
            newCar.Brand= carModel.Brand;
            newCar.Price= carModel.Price;
            newCar.ImageUrl = imageFileName;
            newCar.BodyType=carModel.BodyType;
            newCar.DriveType = carModel.Drivetype; 
            newCar.Engine=carModel.Engine;
            newCar.AirCon=carModel.AirCon;
            newCar.Gearbox=carModel.Gearbox;
            newCar.FuelType=carModel.FuelType;
            newCar.Color=carModel.Color;
            newCar.Condition=carModel.Condition;
            _context.Cars.Add(newCar);
            await _context.SaveChangesAsync();
            return new CarDto
            {
               
                Name = newCar.Name,
                Description = newCar.Description,
                Brand = newCar.Brand,
                Price = newCar.Price,
                ImageUrl = newCar.ImageUrl,
                BodyType= newCar.BodyType,
                Drivetype=newCar.DriveType,
                Engine=newCar.Engine,
                AirCon=newCar.AirCon,
                Gearbox=newCar.Gearbox,
                FuelType=newCar.FuelType,
                Color=newCar.Color,
                Condition=newCar.Condition,
                
            }; ;


        }

        private async Task<string?> GetImageFileName(IFormFile profileFile)
        {
            string uniqueFileName = null;
            if (profileFile != null) 
            {
                uniqueFileName=Guid.NewGuid().ToString()+Path.GetExtension(profileFile.FileName);
                var uploadFolder = Path.Combine(_env.WebRootPath, "images");
                var filePath=Path.Combine(uploadFolder,uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await profileFile.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task<Car> DeleteCar(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(x => x.CarId == id);
            if (car == null) {

                return null;

            }
            if (!string.IsNullOrEmpty(car.ImageUrl))
            {
                string imagePath = Path.Combine(_env.WebRootPath, "images", car.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return car;

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

            var existCar = await _context.Cars.FirstOrDefaultAsync(x => x.CarId == id);
            if (existCar == null)
                return null;


            if (newCar.ProfileImage != null)
            {

                if (!string.IsNullOrEmpty(existCar.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_env.WebRootPath, "images", existCar.ImageUrl);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }


                existCar.ImageUrl = await GetImageFileName(newCar.ProfileImage);
            }
            else { existCar.ImageUrl = existCar.ImageUrl; }

            
            existCar.Name = newCar.Name;
            existCar.Brand = newCar.Brand;
            existCar.Description = newCar.Description;
            existCar.Price = newCar.Price;

            await _context.SaveChangesAsync();

            
            return new CarDto
            {
                CarId = existCar.CarId,
                Name = existCar.Name,
                Brand = existCar.Brand,
                Description = existCar.Description,
                Price = existCar.Price,
                ImageUrl = existCar.ImageUrl
            };
        }

        public async Task<List<Car>> SearchCarByName(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return new List<Car>();
            }
            return await _context.Cars.Where(c => c.Name.ToLower().Contains(query.ToLower()) || c.Brand.ToLower().Contains(query.ToLower())).ToListAsync();
           
        }
    }
}

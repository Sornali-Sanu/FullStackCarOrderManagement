using CarOrderApi.Data;
using CarOrderApi.Dtos.Admin;
using CarOrderApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CarOrderApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _evn;

        public AdminService(AppDbContext db , IWebHostEnvironment evn)
        {
            _db = db;
            _evn= evn;
        }
        private async Task<string?> GetUserProfileImageName(IFormFile ProfileImage)
        {
            string userFileName = null;
            if (ProfileImage != null)
            {
                //Guid.NewGuid create unique File Name
                userFileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                //Path.Combine create folder location
                var uploadFolder = Path.Combine(_evn.WebRootPath, "images");
                var filePath = Path.Combine(uploadFolder, userFileName);
                //fileStream create an empty file in folder path and copy to async copy the data to the file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(fileStream);
                }

            }
            return userFileName;
        }
        [Authorize("Admin")]
        public async Task<Car> AddCarAsync(AdminCarDto car)
        {
            
            var newCar = new Car {
            
            Name=car.Name,   
            Brand =car.Brand,
            Description =car.Description,
            Price =car.Price,
            ImageUrl=await GetUserProfileImageName(car.ProfileImage), 
         
            Gearbox =car.Gearbox,
            Engine=car.Engine,
            Color=car.Color, 
            FuelType=car.FuelType,
            BodyType=car.BodyType,
            Condition=car.Condition,
            AirCon =car.AirCon,
            DriveType = car.Drivetype,
    };
            _db.Cars.Add(newCar);
            await _db.SaveChangesAsync();
            return newCar;
        }

        public async Task<Car> DeleteCar(int id)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car == null)
            { return null; }
            if (!string.IsNullOrEmpty(car.ImageUrl))
            {
                string imagePath = Path.Combine(_evn.WebRootPath, "Images", car.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();
            return car;
           
        }

        public async Task<IEnumerable<Car>> GetCarAsync()
        {
            return await _db.Cars.ToListAsync();
        }

        public Task<object> GetDashboardAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrderAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ToggleUserStatusAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Car> UpdateCar(int id, AdminCarDto car)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderStatusAsync(int id, string status)
        {
            throw new NotImplementedException();
        }
    }
}

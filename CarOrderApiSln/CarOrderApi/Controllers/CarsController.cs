using CarOrderApi.Dtos;
using CarOrderApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _service;

        public CarsController(ICarService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _service.GetAllCars();
            return Ok(cars);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var car = await _service.GetCarById(id);
            return Ok(car);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddNewCar(CarDto car)
        {
            var newCar=await _service.AddNewCar(car);
            return Ok(newCar);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(CarDto car,int id)
        {
            var newCar = await _service.UpdateCar(car, id);
            if(newCar==null)
            {return NotFound("Car not found");}
            return Ok(newCar);
        }
        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        { 
            bool deletedcar=await _service.DeleteCar(id);
            if (!deletedcar)
                return NotFound("car not found");
            return Ok("Car deleted Successfullly");
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetCarByNameOrBrand(string query)
        {
            var cars=await _service.GetCarByName(query);
            if (cars == null || !cars.Any())
            {
                return NotFound("Match Not Found");
            }
            return Ok(cars);
            
        }
    }
}
    


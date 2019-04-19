using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WheelsApp_Backend.Models;

namespace WheelsApp_Backend.Controllers.Fleet
{
    [Route("api/[fleet]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly WheelsContext _context;

        public CarsController(WheelsContext context)
        {
            _context = context;
        }
        
        /*   <summary>
           -- Get all Cars
            </summary> */
        /* <param name=""></param> */
        [HttpGet]
        [Route("/getCars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }


        /*   <summary>
           -- Get Car by ID
            </summary> */
        /* <param name="ID", Car></param>   */
        [HttpGet]
        [Route("/getCarByID/{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }


        /*   <summary>
           -- Update Car by ID
            </summary> */
        /* <param name="ID", Car></param>   */
        [HttpGet]
        [Route("/updateCarByID/{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Car_Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /*   <summary>
          -- Create Car by ID
            </summary> */
        /* <param name="ID", Car></param>   */
        [HttpPost]
        [Route("/createCar")]
        public async Task<ActionResult<Car>> PostCar(CarViewModel carViewModel)
        {
            var car = new Car
            {
                Colour = carViewModel.Colour,
                Date_Added = DateTime.Now.ToShortDateString().Replace('/', '-'),
                Kms = carViewModel.Kms,
                Make = carViewModel.Make,
                Year = carViewModel.Year,
                Registration = carViewModel.Registration,
                Status = "Parked",
                Tank = carViewModel.Tank
            };

            var isAdded = _context.Cars.Any(e => e.Registration == carViewModel.Registration);
            if (isAdded) { return BadRequest(new
            {
                error = "false",
                message = "Sorry, I already have this vehicle on my records!"
            }); }

            _context.Cars.Add(car);
            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCar", new { id = car.Car_Id }, car);
            } catch (Exception e)
            {
                return BadRequest(new
                {
                    error = "false",
                    message = "Sorry, I couldn't add this vehicle!",
                    discription = e.Message
                });
            }
        }


        

        /*   <summary>
                -- Delete Car by ID
             </summary> */
        /* <param name="ID"></param>   
        [HttpDelete]
        [Route("/deleteCarByID/{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }*/

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Car_Id == id);
        }
    }
}

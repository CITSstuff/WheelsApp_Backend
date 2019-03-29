using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelsApp_Backend.Models;

namespace WheelsApp_Backend.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class UserController : Controller {
        private readonly WheelsContext _context;

        public UserController(WheelsContext context) {
            _context = context;

            if (_context.Users.Count() == 0) {
                // Create a new User if collection is empty,
                // which means you can't delete all Users.
                _context.Users.Add(new User {
                    token = 2,
                    id_number = 1253535353536,
                    telephone = "024 158 2200",
                    first_name = "Olwethu",
                    lasst_name = "Makhuz",
                    role = "client",
                    telephone_2 = "014 112 4525",
                    work_contact = "011 424 7588",
                    next_of_keen = "Details of next of keen",

                  });
                _context.SaveChanges();
            }
        }

        // GET: api/User get all users
        [HttpGet]
        [Route("/getAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id_number}")]
        public ActionResult GetUserWithId(long id_number) { 
            var User = _context.Users.Where(u => u.id_number == id_number).First();

            if (User == null) {
                return NotFound();
            }

            return Ok(User);        
        }

        // GET: api/User/5
        //[HttpGet("{role}")]
        //[Route("/getUserByRole")]
        //public async Task<ActionResult<User>> GetUserWithRole(string role)
        //{
        //    var User = await _context.Users.FindAsync(role);

        //    if (User == null)
        //    {
        //        return NotFound();
        //    }

        //    return User;
        //}

    }
}
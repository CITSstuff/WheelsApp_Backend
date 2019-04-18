using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WheelsApi.Helper;
using WheelsApp_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WheelsApp_Backend.Controllers
{
    [Route("api/[passwords]")]
    [ApiController]
    public class PasswordsController : Controller {

        private readonly WheelsContext _context;

        public PasswordsController(WheelsContext context)
        {
            _context = context;
        }

        /* <summary>
             -- Update User password
           </summary> *
        [HttpPut]
        [Route("/updatePassword")] */


        /* <summary>
             -- Update User password
           </summary> */
        [HttpPost]
        [Route("/setPasswordBackup/{id}")]
        public async Task<ActionResult<Client>> SetPasswordBackup(long id, Passwords passwords)
        {

                try {

                var securedPass = Helper.Hash(passwords.Password);
                    var isAuth = _context.Users.Any(u => u.User_Id == id && u.Password == securedPass);
                    if(isAuth)
                    {

                        _context.Passwords.Add(passwords);
                        await _context.SaveChangesAsync();

                        return Ok(passwords);

                    }
                    return NotFound(new
                    {
                        error = "true",
                        message = "Provided data does not match on Database"
                    });
                } catch (Exception e) {
                    return BadRequest(new
                    {
                        error = "true",
                        message = "An error occured while registrering",
                        Discription = e.Message
                    });
                }
            }
        /* return BadRequest(new
        {
            error = "true",
            message = "Sorry, Backup requeires all fileds. ..",
        });*/



        /* <summary>
             -- Update User password
           </summary> *
        [HttpPut]
        [Route("/updatePassword")]
        public void UpdatePassword(int id, [FromBody]string value) {

        } */


        /* <summary>
             -- Delete password backup by ID
           </summary> */
        [HttpDelete]
        [Route("/deleteBackUp/{id}")]
        public void DeleteBackUp(int id)
        {
        }
    }
}

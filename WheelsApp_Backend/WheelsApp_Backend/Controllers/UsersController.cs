using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WheelsApi.Helper;
using WheelsApp_Backend.Models;

namespace WheelsApp_Backend.Controllers {
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly WheelsContext wheelsContext;
        private IConfiguration _configuration;

        public UsersController(WheelsContext wContext, IConfiguration configuration) {
            wheelsContext = wContext;
            _configuration = configuration;

        }

        /*   <summary>
           -- Registers the user for the web app
            </summary> */
        /* <param name="userViewModel"></param>   */
        [AllowAnonymous]
        [HttpPost]
        [Route("/register")]
        public async Task<ActionResult<User>> CreateUser(UserViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var existingUser = wheelsContext.Users.Where(u => u.Id_number == userViewModel.Id_number).FirstOrDefault();
                    var existingUserByEmail = wheelsContext.Users.Where(u => u.Email == userViewModel.Email).FirstOrDefault();
                    var existingUserByUsername = wheelsContext.Users.Where(u => u.Username == userViewModel.Username).FirstOrDefault();

                    if (existingUser == null && existingUserByEmail == null && existingUserByUsername == null) {
                        /* var existingEmail = _context.Users.Where(u => u.Email == userViewModel.Email).FirstOrDefault();
                         * var existingUsername = _context.Users.Where(u => u.Username == userViewModel.Username).FirstOrDefault();
                        if (existingUser ==  null) {
                            var existingUserByEmail = _context.Users.Where(u => u.Email == userViewModel.Email).FirstOrDefault();
                            if (Helper.IsTaken(existingUserByEmail.Email))
                            {
                                return BadRequest(new
                                {
                                    error = "true",
                                    message = "sorry, this email is already taken"

                                });
                            } else {

                                var existingUserByUsername = _context.Users.Where(u => u.Username == userViewModel.Username).FirstOrDefault();
                                if (Helper.IsTaken(existingUserByUsername.Username))
                                {
                                    return BadRequest(new
                                    {
                                        error = "true",
                                        message = "sorry, this username is already taken"
                                    });
                                }
                            }*/

                        /* var user_role = User.FindFirst("user_role")?.Value; */
                        var secure = "";
                        var generatedPassword = "";
                        if (userViewModel.Role == null || userViewModel.Role == Role.Client) {
                            secure = Helper.Hash(userViewModel.Password.ToString());
                            var contact = 0;

                            var user = new Client {

                                First_name = userViewModel.First_Name,
                                Last_name = userViewModel.Last_Name,
                                Email = userViewModel.Email,
                                Password = secure,
                                Id_number = userViewModel.Id_number,
                                Username = userViewModel.Username,
                                Telephone = userViewModel.Telephone ?? contact.ToString(),
                                Telephone_2 = userViewModel.Telephone_2 ?? contact.ToString(),
                                Work_telephone = userViewModel.Work_telephone ?? contact.ToString(),
                                Role = Role.Client,
                                Date_created = DateTime.Now.ToShortDateString().Replace('/', '-')
                            };

                            wheelsContext.Users.Add(user);

                            await wheelsContext.SaveChangesAsync();
                            return CreatedAtAction(nameof(GetUserByID), new
                            {
                                id = user.User_Id,
                                error = "false",
                                message = "registration was successful"
                            }, user);
                        } else if(userViewModel.Role == Role.Admin) {
                            
                                generatedPassword = Helper.CreatePassword(8);
                                secure = Helper.Hash(generatedPassword);
                                var user = new User
                                {

                                    First_name = userViewModel.First_Name,
                                    Last_name = userViewModel.Last_Name,
                                    Email = userViewModel.Email,
                                    Password = secure,
                                    Id_number = userViewModel.Id_number,
                                    Username = userViewModel.Username,
                                    Telephone = userViewModel.Telephone ?? "0",
                                    Telephone_2 = userViewModel.Telephone_2 ?? "0",
                                    Role = userViewModel.Role,
                                    Date_created = DateTime.Now.ToShortDateString().Replace('/', '-')

                                };

                                wheelsContext.Users.Add(user);

                                await wheelsContext.SaveChangesAsync();
                                return CreatedAtAction(nameof(GetUserByID), new
                                {
                                    id = user.User_Id,
                                    error = "false",
                                    message = "registration was successful"
                                }, user);
                            } else { return BadRequest(new {
                                error = "true",
                                message = "sorry, you have no role here!"
                            });
                            }

                    } else {
                        return BadRequest(new {
                            error = "true",
                            message = "sorry, user already exits"
                        });
                    }
                }
                else
                {
                    return BadRequest(new
                    {
                        error = "true",
                        message = "all fields are required"
                    });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = "true",
                    message = "An error occured while registrering",
                    Discription = e.Message
                });
            }

        }


        /* <summary>
                Athenticate user and grant access to 
           </summary>  */
        /** <param name="credentials"></param> **/
        [AllowAnonymous]
        [HttpPut]
        [Route("/authenticate")]
        public IActionResult Authenticate(Credentials credentials)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var secure = Helper.Hash(credentials.Password.ToString());
                    var isAuth = wheelsContext.Users.Where(u => (u.Id_number.ToString() == credentials.Username && u.Password == secure) || (u.Email == credentials.Username && u.Password == secure) || (u.Username == credentials.Username && u.Password == secure)).FirstOrDefault();

                    if (isAuth != null && isAuth.Account_status == "Active")
                    {
                        var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigninKey"]));
                        var token = Helper.Token(
                            _configuration["Issuer"],
                            _configuration["Audience"],
                            signingkey,
                            isAuth.Email,
                            isAuth.User_Id.ToString(),
                            isAuth.Role.Value);
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                        return Ok(new
                        {

                            error = "false",
                            message = "Success",
                            isAuth.Username,
                            isAuth.Email,
                            User = isAuth.Email,
                            isAuth.Id_number,
                            isAuth.Sex,
                            isAuth.Telephone,
                            isAuth.Telephone_2,
                            isAuth.Account_status,
                            isAuth.Date_created,
                            FirstName = isAuth.First_name,
                            LastName = isAuth.Last_name,
                            isAuth.Role,
                            timeStamp_Date = DateTime.Now.ToShortDateString().Replace('/', '-'),
                            timeStamp_Time = DateTime.Now.ToShortTimeString(),
                            Token = tokenString,
                        });
                    }
                    else if (isAuth != null && isAuth.Account_status != "Active")
                    {
                        return BadRequest(new
                        {
                            error = "true",
                            message = "Sorry your account has been deactivated. Please contact admin"
                        });
                    }
                    else
                    {

                        return BadRequest(new
                        {
                            error = "true",
                            message = "Sorry, looks like your username or password is incorrect "
                        });
                    }
                }
                else
                {
                    return BadRequest(new
                    {
                        error = "true",
                        message = "Invalid model"

                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = "true",
                    message = "Incorrect username or password",
                    description = e.Message
                });
            }


        }
         
        /* <summary>
             -- Gets all users
           </summary> */
        [HttpGet]
        [Route("/getAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers() {
            return await wheelsContext.Users.ToListAsync();
        }

        /* <summary>
             -- Get user by ID
           </summary> */
        [HttpGet]
        [Route("/getUserByID/{id}")] 
        public async Task<ActionResult<User>> GetUserByID(long id) {
            /**
             * when system goes live users will be extarcted by ID_number
             * 
             * [Route("/getUserByID/{id_number}")]
             * public async Task<ActionResult<User>> UserByID(long id_number) {
             * var user = wheelsContext.Users.Where( u => u.Id_number == id).FirstOrDefault();
            **/

            var user = await wheelsContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /* <summary>
             -- Update User by ID
           </summary> */
        [HttpPut]
        [Route("/updateUser")]
        public async Task<IActionResult> UpdateUser(User user) {


            try
            {
                if (UserExists(user.User_Id))
                {
                    var existingData = wheelsContext.Users.FirstOrDefault(u => u.User_Id == user.User_Id);
                    existingData.First_name = user.First_name ?? existingData.First_name;
                    existingData.Last_name = user.Last_name ?? existingData.Last_name;
                    existingData.Username = user.Username ?? existingData.Username;
                    existingData.Email = user.Email ?? existingData.Email;
                    existingData.Telephone = user.Telephone ?? existingData.Last_name;
                    existingData.Telephone_2 = user.Telephone_2 ?? existingData.Telephone_2;
                    existingData.Id_number = user.Id_number;
                    existingData.Sex = user.Sex  ?? existingData.Sex;
               
                    wheelsContext.Entry(existingData).State = EntityState.Modified;
                    await wheelsContext.SaveChangesAsync();

                    return Ok(new {
                        error = "false",
                        message = "Congratulations Big baby, you have gone wild!"
                    });
                }
            else {
                return NotFound(new {
                error = "true",
                message = "User does not exit"
                }); }
            } catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(new
                {
                    error = "true",
                    message = "Oops, I wonder what went wrong.",
                    Discription = e.Message
                });
            }
        } 

        /* <summary>
             -- Delete user by ID
           </summary> */
        [HttpDelete]
        [Route("/deleteUser/{id}")]
        public async Task<ActionResult<User>> DeleteUser(long id)  {
            var user = await wheelsContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            wheelsContext.Users.Remove(user);
            await wheelsContext.SaveChangesAsync();

            return user;
        }

        private bool UserExists(long id) {
            return wheelsContext.Users.Any(e => e.User_Id == id);
        }
    }
}

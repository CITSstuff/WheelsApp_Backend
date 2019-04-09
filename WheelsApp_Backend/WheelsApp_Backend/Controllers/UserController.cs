using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelsApi.Helper;
using WheelsApp_Backend.Data;
using WheelsApp_Backend.Models;

namespace WheelsApp_Backend.Controllers {
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller {
        private IDataRepository<User> _iRepoUser;
        private IConfiguration _configuration;
        public UserController(WheelsContext context, IConfiguration configuration) {
            _iRepoUser = new DataRepository<User>();
            _configuration = configuration;
        }

        /* <summary>
             -- Gets all users
           </summary> */
        [HttpGet]
        [Route("/getAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var list = new List<UserViewModel>();
                var users = _iRepoUser.GetAll();

                    foreach (var user in users)
                    {
                        var userVM = new UserViewModel
                        {
                            First_Name = user.First_name,
                            Last_Name = user.Last_name,
                            Email = user.Email,
                            Role = user.Role,
                            Telephone = user.Telephone,
                            Telephone_2 = user.Telephone_2
                        };
                        list.Add(userVM);
                    }               
                return Json(list);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    Error = "true",
                    Message = "Fetching users failed",
                    Description = e.Message
                });
            }
        }

        /*   <summary>
            -- Registers the user for the web app
             </summary> */ 
        /* <param name="userViewModel"></param>   */
        [AllowAnonymous]
        [HttpPost]
        [Route("/register")]
        public IActionResult CreateUser(UserViewModel userViewModel) {
            try {
                if (ModelState.IsValid)
                {

                    var existingUser = _iRepoUser.GetAll().Where(u => u.Id_number == userViewModel.Id_number).FirstOrDefault();
                    var existingUserByEmail = _iRepoUser.GetAll().Where(u => u.Email == userViewModel.Email).FirstOrDefault();
                    var existingUserByUsername = _iRepoUser.GetAll().Where(u => u.Username == userViewModel.Username).FirstOrDefault();

                    if (existingUser == null && existingUserByEmail == null  && existingUserByUsername == null) {
                        /* var existingEmail = _iRepoUser.GetAll().Where(u => u.Email == userViewModel.Email).FirstOrDefault();
                         * var existingUsername = _iRepoUser.GetAll().Where(u => u.Username == userViewModel.Username).FirstOrDefault();
                        if (existingUser ==  null) {
                            var existingUserByEmail = _iRepoUser.GetAll().Where(u => u.Email == userViewModel.Email).FirstOrDefault();
                            if (Helper.IsTaken(existingUserByEmail.Email))
                            {
                                return BadRequest(new
                                {
                                    error = "true",
                                    message = "sorry, this email is already taken"

                                });
                            } else {

                                var existingUserByUsername = _iRepoUser.GetAll().Where(u => u.Username == userViewModel.Username).FirstOrDefault();
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
                                Telephone = userViewModel.Telephone?? contact.ToString(),
                                Telephone_2 = userViewModel.Telephone_2 ?? contact.ToString(),
                                Work_telephone = userViewModel.Work_telephone ?? contact.ToString(),
                                Role = Role.Client,
                                Date_created = DateTime.Now.ToShortDateString().Replace('/', '-')
                            };
                            var request = _iRepoUser.Add(user);

                            return Ok(new
                            {
                                error = "false",
                                message = "registration was successful"
                            });
                        } else {
                            generatedPassword = Helper.CreatePassword(8);
                            secure = Helper.Hash(generatedPassword);
                            if (userViewModel.Role == Role.Admin) {
                                var user = new User {
                                    
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
                                _iRepoUser.Add(user);

                            } return Ok(new {
                                error = "false",
                                message = "registration was successful"
                            });

                        }
                      
                    }
                    else {
                        return BadRequest(new {
                            error = "true",
                            message = "sorry, user already exits"
                        });
                    }
                }
                else {
                    return BadRequest(new {
                        error = "true",
                        message = "all fields are required"
                    });
                }

            }
            catch (Exception e) {
                return BadRequest(new {
                    error = "true",
                    message = "An error occured while registrering",
                    Discription = e.Message
                });
            }

        }

        //This action @ AuthenticateApp([FromForm]Credentials credentials bind form data 
        /// <summary>
        /// Registers the user for the web app
        /// </summary>
        /// <param name="credentials"></param>   
        [AllowAnonymous]
        [HttpPost]
        [Route("/authenticate")]
        public IActionResult Authenticate(Credentials credentials)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var secure = Helper.Hash(credentials.Password.ToString());
                    var isAuth = _iRepoUser.GetAll().Where(u => (u.Id_number.ToString() == credentials.Username && u.Password == secure ) || (u.Email == credentials.Username && u.Password == secure ) || (u.Username == credentials.Username  && u.Password == secure)).FirstOrDefault();

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
                    } else {

                        return BadRequest(new
                        {
                            error = "true",
                            message = "Sorry, looks like your username or password is incorrect "
                        });
                    }
                }
                else
                {
                    return Json(new
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
    }
}
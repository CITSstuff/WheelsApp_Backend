using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WheelsApp_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WheelsApp_Backend.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DBContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(DBContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;

        }



        [Route("login/{userame}/{password}")]
        [HttpPost]
        public async Task<String> Login(string username, string password)
        {
            return await LoginUser(username, password);
        }
        [Route("register/{username}/{userrole}/{password}")]
        [HttpPost]
        public async Task<String> Register(string username, string userrole, string password)
        {
            return await CreateUser(username, userrole, password);
        }

        private async Task<String> LoginUser(string name, string password)
        {
            var user = new IdentityUser();
            user.UserName = name;

            var u = await _userManager.FindByNameAsync(name);
            var isValid = await _userManager.CheckPasswordAsync(u, password);
            var isLocked = await _userManager.GetLockoutEnabledAsync(u);
            if (isValid && !isLocked) return u.PasswordHash;

            return null;

        }
        private async Task<String> CreateUser(string name, string userrole, string password)
        {

            if (!await _roleManager.RoleExistsAsync(userrole))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(userrole));
            }

            var user = new IdentityUser();

            user.UserName = name;

            var adminresult = await _userManager.CreateAsync(user, password);

            if (adminresult.Succeeded)
            {
                var result = await _userManager.AddToRoleAsync(user, userrole);

                if (result.Succeeded)
                {
                    await _userManager.SetLockoutEnabledAsync(user, true);

                    return user.PasswordHash;
                }

            }
            else if (adminresult.Errors.Any(u => u.Description == $"Name{name} is already taken."))
            {
                var u = await _userManager.FindByNameAsync(name);
                var isValid = await _userManager.CheckPasswordAsync(u, password);
                if (isValid) return u.PasswordHash;

            }
            throw new Exception(adminresult.Errors.FirstOrDefault() == null ? "Unknown error" : adminresult.Errors.FirstOrDefault().Description);

        }
    }
}

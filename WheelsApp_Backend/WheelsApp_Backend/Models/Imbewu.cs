using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelsApi.Helper;

namespace WheelsApp_Backend.Models {
    public class Imbewu {
        public static void theSeed() {

            WheelsContext wheelsCtx = new WheelsContext();
            wheelsCtx.Database.EnsureCreated();
            if (wheelsCtx.Users.Any()) {
                return;
            }

            /** Seed admin on the system**/
            var users = new User[] {
                new User {
                    First_Name ="Admin",
                    Last_Name = "Admin",
                    Email ="wheels.ease@gmail.com",
                    Telephone = "0",
                    Password = Helper.Hash("Admin"),
                    Role = Role.Admin,
                    Telephone_2 = "0", 
                    Sex = "Female",
                    Username = "Admin",
                    Id_number = 00000000000013,
                    Date_created= DateTime.Now.AddDays(30).ToShortDateString()
                }
         };
            wheelsCtx.Users.AddRange(users);
            wheelsCtx.SaveChanges();
        }
    }
}

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
            var user = new Client { 
            
                First_Name = "Admin",
                Last_Name = "Admin",
                Email = "wheels.ease@gmail.com",
                Telephone = "0",
                Password = Helper.Hash("Admin"),
                Role = Role.Admin,
                Telephone_2 = "0",
                Sex = "Female",
                Username = "Admin",
                Id_number = 00000000000013,
                Date_created = DateTime.Now.AddDays(30).ToShortDateString(),
            
            /* Addresses = new List<Address>   {
                new Address {
            Building_name = "Bavitana",
            Street =  "8 Aureolle",
            City = "Northgate, Randburg",
            Country = "ZAR",
            Postal_code =  "2162" }
            },
            OfKins = new List<NextOfKin> {
                new NextOfKin {
              Telephone = "078 231 1234",
              Work_telephone = "011 092 3243",

                Address = new Address {
                Building_name = "Mariston",
                Street = "30 Claim street",
                City = "JHB",
                Country = "ZAR",
                Postal_code = "2001"
                    }
                }
            }*/

            };
            wheelsCtx.Users.AddRange(user);
            wheelsCtx.SaveChanges();
        }
    }
}

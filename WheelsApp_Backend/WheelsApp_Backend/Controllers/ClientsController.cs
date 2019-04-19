using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WheelsApi.Helper;
using WheelsApp_Backend.Models;

namespace WheelsApp_Backend.Controllers
{
    [Route("api/[clients]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly WheelsContext _context;

        public ClientsController(WheelsContext context)
        {
            _context = context;
        }

        /*   <summary>
           -- Get all Clients
            </summary> */
        /* <param name=" "></param>   */
        [HttpGet]
        [Route("/getAllClients")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            var clients = await _context.Clients.Where(u => u.Account_status == true && u.Role == Role.Client).ToListAsync();
            var list = new List<Client>();

                foreach (var client in clients)
                {
                    var clientVM = new Client
                    {
                        User_Id = client.User_Id,
                        First_Name = client.First_Name,
                        Last_Name = client.Last_Name,
                        Email = client.Email,
                        Role = client.Role,
                        Username = client.Username,
                        Avatar = client.Avatar,
                        Account_status = client.Account_status,
                        Date_created = client.Date_created,
                        Telephone =client.Telephone,
                        Telephone_2 = client.Telephone_2,
                        Sex = client.Sex,
                        Id_number = client.Id_number,
                        Work_telephone = client.Work_telephone,
                        Password = "No password, LOL! :)",

                        OfKins = client.OfKins,
                        Addresses = client.Addresses
                    };

                    list.Add(clientVM);
                }


            return Ok(list);
        }


        /*   <summary>
           -- Get Client by ID
            </summary> */
        /* <param name="ID", Client></param>   */
        [HttpGet]
        [Route("/getClientByID/{id}")]
        public ActionResult<User> GetClientByID(long id)
        {
            if (Helper.UserExists(id, _context))
            {
                try {

                    var client = Helper.GetUser(id, Role.Client, _context);
                    if (client == null) {
                        return BadRequest(new
                        {
                            error = "true",
                            message = "Ow Boi, this ID does not belong to any client"
                        });
                    } else {
                        return client;
                    }

                } catch (Exception e) {
                    return BadRequest(new
                    {
                        error = "true",
                        message = "Sorry, I failed to get this Client",
                        discription = e.Message
                    });
                }
            } else {
                return NotFound();
            }
        }

        /*   <summary>
           -- Update Client by ID
            </summary> */
        /* <param name="ID", Client></param>   
        [HttpPut]
        [Route("/updateClientByID/{id}")]
        public async Task<IActionResult> updateClientByID(Client client)
        {

            if(!Helper.UserExists(client.User_Id, _context)) {
                return NotFound();
            }

            try
            {
           
                _context.Entry(client).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    error = "false",
                    message = "Congratulations Big baby, you have gone wild!"
                });
                _context.Entry(client).State = EntityState.Modified;
            } catch (DbUpdateConcurrencyException e) {
                return BadRequest(new
                {
                    error = "false",
                    message = "Oops, looks like that didn't go so well. ..",
                    discription = e.Message
                });
            }

            return NoContent();
        } */

        /*   <summary>
            -- Create Client by ID
        </summary> */
        /* <param name="ID", Client></param>   */
        [HttpPost]
        [Route("/createQuickClient")]
        public async Task<ActionResult<Client>> CreateClient()
        {
            var resId = _context.Users.Max(user => user.User_Id) +1;
            var secure = Helper.Hash("quickClient");

            var client = new Client {
                First_Name = "Client",
                Last_Name = "Reservation_" + resId,
                Password = secure,
                Id_number = resId,
                Role = Role.Client,
                Date_created = DateTime.Now.ToShortDateString().Replace('/', '-'),
                Username = "WLZ RES_UNIT : " + resId,


            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            QuickReserveClientViewModel quick = new QuickReserveClientViewModel {
                User_Id = client.User_Id,
                First_Name = client.First_Name,
                Last_Name = client.Last_Name,
                Id_number = client.Id_number,
                Password = "",
                Role = client.Role,
                Date_created = client.Date_created,
                Username = client.Username,
                Reference = client.Username
               
            };
            return CreatedAtAction("GetClientByID", new { id = client.User_Id}, quick);
        }

        /*   <summary>
                -- Deactivate Client by ID
             </summary> */
        /* <param name="ID"></param>   */
        [HttpPut]
        [Route("/deactivateClientByID/{id}")]
        public async Task<ActionResult<User>> DeleteClientByID(long id)
        {
            /** Updating client through user**/

            if (!Helper.UserExists(id, _context)) {
                return NotFound(new
                {
                    error = "false",
                    message = "Sorry, It doesn't look like I have this Client in my records!"
                });
            }
            var existingClient = Helper.GetUser(id, Role.Client, _context);
            existingClient.Account_status = false;
            _context.Users.Update(existingClient);
            await _context.SaveChangesAsync();

            return existingClient;
        }
    }
}

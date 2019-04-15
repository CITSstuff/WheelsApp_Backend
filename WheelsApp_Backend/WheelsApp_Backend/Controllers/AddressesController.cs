using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WheelsApi.Helper;
using WheelsApp_Backend.Models;

namespace WheelsApp_Backend.Controllers
{
    [Route("api/[clients]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly WheelsContext _context;

        public AddressesController(WheelsContext context) {
            _context = context;
        }

        /*   <summary>
           -- Get all Addresses
            </summary> */
        /* <param name="userViewModel"></param>   */
        [HttpGet]
        [Route("/getAllAddresses")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAllAddress()
        {
            return await _context.Address.ToListAsync();
        }


        /*   <summary>
           -- Create Address, For a User by ID
            </summary> */
        /* <param name="ID", Address></param>   *
        [HttpPost]
        [Route("/createUserAddress/{id}")]
        public async Task<Object> CreateAddressBy(long id, Address address)
        {
            try
            {
                if (Helper.UserExists(id, _context))
                {
                    var toAdd = new Address
                    {

                        Building_name = address.Building_name,
                        Street = address.Street,
                        City = address.City,
                        Country = address.Country,
                        Postal_code = address.Postal_code
                    };

                    _context.Address.Add(toAdd);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetAddressByID", new { id = address.Address_Id }, address);
                }
                else
                {
                    return NotFound();
                }
            } catch(Exception e)
            {
                return BadRequest(new
                {
                    error = "true",
                    message = "Ow boi, looks like you failed to update",
                    discription = e.Message
                });
            }

        } */


        /*   <summary>
           -- Get AddressByID
            </summary> */
        /* <param name="ID"></param>   */
        [HttpGet]
        [Route("/getAddressByID/{id}")]
        public async Task<ActionResult<Address>> GetAddressByID(long id)
        {
            var address = await _context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        /*   <summary>
           -- Update AddressByID
            </summary> */
        /* <param name="ID"></param>   */
        [HttpPut]
        [Route("/updateUserAddressByID/{id}")]
        public async Task<Object> UpdateUserAddressByID(long id, Address address)
        {
            if(!Helper.UserExists(id, _context) || !Helper.AddressExists(address.Address_Id, _context)) {
                return NotFound();
            }

            try
            {
            Address existingAddress = Helper.GetAddress(id, _context);

            existingAddress.Building_name = address.Building_name ?? existingAddress.Building_name;
            existingAddress.Street = address.Street ?? existingAddress.Street ;
            existingAddress.City = address.City ?? existingAddress.City;
            existingAddress.Country = address.Country ?? existingAddress.Country;
            existingAddress.Postal_code = address.Postal_code ?? existingAddress.Postal_code;


                _context.Entry(existingAddress).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    error = "false",
                    message = "Congratulations Big baby, you have gone wild!"
                });
            }
            catch (DbUpdateConcurrencyException) {
                if (!Helper.AddressExists(id, _context)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
        }

        /*   <summary>
           -- Delete AddressByID
            </summary> */
        /* <param name="ID"></param>   
        [HttpDelete]
        [Route("/deleteAddressBy/{id}")]
        public async Task<ActionResult<Address>> DeleteAddressByID(long id)
        {
            var address = await Helper.GetAddress(id, _context);
            if (!Helper.AddressExists(id, _context)) {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return address;
        }*/

    }
}

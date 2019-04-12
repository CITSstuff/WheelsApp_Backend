using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WheelsApp_Backend.Models
{

    public enum Role {
        Client, Admin
    }

    public class User {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long   User_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public long   Id_number { get; set; }
        public string Telephone_2 { get; set; }
        public string Sex { get; set; } = "Male";
        public string Date_created { get; set; }
        public string Account_status { get; set; } = "Active";
        public Role? Role { get; set; }
    }
    public class Credentials
    {
        public string Username { set; get; }
        public string Password { set; get; }
    }

    public class Passwords
    {
        public string Password { set; get; }
        public string New_Password { set; get; }
    }

    public class UserViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long User_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public long Id_number { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }
        public string Avatar { get; set; }
        public string Account_Status { get; set; }
        public Role? Role { get; set; }
        public string Telephone { get; set; }
        public string Telephone_2 { get; set; }
        public string Work_telephone { get; set; }

        public List<Address> Addresses { get; set; }
        public List<NextOfKin> OfKins { get; set; }
    }

    public class Client : User {
        public Int32 Token { get; set; }
        public string Avatar { get; set; }
        public string Work_telephone { get; set; }
        public List<Address> Addresses { get; set; }
        public List<NextOfKin> OfKins { get; set; }

    }

    public class NextOfKin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OfKin_ID { get; set; }
        public string Telephone { get; set; }
        public string Work_telephone { get; set; }
        public Address Address { get; set; }

    }
    /* The relationship from 'Address.UserForeignKey' to 'User' with foreign key properties {'Building_name' : string}
    cannot target the primary key {'User_Id' : long}
    because it is not compatible.Configure a principal key or a set of compatible foreign key properties for this relationship.*/

    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Address_Id {get; set; }
        public string Building_name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Postal_code { get; set; }
    }


    public class UpdatePasswordModel {
        public string Password { get; set; }
        public string New_password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WheelsApp_Backend.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Car_Id { get; set; }
        public string Date_Added { get; set; }
        public string Make { get; set; }
        public string Year { get; set; }
        public string Colour { get; set; }
        public string Kms { get; set; }
        public string Registration { get; set; }
        public string Tank { get; set; }
        public string Status { get; set; }

        public List<Damages> Damages { get; set; }

    }

    public class CarViewModel
    {
        
        public string Make { get; set; }
        public string Year { get; set; }
        public string Colour { get; set; }
        public string Kms { get; set; }
        public string Registration { get; set; }
        public string Tank { get; set; }
    }

    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Res_Id { get; set; }
        public string KM_In { get; set; }
        public string KM_Out { get; set; }
        public string DateTime_In { get; set; }
        public string DateTime_Out { get; set; }
        public int  Days { get; set; }

        public Car Car { get; set; }
        public Client Client { get; set; }
    }

    public class Movement
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Movement_Id { get; set; }
        public string Date_Created { get; set; }
        public string DateTime_Out { get; set; }
        public string DateTime_In { get; set; }
        
        public Car Car { get; set; }
        public User Diver { get; set; }
    }


    public class CarProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Properties_Id { get; set; }

    }

        public class Damages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Damage_Id { get; set; }
        public string Weigh { get; set; }
        public string DateOfDamage { get; set; }
        public string DateOfFix { get; set; }
        public string Damage { get; set; }
        public int  Driver { get; set; }
        public bool IsFixed { get; set; }
    }


}
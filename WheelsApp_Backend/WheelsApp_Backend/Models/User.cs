using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelsApp_Backend.Models
{
    public class User {
        public long Id { get; set; }
        public string role { get; set; }
        public Int32 token { get; set; }
        public long id_number { get; set; }
        public string telephone { get; set; }
        public string first_name { get; set; }
        public string lasst_name { get; set; }
        public string telephone_2 { get; set; }
        public string work_contact { get; set; }
        public string next_of_keen { get; set; }
    }
}

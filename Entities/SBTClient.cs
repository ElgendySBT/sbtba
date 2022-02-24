using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Entities
{
    public class SBTClient
    {
        [Key]
        public long ClientId { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
       
    }
}

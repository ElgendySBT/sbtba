using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Entities
{
    public class SBTUser:IdentityUser
    {
        public string DisplayName { get; set; }
        //public Address Address { get; set; }
    }
}

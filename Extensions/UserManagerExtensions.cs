
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SBTBackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SBTBackEnd.Extensions
{
    public static class UserManagerExtensions
    {
       
        public static async Task<SBTUser> FindByEmailFromClaimsPrincipal(this UserManager<SBTUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}

using SBTBackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Services.TokenService
{
    public interface ITokenService
    {
        string CreateToken(SBTUser user);
    }
}

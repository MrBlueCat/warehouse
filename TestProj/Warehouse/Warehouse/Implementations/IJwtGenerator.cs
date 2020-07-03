using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Warehouse
{
    public interface IJwtGenerator
    {
        string GenerateEncodedToken(ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(User user);
    }
}

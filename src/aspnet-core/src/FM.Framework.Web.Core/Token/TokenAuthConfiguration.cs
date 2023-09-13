using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Framework.Web.Core.Token
{
    public class TokenAuthConfiguration: ITokenAuthConfiguration
    {
        public new SymmetricSecurityKey SecurityKey { get; set; }

        public new string Issuer { get; set; }

        public new string Audience { get; set; }

        public new SigningCredentials SigningCredentials { get; set; }

        public new TimeSpan AccessTokenExpiration { get; set; }

        public new TimeSpan RefreshTokenExpiration { get; set; }
    }
}

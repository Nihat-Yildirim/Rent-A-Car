using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class RefreshToken
    {
        public string RefreshTokenValue { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }
    }
}

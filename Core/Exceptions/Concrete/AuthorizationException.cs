using Core.Exceptions.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions.Concrete
{
    public class AuthorizationException : ExceptionBase
    {
        public AuthorizationException(string message) : base(message,HttpStatusCode.Unauthorized)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public enum HttpStatusCode
    {
        OK = 200,
        MOVED_PERMANENTLY = 301,
        FOUND = 302,
        TEMPORARY_REDIRECT = 307,
        BAD_REQUEST = 400,
        NOT_FOUND = 404,
        SERVER_ERROR = 500,
    }
}

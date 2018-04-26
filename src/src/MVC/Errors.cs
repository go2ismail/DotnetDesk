using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.MVC
{
    public static class Errors
    {
        public static class Error500
        {
            public const string FullUrl = "/error/500";
        }

        public static class Error404
        {
            public const string FullUrl = "/error/404";
        }
    }
}

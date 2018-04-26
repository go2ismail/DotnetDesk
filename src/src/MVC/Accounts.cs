using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.MVC
{
    public static class Accounts
    {
        public static class Login
        {
            public const string FullUrl = "/Account/Login";
        }

        public static class Register
        {
            public const string FullUrl = "/Account/Register";
        }

        public static class RegisterSuccess
        {
            public const string FullUrl = "/Account/RegisterSuccess";
        }

        public static class ForgotPassword
        {
            public const string FullUrl = "/Account/ForgotPassword";
        }
    }
}

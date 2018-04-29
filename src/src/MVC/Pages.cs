using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.MVC
{
    public static class Pages
    {
        public static class ConfigIndex
        {
            public const string FullUrl = "/Config/Index";
            public const string Controller = "Config";
            public const string Action = "Index";
        }

        public static class ConfigOrganization
        {
            public const string FullUrl = "/Config/Organization";
            public const string Controller = "Config";
            public const string Action = "Organization";
        }

        public static class CustomerIndex
        {
            public const string FullUrl = "/Customer/Index";
            public const string Controller = "Customer";
            public const string Action = "Index";
        }

        public static class ProductIndex
        {
            public const string FullUrl = "/Product/Index";
            public const string Controller = "Product";
            public const string Action = "Index";
        }

        public static class SupportAgentIndex
        {
            public const string FullUrl = "/SupportAgent/Index";
            public const string Controller = "SupportAgent";
            public const string Action = "Index";
        }

        public static class SupportEngineerIndex
        {
            public const string FullUrl = "/SupportEngineer/Index";
            public const string Controller = "SupportEngineer";
            public const string Action = "Index";
        }

        public static class TicketIndex
        {
            public const string FullUrl = "/Ticket/Index";
            public const string Controller = "Ticket";
            public const string Action = "Index";
        }

        public static class TicketCustomer
        {
            public const string FullUrl = "/Ticket/Customer";
            public const string Controller = "Ticket";
            public const string Action = "Customer";
        }
    }
}

using Microsoft.Owin;

namespace FrunutStock.Web
{
    internal class CookieAuthenticationOptions
    {
        public string AuthenticationType { get; set; }
        public PathString LoginPath { get; set; }
    }
}
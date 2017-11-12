using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Diagnostics;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using FrunutStock.Web.Models;

namespace FrunutStock.Web
{
    public class Startup
    {
        public static Func<UserManager<Models.AppUser>> UserManagerFactory { get; private set; }
        public static Func<RoleManager<IdentityRole>> RoleManagerFactory { get; private set; }
        public void Configuration(IAppBuilder app)
        {

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/auth/login")
            });
           
            // configure the user manager
            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<Models.AppUser>(
                    new UserStore<Models.AppUser>(new AppDbContext()));
                // allow alphanumeric characters in username
                usermanager.UserValidator = new UserValidator<Models.AppUser>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };
                usermanager.ClaimsIdentityFactory = new AppUserClaimsIdentityFactory();
                return usermanager;
            };
            
            createRolesandUsers();

        }
        private void createRolesandUsers()
        {
            AppDbContext context = new AppDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<Models.AppUser>(new UserStore<Models.AppUser>(context));
            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new Models.AppUser();
                user.UserName = "alirah";
                user.Email = "alirahmanian@hotmail.com";

                string userPWD = "1qaz7410";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);

            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }
        }
        private void GetDefualtRoles()
        {
            AppDbContext context = new AppDbContext();
            foreach (var user in context.Users)
            {
                Debug.WriteLine(user.Id + ":" + user.UserName);
            }
            if (context.Roles.ToList<IdentityRole>().Where(r => r.Name == "Admin").FirstOrDefault() == null)
            {
                Debug.WriteLine("not found");
                
            }
            else
            {
                Debug.WriteLine("found");
            }
            var roles = context.Roles.ToList<IdentityRole>();
            foreach(var role in roles)
            {
                Debug.WriteLine(role.Id + ":" + role.Name);
            }
           
           // context.SaveChanges();
        }
        


    }
}
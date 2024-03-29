﻿using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Web.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using Web.Models;

[assembly: OwinStartupAttribute(typeof(Web.Startup))]
namespace Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Bootstrapper.Run(app);
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            //Here we create a Admin super user who will maintain the website                   

            if(UserManager.FindByEmail("sohail.ahmed@arthurlawrence.net") == null)
            { 
                var user = new ApplicationUser();
                user.UserName = "sohail.ahmed@arthurlawrence.net";
                user.Email = "sohail.ahmed@arthurlawrence.net";
                user.FirstName = "sohail";
                user.LastName = "shaikh";
                user.EmployeeNumber = "355";
                string userPWD = "admin12";

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
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);

            }

            // creating Creating Employee role     
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);

            }
            // creating Creating Employee role     
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }
        }


    }
}

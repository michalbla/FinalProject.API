using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI_FinalProject.Model.Entities;

namespace TNAI_FinalProject.Model
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if(context.Roles.Any())
            {
                return;
            }
          
            context.Roles.AddRange(
                new RoleUser()
                {
                    Name = "Admin"
                },
                new RoleUser()
                {
                    Name = "Accountant"
                },
                new RoleUser()
                {
                    Name = "Employee"
                }
                );

            context.SaveChanges();
            
            if (context.Users.Any())
            {
                return;
            }

            context.Users.AddRange(
                new User()
                {
                    FirstName = "Michał",
                    LastName = "Błaszczyk",
                    Email = "Admin@wp.pl",
                    PasswordHash = "1234",
                    RoleId = context.Roles.Where(x => x.Name == "Admin").First().Id,
                },
                new User()
                {
                    FirstName = "Barbara",
                    LastName = "Deptuła",                    
                    Email = "Ksiegowa@wp.pl",
                    PasswordHash = "1234",
                    RoleId = context.Roles.Where(x => x.Name == "Accountant").First().Id,
                }
                );
            context.SaveChanges();
        }
    }
}

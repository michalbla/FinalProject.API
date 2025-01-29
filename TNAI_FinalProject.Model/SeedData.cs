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

            if (context.Admins.Any())
            {
                return;
            }

            context.Admins.AddRange(
                new Admin()
                {
                    Name = "ZdzichuHaker",
                    PasswordHash = "Haslo",

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
                    RoleId = context.Roles.Where(x => x.Name == "Admin").First().Id
                },
                new User()
                {
                    FirstName = "Barbara",
                    LastName = "Deptuła",
                    Email = "Ksiegowa@wp.pl",
                    PasswordHash = "1234",
                    RoleId = context.Roles.Where(x => x.Name == "Accountant").First().Id,
                    AdminId = context.Admins.Where(x => x.Name == "ZdzichuHaker").First().Id
                },
                 new User()
                 {
                     FirstName = "Dariusz",
                     LastName = "Krzywokrok",
                     Email = "Slepota@wp.pl",
                      PasswordHash = "4321",
                      RoleId = context.Roles.Where(x => x.Name == "Employee").First().Id,
                      AdminId = context.Admins.Where(x => x.Name == "ZdzichuHaker").First().Id
                  },
                  new User()
                  {
                  FirstName = "Beatka",
                  LastName = "B",
                  Email = "Beti@wp.pl",
                  PasswordHash = "2222",
                  RoleId = context.Roles.Where(x => x.Name == "Employee").First().Id,
                  AdminId = context.Admins.Where(x => x.Name == "ZdzichuHaker").First().Id
                  }
                );
            context.SaveChanges();


        }
    }
}

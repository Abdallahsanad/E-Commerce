using Microsoft.AspNetCore.Identity;
using Store.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Identity
{
    public static class StoreIdentityDbContextSeed
    {
        public async static Task SeedAppUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() ==0)
            {
                var user = new AppUser()
                {
                    Email = "abdallahsanadabo@gmail.com",
                    DisplayName = "Abdallah Ali",
                    PhoneNumber = "01142760215",
                    Address = new Address()
                    {
                        FName = "Abdallah",
                        LName = "Ali",
                        Street = "Etihad",
                        City = "Obour",
                        Country = "Egypt"
                    }
                };
                await _userManager.CreateAsync(user, "2312m@2312M");

            }

        }
    }
}

using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class GebruikerRepo
    {
        private readonly UserManager<Gebruiker> userMgr;
        private readonly RoleManager<IdentityRole> roleManager;

        public GebruikerRepo(UserManager<Gebruiker> userMgr, RoleManager<IdentityRole> roleManager)
        {
            this.userMgr = userMgr;
            this.roleManager = roleManager;
        }

        public async Task<Gebruiker> Add(Gebruiker gebruiker)
        {
            IdentityResult result = await userMgr.CreateAsync(gebruiker);
            return null;
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Gebruiker>> GetAllAsync(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            var result = userMgr.Users.Include(u => u.Roles).Where(p => p.Roles.Any(r => r.RoleId == role.Id));

            //  IList<Person> result = await userMgr.GetUsersInRoleAsync(roleName) ;
            return result;
        }
    }
}

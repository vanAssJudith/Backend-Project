using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public static class Seeders
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Gebruiker>().HasData(
            //    new Gebruiker
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        UserName = "Judithvanass",
            //        Naam = "Judith van Ass",
            //        Email = "Judith.van.ass@student.howest.be",
            //        //PasswordHash = "MijnP@sw00rd1",
            //    });

            modelBuilder.Entity<Moeilijkheidsgraad>().HasData(
                new Moeilijkheidsgraad() { Id = 1, Titel = "Gemakkelijk" },
                new Moeilijkheidsgraad() { Id = 2, Titel = "Gemiddeld" },
                new Moeilijkheidsgraad() { Id = 3, Titel = "Moeilijk" });
        }

        public async static Task SeedRoles(RoleManager<IdentityRole> RoleMgr)
        {
            IdentityResult roleResult;
            string[] roleNames = { "Admin", "Deelnemer" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleMgr.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleMgr.CreateAsync(new IdentityRole(roleName));
                }
            }

        }
        public async static Task SeedUsers(UserManager<Gebruiker> userMgr)
        {
            if (await userMgr.FindByNameAsync("Docent@MCT") == null)
            {
                var user = new Gebruiker()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Docent@MCT",
                    Naam = "JohanVan@Howest",
                    Email = "Docent@1"
                };

                var userResult = await userMgr.CreateAsync(user, "Docent@1");
                var roleResult = await userMgr.AddToRoleAsync(user, "Admin");

                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }

            if (await userMgr.FindByNameAsync("judithvanass") == null)
            {
                var user = new Gebruiker()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "judithvanass",
                    Naam = "JudithvanAss",
                    Email = "judith.van.ass@student.howest.be"
                };

                var userResult = await userMgr.CreateAsync(user, "MijnP@sw00rd1");
                var roleResult = await userMgr.AddToRoleAsync(user, "Deelnemer");

                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }

            var nmbrDeelnemers = 9;
            for (var i = 1; i <= nmbrDeelnemers; i++)
            {
                if (userMgr.FindByNameAsync("Deelnemer" + i).Result == null)
                {
                    Gebruiker deelnemer = new Gebruiker
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "Deelnemer" + i,
                        Naam = "naamDeelnemer" + i,
                        Email = "emailDeelnemer" + i + "@student.howest.be"
                    };

                    var userResult = await userMgr.CreateAsync(deelnemer, "deelnemerP@sw00rd" + i);
                    var roleResult = await userMgr.AddToRoleAsync(deelnemer, "Deelnemer");
                    if (!userResult.Succeeded || !roleResult.Succeeded)
                    {
                        throw new InvalidOperationException("Failed to build " + deelnemer.UserName);
                    }
                }
            }
        }

    }
}

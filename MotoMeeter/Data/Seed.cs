﻿using Microsoft.AspNetCore.Identity;
using MotoMeeter.Data.Enum;
using MotoMeeter.Models;
using System.Diagnostics;

namespace MotoMeeter.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Title = "Moto Club 1",
                            Image = "https://superbike-photos.com/wp-content/uploads/2022/08/IMG_8185.jpg",
                            Description = "This is the description of the first moto club!",
                            ClubCategory = ClubCategory.SportBike,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                         },
                        new Club()
                        {
                            Title = "Moto Club 2",
                            Image = "https://superbike-photos.com/wp-content/uploads/2022/08/IMG_9277-copy.jpg",
                            Description = "This is the description of the second club!",
                            ClubCategory = ClubCategory.Minibike,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Club()
                        {
                            Title = "Moto Club 3",
                            Image = "https://superbike-photos.com/wp-content/uploads/2022/08/IMG_5543-1024x683.jpg",
                            Description = "This is the description of the third club!",
                            ClubCategory = ClubCategory.SportBike,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Club()
                        {
                            Title = "Moto Club 4",
                            Image = "https://superbike-photos.com/wp-content/uploads/2022/08/IMG_7200.jpg",
                            Description = "This is the description of the fourth club",
                            ClubCategory = ClubCategory.Cruiser,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Michigan",
                                State = "NC"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Races
                if (!context.Meetups.Any())
                {
                    context.Meetups.AddRange(new List<Meetup>()
                    {
                        new Meetup()
                        {
                            Title = "Moto Meetup 1",
                            Image = "https://superbike-photos.com/wp-content/uploads/2022/08/IMG_5152-copy.jpg",
                            Description = "This is the description of the first race",
                            MeetupCategory = MeetupCategory.Canyon,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Meetup()
                        {
                            Title = "Moto Meetup 2",
                            Image = "https://superbike-photos.com/wp-content/uploads/2022/08/IMG_5805-1024x683.jpg",
                            Description = "This is the description of the first race",
                            MeetupCategory = MeetupCategory.RoadTrip,
                            AddressId = 5,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }

                        }
                    });
                    context.SaveChanges();
                }
            }

        }

        //public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        //{
        //    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        //    {

        //        //Roles
        //        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //        if (!await roleManager.RoleExistsAsync(UserRoles.User))
        //            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        //        //Users
        //        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        //        string adminUserEmail = "teddysmithdeveloper@gmail.com";

        //        var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
        //        if (adminUser == null)
        //        {
        //            var newAdminUser = new AppUser()
        //            {
        //                UserName = "teddysmithdev",
        //                Email = adminUserEmail,
        //                EmailConfirmed = true,
        //                Address = new Address()
        //                {
        //                    Street = "123 Main St",
        //                    City = "Charlotte",
        //                    State = "NC"
        //                }
        //            };
        //            await userManager.CreateAsync(newAdminUser, "Coding@1234?");
        //            await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
        //        }


        //        string appUserEmail = "user@etickets.com";

        //        var appUser = await userManager.FindByEmailAsync(appUserEmail);
        //        if (appUser == null)
        //        {
        //            var newAppUser = new AppUser()
        //            {
        //                UserName = "app-user",
        //                Email = appUserEmail,
        //                EmailConfirmed = true,
        //                Address = new Address()
        //                {
        //                    Street = "123 Main St",
        //                    City = "Charlotte",
        //                    State = "NC"
        //                }
        //            };
        //            await userManager.CreateAsync(newAppUser, "Coding@1234?");
        //            await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
        //        }
        //    }
        //}
    }
}
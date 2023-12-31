﻿using Booking_Api.Models;

namespace Booking_Api.Data
{
    public class BookingManagementDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BookingManagementDbContext>();

                context.Database.EnsureCreated();
                // University
                if (!context.Universities.Any())
                {
                    context.Universities.AddRange(new List<University>()
                    {
                        new University
                        {
                            Code = "U001",
                            Name = "University of Greenwich",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        },
                        new University
                        {
                            Code = "U002",
                            Name = "University of Westminster",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        },
                        new University
                        {
                            Code = "U003",
                            Name = "University of Oxford",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        }
                    });
                    context.SaveChanges();
                }
                // Room
                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(new List<Room>(){
                        new Room{
                            Name = "Room 1",
                            Floor = 1,
                            Capacity = 10,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                        },
                        new Room{
                            Name = "Room 2",
                            Floor = 2,
                            Capacity = 10,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                        },
                        new Room{
                            Name = "Room 3",
                            Floor = 3,
                            Capacity = 12,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                        },
                    });
                    context.SaveChanges();
                }
                // Role
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new List<Role>(){
                        new Role{
                            Name = "Admin",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                        },
                        new Role{
                            Name = "User",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                        },
                    });
                    context.SaveChanges();
                }
                // Employee
                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(new List<Employe>()
                    {
                        new Employe{
                            Nik = "N00011",
                            FirstName = "John",
                            LastName = "Doe",
                            BirthDate = DateTime.Now,
                            Gender = 0,
                            HiringDate = DateTime.Now,
                            Email = "Jhon@gmail.com",
                            PhoneNumber = "08123456789",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                        },
                        new Employe{
                            Nik = "N00012",
                            FirstName = "Jane",
                            LastName = "Doe",
                            BirthDate = DateTime.Now,
                            Gender = 0,
                            HiringDate = DateTime.Now,
                            Email = "Joe@gmail.com",
                            PhoneNumber = "08123456789",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                        }

                    });
                    context.SaveChanges();
                }
                // Booking
                if (!context.Bookings.Any())
                {
                    context.Bookings.AddRange(new List<Booking>()
                    {
                        new Booking
                        {
                            RoomGuid = context.Rooms.FirstOrDefault().Guid,
                            EmployeeGuid = context.Employees.FirstOrDefault().Guid,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now,
                            Status = 2,
                            Remarks = "Remarks",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        },
                        new Booking
                        {
                            RoomGuid = context.Rooms.FirstOrDefault().Guid,
                            EmployeeGuid = context.Employees.FirstOrDefault().Guid,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now,
                            Status = 1,
                            Remarks = "Remarks",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        }
                    });
                    context.SaveChanges();
                }

                // Education
                if (!context.Educations.Any())
                {
                    context.Educations.AddRange(new List<Education>()
                    {
                        new Education
                        {
                            Guid = context.Employees.FirstOrDefault().Guid,
                            UniversityGuid = context.Universities.FirstOrDefault().Guid,
                            Degree = "Bachelor",
                            Major = "Computer Science",
                            Gpa = 3,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        }
                    });
                    context.SaveChanges();
                }
                // Account
                if (!context.Accounts.Any())
                {
                    context.Accounts.AddRange(new List<Account>()
                    {
                        new Account
                        {
                            Guid = Guid.NewGuid(),
                            Password = "admin",
                            IsDeleted = false,
                            Otp = 123456,
                            IsUsed = true,
                            ExpiredTime = DateTime.Now,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        },
                        new Account{
                            Guid =  Guid.NewGuid(),
                            Password = "user",
                            IsDeleted = false,
                            Otp = 123456,
                            IsUsed = true,
                            ExpiredTime = DateTime.Now,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        }
                    });
                    context.SaveChanges();
                }
                // AccountRole
                if (!context.AccountRoles.Any())
                {
                    context.AccountRoles.AddRange(new List<AccountRole>()
                    {
                        new AccountRole
                        {

                            AccountGuid = context.Accounts.FirstOrDefault().Guid,
                            RoleGuid = context.Roles.FirstOrDefault().Guid,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        },
                        new AccountRole{

                            AccountGuid = context.Accounts.FirstOrDefault().Guid,
                            RoleGuid = context.Roles.FirstOrDefault().Guid,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        }
                    });
                    context.SaveChanges();
                }

            }
        }
    }
}

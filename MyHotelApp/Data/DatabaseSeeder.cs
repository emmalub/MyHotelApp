using MyHotelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(HotelDbContext context)
        {
            try
            {
                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(
                        new Models.SingleRoom { RoomType = "Enkelrum", Price = 600, IsActive = true },
                        new Models.DoubleRoom { RoomType = "Dubbelrum", Price = 1000, Size = 30, MaxExtraBeds = 1, IsActive = true },
                        new Models.SingleRoom { RoomType = "Enkelrum", Price = 500, IsActive = false },
                        new Models.DoubleRoom { RoomType = "Dubbelrum", Price = 1000, Size = 45, MaxExtraBeds = 2, IsActive = true }
                    );
                    context.SaveChanges();
                }
                if (!context.Customers.Any())
                {
                    context.Customers.AddRange(

                        new Models.Customer { FirstName = "Emma", LastName = "Lübke", Address = "Avägen 1", PostalCode = "12345", City = "Sundsvall", IsVip = true, IsActive = true, Email = "emma.lubke@dakjbhjacaw.com", Phone = "0123456789" },
                        new Models.Customer { FirstName = "Ebba", LastName = "Gradin", Address = "Bvägen 1", PostalCode = "67890", City = "Stockholm", IsVip = false, IsActive = true, Email = "ebba.gradin@dakjbhjacaw.com", Phone = "08910111213" },
                        new Models.Customer { FirstName = "Malika", LastName = "Lika", Address = "Cvägen 1", PostalCode = "23456", City = "Uppsala", IsVip = false, IsActive = true, Email = "malika.lika@dakjbhjacaw.com", Phone = "0701234567" },
                        new Models.Customer { FirstName = "Agge", LastName = "Bagge", Address = "Dvägen 1", PostalCode = "78912", City = "Umeå", IsVip = false, IsActive = true, Email = "agge.bagge@dakjbhjacaw.com", Phone = "07089101112" }
                    );
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during seeding: {ex.Message}");
            }
        }
    }
}

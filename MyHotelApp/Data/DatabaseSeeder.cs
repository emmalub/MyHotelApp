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
            if (!context.Rooms.Any())
            {
                context.Rooms.AddRange(
                    new Models.SingleRoom { RoomType = "SingleRoom", Price = 600, IsActive = true },
                    new Models.DoubleRoom { RoomType = "DoubleRoom", Price = 1000, Size = 30, ExtraBeds = 1, IsActive = true },
                    new Models.SingleRoom { RoomType = "SingleRoom", Price = 500, IsActive = false },
                    new Models.DoubleRoom { RoomType = "DoubleRoom", Price = 1000, Size = 45, ExtraBeds = 2, IsActive = true }
                );
                context.SaveChanges();
            }
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(

                    new Models.Customer { FirstName = "Emma", LastName = "Lübke", Email = "emma.lubke@dakjbhjacaw.com", Phone = "0123456789" },
                    new Models.Customer { FirstName = "Ebba", LastName = "Gradin", Email = "ebba.gradin@dakjbhjacaw.com", Phone = "08910111213" },
                    new Models.Customer { FirstName = "Malika", LastName = "Lika", Email = "malika.lika@dakjbhjacaw.com", Phone = "0701234567" },
                    new Models.Customer { FirstName = "Agge", LastName = "Bagge", Email = "agge.bagge@dakjbhjacaw.com", Phone = "07089101112" }

                        );
            }

        }
    }
}

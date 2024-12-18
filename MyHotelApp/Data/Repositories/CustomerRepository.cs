using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHotelApp.Data.Context;
using MyHotelApp.Models;


namespace MyHotelApp.Data.Repositories
{
    public class CustomerRepository
    {
        private readonly HotelDbContext _context;

        public CustomerRepository(HotelDbContext context)
        { 
            _context = context;
        }

        //public Customer GetCustomerById(int id)
        //{
        //  return _context.Customer.First(c => c.Id == id);
        //}

        //public List<Customer> GetAllCustomers()
        //{
        //    return _context.Customer.ToList();
        //}

        public void GetCustomerInfo()
        { 
        /// SKRIV KOD FÖR ATT HÄMTA KUNDINFO
        }
    }
}

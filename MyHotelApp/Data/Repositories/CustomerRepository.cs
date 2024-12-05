using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyHotelApp.Data.Repositories
{
    public class CustomerRepository
    {
        private readonly HotelDbContext _context;

        public CustomerRepository(HotelDbContext context)
        { 
            _context = context;
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.First(c => c.Id == id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyHotelApp.Data;
using MyHotelApp.Models;
using Spectre.Console;

namespace MyHotelApp.Services
{
    public class CustomerService
    {
        private readonly HotelDbContext _context;

        public CustomerService(HotelDbContext context)
        {
            _context = context;
        }
        public void CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            Console.WriteLine("Ny kund sparad!");
        }
        
        public List<Customer> GetCustomers()
        {
            try
            {
                return _context.Customers.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
                return new List<Customer>();
            }
        }
        public Customer GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }
        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            try
            {
                _context.SaveChanges();
                Console.WriteLine("Ändringar har sparats!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            }
        }
        public void DeactivateCustomer(int customerId)
        {
            var customer = _context.Customers
                .Include(c => c.Bookings)
                .FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
            {
                Console.WriteLine("Kunden finns inte.");
                return;
            }

            if (customer.Bookings != null && customer.Bookings.Any())
            {
                Console.WriteLine("Kunden har aktiva bokningar och kan inte tas bort.");
                return;
            }

            customer.IsActive = false;
            _context.Customers.Update(customer);

            try
            {
                _context.SaveChanges();
                Console.WriteLine("Kunden har inaktiverats!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            }
        }
    }
}

namespace MyHotelApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsVip { get; set; }
        public bool IsActive { get; set; }
        public string Name => $"{FirstName} {LastName}";

        public List<Booking>? Bookings { get; set; } = new List<Booking>();
        public List<Invoice>? Invoices { get; set; } = new List<Invoice>();

    }
}

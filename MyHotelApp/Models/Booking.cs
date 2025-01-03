namespace MyHotelApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public virtual Customer Guest { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Invoice Invoice { get; set; }
        public bool IsActive { get; set; }
        public bool IsPaid { get; set; }
        public string? Conditions { get; set; }
        public int ExtraBeds { get; set; }


    }
}

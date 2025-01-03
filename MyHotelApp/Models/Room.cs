namespace MyHotelApp.Models
{
    public partial class Room
    {
        public int Id { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>();

        public virtual string GetRoomDescription() => $"Rum: {Id}, Typ: {RoomType}, Pris: {Price}, Status: {IsActive}";
    }
}

namespace MyHotelApp.Models
{
    public class DoubleRoom : Room
    {
        public int MaxExtraBeds { get; set; }
        public double Size { get; set; }

        public DoubleRoom()
        {
            RoomType = "Dubbelrum";
        }

        public override string GetRoomDescription() => $"Rum: {Id}, Typ: {RoomType}, Pris: {Price}, Status: {IsActive}, Extrasängar: {MaxExtraBeds}";
    }
}

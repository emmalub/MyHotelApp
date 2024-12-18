﻿namespace MyHotelApp.Models
{
    public class DoubleRoom : Room
    {
        public int ExtraBeds { get; set; }
        public double Size { get; set; }

        public DoubleRoom()
        {
            RoomType = "Dubbelrum";
        }

        public override string GetRoomDescription()
        {
            return base.GetRoomDescription() + $", Extrasängar: {ExtraBeds}";
        }
    }
}

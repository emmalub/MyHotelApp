namespace MyHotelApp.Services
{
    public class MessageService
    {
        public void SendMessage(string recipient, string message)
        {
            Console.WriteLine($"Skickar e-post till {recipient} med följande meddelande: {message}");
        }

    }
}

namespace BookingApi.Models
{
    public class SearchHotelViewModel
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string City { get; set; }
        public int Adults { get; set; }
        public int Rooms { get; set; }
    }
}

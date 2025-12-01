namespace BookingApi.Models
{
    public class HotelDetailViewModel
    {
        public string Name { get; set; }
        public List<string> Photos { get; set; }
        public string Description { get; set; }
        public string reviewScoreWord { get; set; }
        public float reviewScore { get; set; }
        public int reviewCount { get; set; }
    }
}
using BookingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookingApi.Controllers
{
    public class BookingController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchHotelViewModel model)
        {
            TempData["checkin"] = model.CheckIn.ToString("yyyy-MM-dd");
            TempData["checkout"] = model.CheckOut.ToString("yyyy-MM-dd");

            string destid = await GetHotelDestIdFromCity(model.City);
            var client = new HttpClient();

            for (int i = 0; i < 3; i++)
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(
                        $"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels" +
                        $"?dest_id={destid}" +
                        $"&search_type=CITY" +
                        $"&arrival_date={model.CheckIn:yyyy-MM-dd}" +
                        $"&departure_date={model.CheckOut:yyyy-MM-dd}" +
                        $"&adults={model.Adults}" +
                        $"&room_qty={model.Rooms}" +
                        $"&page_number=1" +
                        $"&units=metric" +
                        $"&temperature_unit=c" +
                        $"&languagecode=en-us" +
                        $"&currency_code=USD" +
                        $"&location=US"),
                    Headers =
            {
                { "x-rapidapi-key", "******************************************" },
                { "x-rapidapi-host", "booking-com15.p.rapidapi.com" },
            },
                };

                var response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    await Task.Delay(5000); // 2 saniye bekle
                    continue; // tekrar dene
                }

                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<BookingApiViewModel>(body);

                if (values?.data?.hotels == null || !values.data.hotels.Any())
                {
                    // Otel yok, hata mesajı veya boş liste döndür
                    return View(new List<BookingApiViewModel.Hotel>());
                }

                return View(values.data.hotels.ToList());
            }

            return View("Error"); // 3 denemede de olmadı → limit dolmuştur
        }

        public async Task<string> GetHotelDestIdFromCity(string city)
        {
            var client = new HttpClient();

            for (int i = 0; i < 3; i++)
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchDestination?query={city}"),
                    Headers =
            {
                { "x-rapidapi-key", "*****************************************" },
                { "x-rapidapi-host", "booking-com15.p.rapidapi.com" },
            },
                };

                var response = await client.SendAsync(request);

                // 429 Too Many Requests
                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    await Task.Delay(2000);
                    continue;
                }

                var body = await response.Content.ReadAsStringAsync();

                // API hata dönerse yakala
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Destination API hata döndü: " + body);
                }

                var values = JsonConvert.DeserializeObject<DestinationIdViewModel>(body);

                if (values?.data == null || values.data.Length == 0)
                    throw new Exception("Şehir bulunamadı: " + city);

                return values.data[0].dest_id;
            }

            throw new Exception("API limitine takıldınız. Lütfen 1-2 dakika sonra tekrar deneyin.");
        }


        //Otel Detay
        [HttpGet]
        public IActionResult HotelDetails(string hotelName, List<string> imageUrl, string label, string reviewScoreWord = "", float reviewScore = 0f, int reviewCount = 0)
        {

            var model = new HotelDetailViewModel
            {
                Name = hotelName,
                Photos = imageUrl ?? new List<string>(), // boşsa default list
                Description = label,
                reviewScoreWord = reviewScoreWord,
                reviewScore = reviewScore,
                reviewCount= reviewCount
            };

            return View(model);
        }

    }
}

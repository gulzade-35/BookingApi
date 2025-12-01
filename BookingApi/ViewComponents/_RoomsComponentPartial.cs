using Microsoft.AspNetCore.Mvc;

namespace BookingApi.ViewComponents
{
    public class _RoomsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

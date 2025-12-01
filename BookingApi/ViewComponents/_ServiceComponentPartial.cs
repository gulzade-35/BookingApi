using Microsoft.AspNetCore.Mvc;

namespace BookingApi.ViewComponents
{
    public class _ServiceComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

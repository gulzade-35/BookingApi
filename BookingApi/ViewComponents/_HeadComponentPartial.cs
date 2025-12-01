using Microsoft.AspNetCore.Mvc;

namespace BookingApi.ViewComponents
{
    public class _HeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

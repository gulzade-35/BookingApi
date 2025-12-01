using Microsoft.AspNetCore.Mvc;

namespace BookingApi.ViewComponents
{
    public class _NavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

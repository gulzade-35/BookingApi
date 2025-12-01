using Microsoft.AspNetCore.Mvc;

namespace BookingApi.ViewComponents
{
    public class _BookingComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

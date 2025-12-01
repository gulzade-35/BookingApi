using Microsoft.AspNetCore.Mvc;

namespace BookingApi.ViewComponents
{
    public class _SliderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

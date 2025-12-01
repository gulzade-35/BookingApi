using Microsoft.AspNetCore.Mvc;

namespace BookingApi.ViewComponents
{
    public class _AboutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

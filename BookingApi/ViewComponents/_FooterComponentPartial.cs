using Microsoft.AspNetCore.Mvc;

namespace BookingApi.ViewComponents
{
    public class _FooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

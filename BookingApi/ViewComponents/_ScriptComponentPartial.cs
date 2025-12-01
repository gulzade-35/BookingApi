using Microsoft.AspNetCore.Mvc;

namespace BookingApi.ViewComponents
{
    public class _ScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

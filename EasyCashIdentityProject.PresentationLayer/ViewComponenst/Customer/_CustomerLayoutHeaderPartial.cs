using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.ViewComponenst.Customer
{
    public class _CustomerLayoutHeaderPartial:ViewComponent
    {
        public IViewComponentResult Invoke() 
        {
            return View(); 
        }

    }
}

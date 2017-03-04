using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace UniTime.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : UniTimeControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); // Layout of the angular application.
        }
	}
}
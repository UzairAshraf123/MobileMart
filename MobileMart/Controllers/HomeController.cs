using MobileMart.BL;
using MobileMart.Utility;
using System.Linq;
using System.Web.Mvc;

namespace MobileMart.Controllers
{
    public class HomeController : Controller
    {
        HomeBL BL = new HomeBL();

        public ActionResult Index()
        {
            return View(BL.index());
        }

        public ActionResult RegisterAndLogin()
        {
            AdminBL adminBL = new AdminBL();
            var countries = adminBL.GetCountries().Select(s => new
            {
                Text = s.name,
                Value = s.id
            }).ToList();
            ViewBag.CountryDropDown = new SelectList(countries, "Value", "Text");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserProfile(int ID)
        {
            HomeBL BL = new HomeBL();
            var profile = BL.ShopDetails(ID);
            return View(profile);
        }


        public ActionResult ProductDetail(int? ID)
        {
            if (ID > 0)
            {
                HomeBL BL = new HomeBL();
                var detail = BL.Detail(ID);
                return View(detail);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
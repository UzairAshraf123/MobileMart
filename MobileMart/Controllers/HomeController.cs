using MobileMart.BL;
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

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult Companies ()
        {
            return View();
        }

        public ActionResult UserProfile(int ID)
        {
           
            AdminBL BL = new AdminBL();
            var profile = BL.GetShopByOwnerID(ID);
            return View(profile);
        }

       
    }
}
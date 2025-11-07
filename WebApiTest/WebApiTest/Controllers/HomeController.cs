using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
//using System.Web;
//using System.Web.Mvc;

namespace WebApiTest.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(FormCollection fc)
        {
            if (fc["username"] == "Admin" && fc["password"] == "password123")
            {
                HttpContext.Session.SetString("tempid", fc["username"]);

                return RedirectToAction("VerifyAuth");
            }
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        [HttpGet]
        public IActionResult VerifyAuth()
        {
            if (HttpContext.Session.GetString("tempid") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("tempid");
                return View();
            }
            return RedirectToAction("Login");
        }

        private const int V = 300;
        string key = "test123@12";

        [HttpPost]
        public IActionResult VerifyAuth(FormCollection fc)
        {
            var token = fc["passcode"];
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            string useruniquekey = Convert.ToString(HttpContext.Session.GetString("tempid")) + key;
            bool isvalid = tfa.ValidateTwoFactorPIN(useruniquekey, token);
            if (isvalid)
            {
                HttpContext.Session.Remove("tempid");
                HttpContext.Session.SetString("authid", Convert.ToString(HttpContext.Session.GetString("tempid")));
                return RedirectToAction("Cliente");
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AdminQR()
        {
            if (HttpContext.Session.GetString("tempid") != null)
            {
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                string useruniquekey = Convert.ToString(HttpContext.Session.GetString("tempid")) + key;
                HttpContext.Session.SetString("Useruniquekey", useruniquekey);
                SetupCode setupInfo = tfa.GenerateSetupCode("WebApiTest", Convert.ToString(HttpContext.Session.GetString("tempid")), useruniquekey, true, 300);
                ViewBag.QRCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                ViewBag.SetupCode = setupInfo.ManualEntryKey;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }


        }

        [HttpGet]
        public IActionResult Cliente()
        {
            if (HttpContext.Session.GetString("authid") != null)
            {
                return RedirectToAction("Cliente");
            }
            return RedirectToAction("Login");
        }
    }
}

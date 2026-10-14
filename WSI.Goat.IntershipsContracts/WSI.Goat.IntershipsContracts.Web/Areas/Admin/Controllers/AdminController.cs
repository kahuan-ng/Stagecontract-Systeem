using Microsoft.AspNetCore.Mvc;
using WSI.Goat.InternshipContracts.Web.Data;
using WSI.Goat.InternshipsContracts.Web.Areas.Admin.ViewModels;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly InternshipContractsDbContext _internshipContractsDbContext;

        public AdminController(InternshipContractsDbContext internshipContractsDbContext)
        {
            _internshipContractsDbContext = internshipContractsDbContext;
        }

        public IActionResult Index()
        {
            string? adminName = HttpContext.Session.GetString("AdminName");

            if (string.IsNullOrEmpty(adminName)) return RedirectToAction("Login");

            AdminIndexViewModel adminIndexViewModel = new()
            {
                AdminName = adminName
            };

            return View(adminIndexViewModel);
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AdminName")))
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AdminLoginViewModel adminLoginViewModel)
        {
            if (!ModelState.IsValid) return View(adminLoginViewModel);
            
            var adminRole = _internshipContractsDbContext.ApplicationRoles
                .FirstOrDefault(ar => ar.BaseName == "Admin");

            if (adminRole == null)
            {
                ModelState.AddModelError(string.Empty, "Systeemfout: Admin rol niet gevonden.");
                return View(adminLoginViewModel);
            }

            var admin = _internshipContractsDbContext.ApplicationUsers
                .FirstOrDefault(u => u.Email == adminLoginViewModel.Email 
                                     && u.Password == adminLoginViewModel.Password
                                     && u.ApplicationRoleId == adminRole.Id);

            if (admin != null)
            {
                HttpContext.Session.SetString("AdminId", admin.Id.ToString());
                HttpContext.Session.SetString("AdminName", admin.FirstName);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Ongeldige login gegevens");
            return View(adminLoginViewModel);
        }
        
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            HttpContext.Session.Remove("AdminId");
            HttpContext.Session.Remove("AdminName");
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}


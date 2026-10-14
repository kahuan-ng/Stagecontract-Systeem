using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WSI.Goat.InternshipContracts.Core.Entities;
using WSI.Goat.InternshipContracts.Web.Data;
using WSI.Goat.InternshipsContracts.Web.Areas.Admin.ViewModels;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersManagementController : Controller
    {
        private readonly InternshipContractsDbContext _internshipContractsDbContext;
        public UsersManagementController(InternshipContractsDbContext internshipContractsDbContext)
        {
            _internshipContractsDbContext = internshipContractsDbContext;
        }

        public IActionResult Index()
        {
            string? adminName = HttpContext.Session.GetString("AdminName");

            if (string.IsNullOrEmpty(adminName))
            {
                return RedirectToAction("Login", "Admin");
            }
            List<UserOverviewViewModel> users = _internshipContractsDbContext.ApplicationUsers
                                .Select(u => new UserOverviewViewModel
                                {
                                    Id = u.Id,

                                    FirstName = u.FirstName,

                                    LastName = u.LastName,

                                    Email = u.Email,

                                    Function = u.Function
                                })
                                .ToList();

            UsersManagementIndexViewModel usersManagementIndexViewModel = new()
            {
                AdminName = adminName,

                Users = users
            };
            return View(usersManagementIndexViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> roles = _internshipContractsDbContext.ApplicationRoles
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),

                    Text = r.BaseName
                })
                .ToList();
            UserCreateEditViewModel userCreateEditViewModel = new()
            {
                Roles = roles
            };

            return View(userCreateEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserCreateEditViewModel userCreateEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                userCreateEditViewModel.Roles = _internshipContractsDbContext.ApplicationRoles
                        .Select(r => new SelectListItem
                        {
                            Value = r.Id.ToString(),

                            Text = r.BaseName
                        })
                        .ToList();

                return View(userCreateEditViewModel);
            }
            ApplicationUser user = new()
            {
                Id = Guid.NewGuid(),
                FirstName = userCreateEditViewModel.FirstName,
                LastName = userCreateEditViewModel.LastName,
                Email = userCreateEditViewModel.Email,
                Password = userCreateEditViewModel.Password,
                Function = userCreateEditViewModel.Function,
                TelephoneNumber = userCreateEditViewModel.TelephoneNumber,
                ApplicationRoleId = userCreateEditViewModel.SelectedRoleId.Value,
                BaseName = userCreateEditViewModel.FirstName + " " + userCreateEditViewModel.LastName,
                Created = DateTime.Now
            };

            _internshipContractsDbContext.ApplicationUsers.Add(user);
            _internshipContractsDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            ApplicationUser? user = _internshipContractsDbContext.ApplicationUsers
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            List<SelectListItem> roles = _internshipContractsDbContext.ApplicationRoles
                               .Select(r => new SelectListItem
                               {
                                   Value = r.Id.ToString(),

                                   Text = r.BaseName
                               })
                               .ToList();

            UserCreateEditViewModel userCreateEditViewModel = new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Function = user.Function,
                TelephoneNumber = user.TelephoneNumber,
                SelectedRoleId = user.ApplicationRoleId,
                Roles = roles
            };

            return View(userCreateEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserCreateEditViewModel userCreateEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                userCreateEditViewModel.Roles = _internshipContractsDbContext.ApplicationRoles
                        .Select(r => new SelectListItem
                        {
                            Value = r.Id.ToString(),

                            Text = r.BaseName
                        })
                        .ToList();
                return View(userCreateEditViewModel);
            }

            ApplicationUser? user = _internshipContractsDbContext.ApplicationUsers
                .FirstOrDefault(u => u.Id == userCreateEditViewModel.Id);

            if (user == null)
            {
                return NotFound();
            }
            user.FirstName = userCreateEditViewModel.FirstName;
            user.LastName = userCreateEditViewModel.LastName;
            user.Email = userCreateEditViewModel.Email;
            user.Function = userCreateEditViewModel.Function;
            user.TelephoneNumber = userCreateEditViewModel.TelephoneNumber;

            user.ApplicationRoleId = userCreateEditViewModel.SelectedRoleId.Value;
            if (!string.IsNullOrWhiteSpace(userCreateEditViewModel.Password))
            {
                user.Password = userCreateEditViewModel.Password;
            }
            user.BaseName = userCreateEditViewModel.FirstName + " " + userCreateEditViewModel.LastName;
            user.Updated = DateTime.Now;

            _internshipContractsDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
       
    }
}

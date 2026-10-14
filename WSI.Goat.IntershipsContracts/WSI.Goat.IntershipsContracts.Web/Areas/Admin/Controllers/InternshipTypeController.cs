using Microsoft.AspNetCore.Mvc;
using WSI.Goat.InternshipContracts.Core.Entities;
using WSI.Goat.InternshipContracts.Web.Data;
using WSI.Goat.InternshipsContracts.Web.Areas.Admin.ViewModels;
using WSI.Goat.IntershipsContracts.Core.Entities;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InternshipTypeController : Controller
    {
        private readonly InternshipContractsDbContext _context;
        public InternshipTypeController(InternshipContractsDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<InternshipTypeOverviewViewModel> internshipTypes = _context.InternshipTypes
                .Select(t => new InternshipTypeOverviewViewModel
                {
                    Id = t.Id,
                    BaseName = t.BaseName
                })
                .ToList();

            InternshipTypeIndexViewModel internshipTypeIndexViewModel = new()
            {
                InternshipTypes = internshipTypes
            };

            return View(internshipTypeIndexViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            InternshipTypeCreateEditViewModel internshipTypeCreateEditViewModel = new();
            return View(internshipTypeCreateEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InternshipTypeCreateEditViewModel internshipTypeCreateEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(internshipTypeCreateEditViewModel);
            }
            InternshipType internshipType = new()
            {
                Id = Guid.NewGuid(),
                BaseName = internshipTypeCreateEditViewModel.BaseName,
                Created = DateTime.Now
            };
            _context.InternshipTypes.Add(internshipType);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            InternshipType? internshipType = _context.InternshipTypes
                .FirstOrDefault(t => t.Id == id);
            if (internshipType == null)
            {
                return NotFound();
            }
            InternshipTypeCreateEditViewModel internshipTypeCreateEditViewModel = new()
            {
                Id = internshipType.Id,
                BaseName = internshipType.BaseName
            };
            return View(internshipTypeCreateEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InternshipTypeCreateEditViewModel internshipTypeCreateEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(internshipTypeCreateEditViewModel);
            }
            InternshipType? internshipType = _context.InternshipTypes
                           .FirstOrDefault(t => t.Id == internshipTypeCreateEditViewModel.Id);
            if (internshipType == null)
            {
                return NotFound();
            }
            internshipType.BaseName = internshipTypeCreateEditViewModel.BaseName;
            internshipType.Updated = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}
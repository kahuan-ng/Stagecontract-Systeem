using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WSI.Goat.InternshipContracts.Web.Data;
using WSI.Goat.InternshipsContracts.Web.Areas.Admin.ViewModels;
using WSI.Goat.IntershipsContracts.Core.Entities;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly InternshipContractsDbContext _internshipContractsDbContext;
        public DepartmentController(InternshipContractsDbContext internshipContractsDbContext)
        {
            _internshipContractsDbContext = internshipContractsDbContext;
        }
        public IActionResult Index()
        {
            List<DepartmentOverviewViewModel> departments = _internshipContractsDbContext.Departments
                .Select(d => new DepartmentOverviewViewModel
                {
                    Id = d.Id,
                    BaseName = d.BaseName,
                    Location = d.Location,
                    ClusterName = d.Cluster.BaseName,
                    StageCoordinatorName = d.StageCoordinator.FirstName + " " + d.StageCoordinator.LastName
                })
                .ToList();

            DepartmentIndexViewModel departmentIndexViewModel = new()
            {
                Departments = departments
            };

            return View(departmentIndexViewModel);
        }
        private void LoadDropdowns(DepartmentCreateEditViewModel departmentCreateEditViewModel)
        {
            List<SelectListItem> clusters = _internshipContractsDbContext.Clusters
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.BaseName
                })
                .ToList();

            departmentCreateEditViewModel.Clusters = clusters;
            List<SelectListItem> stageCoordinators = _internshipContractsDbContext.ApplicationUsers
                           .Where(u => u.Function != null &&
                                       u.Function.Contains("Stagecoördinator"))
                           .Select(u => new SelectListItem
                           {
                               Value = u.Id.ToString(),
                               Text = u.FirstName + " " + u.LastName
                           })
                           .ToList();

            departmentCreateEditViewModel.StageCoordinators = stageCoordinators;
            List<SelectListItem> departments = _internshipContractsDbContext.Departments
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.BaseName
                })
                .ToList();
            departmentCreateEditViewModel.Departments = departments;
        }
        [HttpGet]
        public IActionResult Create()
        {
            DepartmentCreateEditViewModel departmentCreateEditViewModel = new();
            LoadDropdowns(departmentCreateEditViewModel);
            return View(departmentCreateEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentCreateEditViewModel departmentCreateEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns(departmentCreateEditViewModel);
                return View(departmentCreateEditViewModel);
            }

            if (departmentCreateEditViewModel.ClusterId == null)
            {
                return Content("ClusterId is NULL");
            }

            if (departmentCreateEditViewModel.StageCoordinatorId == null)
            {
                return Content("StageCoordinatorId is NULL");
            }
            if (departmentCreateEditViewModel.BaseName==null)
                {
                return Content("Departments is NULL");
            }
            Department department = new()
            {
                Id = Guid.NewGuid(),
               
                BaseName = departmentCreateEditViewModel.BaseName,
                Location = departmentCreateEditViewModel.Location,
                ClusterId = departmentCreateEditViewModel.ClusterId,
                StageCoordinatorId = departmentCreateEditViewModel.StageCoordinatorId,
                Created = DateTime.Now
            };

            _internshipContractsDbContext.Departments.Add(department);

            _internshipContractsDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Department? department = _internshipContractsDbContext.Departments
                .FirstOrDefault(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }
            DepartmentCreateEditViewModel departmentCreateEditViewModel = new()
            {
                Id = department.Id,
                BaseName = department.BaseName,
                Location = department.Location,
                ClusterId = department.ClusterId,
                StageCoordinatorId = department.StageCoordinatorId
            };
            LoadDropdowns(departmentCreateEditViewModel);
            return View(departmentCreateEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentCreateEditViewModel departmentCreateEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns(departmentCreateEditViewModel);
                return View(departmentCreateEditViewModel);
            }
            Department? department = _internshipContractsDbContext.Departments
                            .FirstOrDefault(d => d.Id == departmentCreateEditViewModel.Id);

            if (department == null)
            {
                return NotFound();
            }

            department.BaseName = departmentCreateEditViewModel.BaseName;
            department.Location = departmentCreateEditViewModel.Location;
            department.ClusterId = departmentCreateEditViewModel.ClusterId;
            department.StageCoordinatorId = departmentCreateEditViewModel.StageCoordinatorId;
            department.Updated = DateTime.Now;
            _internshipContractsDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}
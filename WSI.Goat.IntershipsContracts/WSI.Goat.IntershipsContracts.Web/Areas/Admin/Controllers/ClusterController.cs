using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WSI.Goat.InternshipContracts.Core.Entities;
using WSI.Goat.InternshipContracts.Web.Data;
using WSI.Goat.InternshipsContracts.Web.Areas.Admin.ViewModels;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClusterController : Controller
    {
        private readonly InternshipContractsDbContext _internshipContractsDbContext;

        public ClusterController(InternshipContractsDbContext internshipContractsDbContext)
        {
            _internshipContractsDbContext = internshipContractsDbContext;
        }
        public IActionResult Index()
        {
            List<ClusterOverviewViewModel> clusters = _internshipContractsDbContext.Clusters
             .Select(c => new ClusterOverviewViewModel
             {
                 Id = c.Id,
                 BaseName = c.BaseName
             })
       .ToList();
            ClusterIndexViewModel ClusterIndexViewModel = new()
            {
                Clusters = clusters
            };

            return View(ClusterIndexViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> supervisors = _internshipContractsDbContext.ApplicationUsers
                .Where(u => u.Function == "Supervisor stageplaats")
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.FirstName + " " + u.LastName
                })
                .ToList();

            ClusterOverviewViewModel clusterOverviewViewModel = new()
            {
                Supervisors = supervisors
            };

            return View(clusterOverviewViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClusterOverviewViewModel clusterOverviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                clusterOverviewViewModel.Supervisors = _internshipContractsDbContext.ApplicationUsers
                    .Where(u => u.Function == "Supervisor stageplaats")
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.FirstName + " " + u.LastName
                    })
                    .ToList();

                return View(clusterOverviewViewModel);
            }

            ApplicationUser? supervisor = _internshipContractsDbContext.ApplicationUsers
                .FirstOrDefault(u => u.Function == "Supervisor stageplaats");

            if (supervisor == null)
            {
                return Content("Geen supervisor stageplaats gevonden");
            }

            Cluster cluster = new()
            {
                Id = Guid.NewGuid(),
                BaseName = clusterOverviewViewModel.BaseName,
                SupervisorId = supervisor.Id,
                Created = DateTime.Now
            };

            _internshipContractsDbContext.Clusters.Add(cluster);
            _internshipContractsDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Cluster? cluster = _internshipContractsDbContext.Clusters
                .FirstOrDefault(c => c.Id == id);

            if (cluster == null)
            {
                return NotFound();
            }
            List<SelectListItem> supervisors = _internshipContractsDbContext.ApplicationUsers
                            .Where(u => u.Function == "Supervisor stageplaats")
                            .Select(u => new SelectListItem
                            {
                                Value = u.Id.ToString(),
                                Text = u.FirstName + " " + u.LastName
                            })
                            .ToList();

            ClusterOverviewViewModel clusterOverviewViewModel = new()
            {
                Id = cluster.Id,
                BaseName = cluster.BaseName,
                Supervisors = supervisors
            };

            return View(clusterOverviewViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClusterOverviewViewModel clusterOverviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                clusterOverviewViewModel.Supervisors = _internshipContractsDbContext.ApplicationUsers
                    .Where(u => u.Function == "Supervisor stageplaats")
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.FirstName + " " + u.LastName
                    })
                    .ToList();

                return View(clusterOverviewViewModel);
            }
            Cluster? cluster = _internshipContractsDbContext.Clusters
                            .FirstOrDefault(c => c.Id == clusterOverviewViewModel.Id);

            if (cluster == null)
            {
                return NotFound();
            }
            cluster.BaseName = clusterOverviewViewModel.BaseName;
            cluster.Updated = DateTime.Now;
            _internshipContractsDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
       
    }
}

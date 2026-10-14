using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Diagnostics;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using WSI.Goat.InternshipContracts.Core.Entities;
using WSI.Goat.InternshipContracts.Web.Areas.Supervisor.ViewModels;
using WSI.Goat.InternshipContracts.Web.Data;
using WSI.Goat.InternshipContracts.Web.Models;
using WSI.Goat.InternshipsContracts.Web.Services.Interfaces;
using WSI.Goat.InternshipsContracts.Web.ViewModels;

namespace WSI.Goat.InternshipContracts.Web.Areas.Supervisor.Controllers;

[Area("Supervisor")]
public class SupervisorController : Controller
{
    private readonly InternshipContractsDbContext _internshipContracts;
    private readonly IFormBuilder _formBuilder;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SupervisorController(InternshipContractsDbContext internshipContractsDbContext, IFormBuilder formBuilder, IWebHostEnvironment webHostEnvironment)
    {
        _internshipContracts = internshipContractsDbContext;
        _formBuilder = formBuilder;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        string supervisorName = HttpContext.Session.GetString("SupervisorName");
        string supervisorIdString = HttpContext.Session.GetString("SupervisorId");

        if (string.IsNullOrEmpty(supervisorName) || string.IsNullOrEmpty(supervisorIdString))
        {
            return RedirectToAction("Login");
        }

        Guid supervisorIdGuid = Guid.Parse(supervisorIdString);

        var supervisor = _internshipContracts.ApplicationUsers
            .FirstOrDefault(s => s.Id == supervisorIdGuid);

        if (supervisor == null)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        var organization = _internshipContracts.StageOrganizations
            .FirstOrDefault(o => o.RepresentativeId == supervisorIdGuid);

        List<InternshipContract> supervisorContracts = new();

        if (organization != null)
        {
            supervisorContracts = _internshipContracts.InternshipContracts
                .Include(c => c.InternshipOrganization)
                .Include(c => c.Department)
                .Include(c => c.InternshipType)
                .Include(c => c.Student)
                .Include(c => c.InternshipMentor)
                .Include(c => c.InternshipAttendant)
                    .Where(c => c.InternshipOrganizationId == organization.Id)
                .ToList();
        }

        SupervisorIndexViewModel supervisorIndexViewModel = new()
        {
            SupervisorName = supervisor.FirstName + " " + supervisor.LastName,
            Contracts = supervisorContracts.Select(c => new SupervisorShowContractDetailsViewModel()
            {
                ContractId = c.Id,
                InternshipContractName = c.BaseName,
                StudentName = c.Student != null
                    ? $"{c.Student.FirstName} {c.Student.LastName}"
                    : "Onbekende student",
                InternshipMunicipality = c.InternshipMunicipality,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                HasRiskAnalysis = c.HasRiskAnalysis,
                MentorIsSigned = c.MentorIsSigned,
                StudentIsSigned = c.StudentIsSigned,
                SupervisourIsSigned = c.SupervisourIsSigned,
            }).ToList()
        };

        return View(supervisorIndexViewModel);
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SupervisorName")))
        {
            return RedirectToAction("Index", "Supervisor", new { area = "Supervisor" });
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(SupervisorLoginViewModel supervisorLoginViewModel)
    {
        if (!ModelState.IsValid) return View(supervisorLoginViewModel);

        var supervisorRole = _internshipContracts.ApplicationRoles
            .FirstOrDefault(ar => ar.BaseName == "Supervisor");

        if (supervisorRole == null)
        {
            ModelState.AddModelError(string.Empty, "Systeemfout: Supervisor rol niet gevonden.");
            return View(supervisorLoginViewModel);
        }

        var supervisor = _internshipContracts.ApplicationUsers
            .FirstOrDefault(s => s.Email == supervisorLoginViewModel.Email
                                 && s.Password == supervisorLoginViewModel.Password
                                 && s.ApplicationRoleId == supervisorRole.Id);
        if (supervisor != null)
        {
            HttpContext.Session.SetString("SupervisorId", supervisor.Id.ToString());
            HttpContext.Session.SetString("SupervisorName", supervisor.FirstName + " " + supervisor.LastName);

            return RedirectToAction("Index", "Supervisor", new { area = "Supervisor" });
        }

        ModelState.AddModelError(string.Empty, "Ongeldige login gegevens");
        return View("Login", supervisorLoginViewModel);
    }

    [HttpGet]
    public IActionResult CompleteContract(Guid id)
    {
        string? supervisorIdString = HttpContext.Session.GetString("SupervisorId");

        if (string.IsNullOrEmpty(supervisorIdString)) return RedirectToAction("Login");

        var organization = _internshipContracts.StageOrganizations
            .FirstOrDefault(o => o.RepresentativeId == Guid.Parse(supervisorIdString));

        if (organization == null) return NotFound("Geen organisatie gekoppeld aan deze supervisor.");

        var contract = _internshipContracts.InternshipContracts
            .Include(c => c.Student)
            .Include(c => c.Department)
            .Include(c => c.InternshipType)
            .FirstOrDefault(c => c.Id == id && c.InternshipOrganizationId == organization.Id);

        if (contract == null) return NotFound();

        SupervisorCompleteContractViewModel supervisorCompleteContractViewModel = new()
        {
            ContractId = contract.Id,
            ContractTitle = contract.BaseName,
            StudentName = contract.Student != null
                ? $"{contract.Student.FirstName} {contract.Student.LastName}"
                : "Onbekende student",
            StudentEmail = contract.Student?.Email ?? "Geen e-mail bekend",
            StudentPhone = contract.Student?.TelephoneNumber ?? "Geen telefoonnummer bekend",
            DepartmentName = contract.Department?.BaseName ?? "Niet toegewezen",
            InternshipTypeName = contract.InternshipType?.BaseName ?? "Niet toegewezen",
            InternshipStreet = contract.InternshipStreet,
            InternshipStreetNumber = contract.InternshipStreetNumber,
            InternshipPostalCode = contract.InternshipPostalCode,
            InternshipMunicipality = contract.InternshipMunicipality,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            NeedsMedical = contract.NeedsMedical,
            HasRiskAnalysis = contract.HasRiskAnalysis,
            MentorIsSigned = contract.MentorIsSigned,
            StudentIsSigned = contract.StudentIsSigned,
            SupervisourIsSigned = contract.SupervisourIsSigned,
            CoordinatorValidation = contract.CoordinatorValidation,
            InternshipCoordinatorRemarks = contract.InternshipCoordinatorRemarks,
            MedicalResearchLocation = contract.MedicalResearchLocation,
        };

        return View(supervisorCompleteContractViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CompleteContract(SupervisorCompleteContractViewModel supervisorCompleteContractViewModel, IFormFile? riskAnalysis)
    {
        if (supervisorCompleteContractViewModel.SupervisourIsSigned == null)
        {
            ModelState.AddModelError("Signature", "Een afbeelding van uw handtekening is verplicht om het contract naar howest te kunnen versturen.");
        }
        
        if (!ModelState.IsValid)
        {
            var contractId = supervisorCompleteContractViewModel.ContractId;
            var contractValidation = _internshipContracts.InternshipContracts
                .Include(c => c.Student)
                .Include(c => c.Department)
                .Include(c => c.InternshipType)
                .FirstOrDefault(c => c.Id == contractId);

            if (contractValidation != null)
            {
                if (contractValidation.Student != null)
                {
                    supervisorCompleteContractViewModel.StudentName = contractValidation.Student.FirstName + " " + contractValidation.Student.LastName;
                    supervisorCompleteContractViewModel.StudentEmail = contractValidation.Student.Email;
                    supervisorCompleteContractViewModel.StudentPhone = contractValidation.Student.TelephoneNumber;
                    supervisorCompleteContractViewModel.ContractTitle = contractValidation.BaseName;
                    supervisorCompleteContractViewModel.StartDate = contractValidation.StartDate;
                    supervisorCompleteContractViewModel.EndDate = contractValidation.EndDate;
                    supervisorCompleteContractViewModel.InternshipStreet = contractValidation.InternshipStreet;
                    supervisorCompleteContractViewModel.InternshipStreetNumber = contractValidation.InternshipStreetNumber;
                    supervisorCompleteContractViewModel.InternshipPostalCode = contractValidation.InternshipPostalCode;
                    supervisorCompleteContractViewModel.InternshipMunicipality = contractValidation.InternshipMunicipality;
                }
                supervisorCompleteContractViewModel.DepartmentName = contractValidation.Department?.BaseName ?? "Niet toegewezen";
                supervisorCompleteContractViewModel.InternshipTypeName = contractValidation.InternshipType?.BaseName ?? "Niet toegewezen";
            }
            return View(supervisorCompleteContractViewModel);
        }

        string? supervisorIdString = HttpContext.Session.GetString("SupervisorId");
        if (string.IsNullOrEmpty(supervisorIdString)) return RedirectToAction("Login");

        Guid supervisorIdGuid = Guid.Parse(supervisorIdString);

        var organization = _internshipContracts.StageOrganizations
            .FirstOrDefault(o => o.RepresentativeId == supervisorIdGuid);

        if (organization == null) return NotFound("Geen organisatie gekoppeld aan deze supervisor.");

        var contract = _internshipContracts.InternshipContracts
            .FirstOrDefault(c => c.Id == supervisorCompleteContractViewModel.ContractId && c.InternshipOrganizationId == organization.Id);

        if (contract == null) return NotFound();

        if (supervisorCompleteContractViewModel.SupervisourSignature != null)
        {
            var filename = $"{Guid.NewGuid()}_{supervisorCompleteContractViewModel.SupervisourSignature.FileName}";
            var pathToDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(pathToDirectory)) Directory.CreateDirectory(pathToDirectory);
            var pathToFile = Path.Combine(pathToDirectory, filename);
            using (FileStream fileStream = new FileStream(pathToFile, FileMode.Create))
            {
                supervisorCompleteContractViewModel.SupervisourSignature.CopyTo(fileStream);
            }

            contract.SupervisourSignature = filename;
            contract.SupervisourIsSigned = true;
        }
        else
        {
            contract.SupervisourIsSigned = supervisorCompleteContractViewModel.SupervisourIsSigned;
        }


        if (supervisorCompleteContractViewModel.RiskAnalysis != null && supervisorCompleteContractViewModel.HasRiskAnalysis)
        {
            var filename = $"{organization.BaseName}_{Path.GetFileName(supervisorCompleteContractViewModel.RiskAnalysis.FileName)}";
            var pathToDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "files");
            if (!Directory.Exists(pathToDirectory)) Directory.CreateDirectory(pathToDirectory);
            var pathToFile = Path.Combine(pathToDirectory, filename);
            using (FileStream fileStream = new FileStream(pathToFile, FileMode.Create))
            {
                supervisorCompleteContractViewModel.RiskAnalysis.CopyTo(fileStream);
            }

            contract.RiskAnalysis = filename;
            contract.HasRiskAnalysis = true;
        }
        else
        {
            if (string.IsNullOrEmpty(contract.RiskAnalysis))
            {
                contract.HasRiskAnalysis = supervisorCompleteContractViewModel.HasRiskAnalysis;
            }
        }

        contract.BaseName = supervisorCompleteContractViewModel.ContractTitle;
        contract.InternshipStreet = supervisorCompleteContractViewModel.InternshipStreet;
        contract.InternshipStreetNumber = supervisorCompleteContractViewModel.InternshipStreetNumber;
        contract.InternshipPostalCode = supervisorCompleteContractViewModel.InternshipPostalCode;
        contract.InternshipMunicipality = supervisorCompleteContractViewModel.InternshipMunicipality;
        contract.StartDate = supervisorCompleteContractViewModel.StartDate.Date;
        contract.EndDate = supervisorCompleteContractViewModel.EndDate.Date;
        contract.NeedsMedical = supervisorCompleteContractViewModel.NeedsMedical;
        contract.HasRiskAnalysis = supervisorCompleteContractViewModel.HasRiskAnalysis;
        
        
        contract.MedicalResearchLocation = supervisorCompleteContractViewModel.MedicalResearchLocation;
        contract.MedicalResearchLocation = supervisorCompleteContractViewModel.MedicalResearchLocation;
        
        contract.InternshipCoordinatorRemarks = supervisorCompleteContractViewModel.InternshipCoordinatorRemarks;
        
        _internshipContracts.InternshipContracts.Update(contract);

        try
        { 
            _internshipContracts.SaveChanges();
        }
        catch (DbUpdateException dbUpdateException)
        {
            Debug.WriteLine(dbUpdateException.Message);
    
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("SupervisorId");
        HttpContext.Session.Remove("SupervisorName");
        return RedirectToAction("Index", "Home", new { area = "" });
    }

    [HttpGet]
    public IActionResult DownloadRiskAnalysis()
    {
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "files", "Voorbeeld_Risicoanalyse_stageovereenkomst.pdf");
        var bytes = System.IO.File.ReadAllBytes(filePath);
        return File(bytes, "application/pdf", "Voorbeeld_Risicoanalyse_stageovereenkomst.pdf");
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using QuestPDF.Fluent;
using WSI.Goat.InternshipContracts.Core.Entities;
using WSI.Goat.InternshipContracts.Web.Data;
using WSI.Goat.InternshipsContracts.Web.Areas.Mentor.ViewModels;
using WSI.Goat.InternshipsContracts.Web.Services;
using WSI.Goat.InternshipsContracts.Web.Services.Interfaces;
using WSI.Goat.InternshipsContracts.Web.ViewModels;

namespace WSI.Goat.InternshipContracts.Web.Areas.Student.Controllers;

[Area("Student")]
public class StudentController : Controller
{
    private readonly InternshipContractsDbContext _internshipContractsDbContext;
    private readonly IFormBuilder _formBuilder;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public StudentController(InternshipContractsDbContext internshipContractsDbContext, IFormBuilder formBuilder, IWebHostEnvironment webHostEnvironment)
    {
        _internshipContractsDbContext = internshipContractsDbContext;
        _formBuilder = formBuilder;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        string studentName = HttpContext.Session.GetString("StudentName");
        string studentIdString = HttpContext.Session.GetString("StudentId");

        if (string.IsNullOrEmpty(studentName) || string.IsNullOrEmpty(studentIdString))
        {
            return RedirectToAction("Login");
        }
        
        Guid studentIdGuid = Guid.Parse(studentIdString);

        var student = _internshipContractsDbContext.ApplicationUsers
            .FirstOrDefault(s => s.Id == studentIdGuid);
        
        if (student == null)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        var studentContracts = _internshipContractsDbContext.InternshipContracts
            .Include(c => c.InternshipOrganization)
            .Include(c => c.Department)
            .Include(c => c.InternshipType)
            .Include(c => c.Student)
            .Include(c => c.InternshipMentor)
            .Include(c => c.InternshipAttendant)
            .Include(c => c.Articles)
            .Where(c => c.StudentId == studentIdGuid)
            .ToList();

        StudentIndexViewModel studentIndexViewModel = new()
        {
            StudentName = student.FirstName + " " + student.LastName,
            Contracts = studentContracts.Select(c => new InternshipContractDetailViewModel()
            {
                InternshipContractId = c.Id.ToString(),
                InternshipContractName = c.BaseName,
                InternshipStreet = c.InternshipStreet,
                InternshipStreetNumber = c.InternshipStreetNumber,
                InternshipPostalCode = c.InternshipPostalCode,
                InternshipMunicipality = c.InternshipMunicipality,
                InternshipOrganization = c.InternshipOrganization?.BaseName,
                Department = c.Department?.BaseName,
                InternshipType = c.InternshipType?.BaseName,
                Student = c.Student != null
                    ? c.Student.FirstName + " " + c.Student.LastName
                    : "Onbekende student",
                InternshipMentor = c.InternshipMentor != null
                    ? c.InternshipMentor.FirstName + " " + c.InternshipMentor.LastName
                    : "Nog geen mentor toegewezen",
                InternshipAttendant = c.InternshipAttendant != null
                    ? c.InternshipAttendant.FirstName + " " + c.InternshipAttendant.LastName
                    : "Nog geen begeleider toegewezen",
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                NeedsMedical = c.NeedsMedical,
                MentorIsSigned = c.MentorIsSigned,
                StudentIsSigned = c.StudentIsSigned,
                SupervisourIsSigned = c.SupervisourIsSigned,
                StudentSignature = c.StudentSignature,
                Articles = c.Articles.Select(a => new ContractArticleViewModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description
                }).ToList(),
                InternshipCoordinatorRemarks = c.InternshipCoordinatorRemarks,
                RiskAnalysis = c.RiskAnalysis,
                HasRiskAnalysis = c.HasRiskAnalysis
            }).ToList()
        };
        
        return View(studentIndexViewModel);
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("StudentName")))
        {
            return RedirectToAction("Index", "Student", new { area = "Student" });
        }
        
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(StudentLoginViewModel studentLoginViewModel)
    {
        if (!ModelState.IsValid) return View(studentLoginViewModel);
        
        var studentRole = _internshipContractsDbContext.ApplicationRoles
            .FirstOrDefault(ar => ar.BaseName == "Student");
        
        if (studentRole == null)
        {
            ModelState.AddModelError(string.Empty, "Systeemfout: Student rol niet gevonden.");
            return View(studentLoginViewModel);
        }

        var student = _internshipContractsDbContext.ApplicationUsers
            .FirstOrDefault(u => u.Email == studentLoginViewModel.Email && u.Password == studentLoginViewModel.Password 
                                                                        && u.ApplicationRoleId == studentRole.Id);

        if (student != null)
        {
            HttpContext.Session.SetString("StudentId", student.Id.ToString());
            HttpContext.Session.SetString("StudentName", student.FirstName + " " + student.LastName);
            return RedirectToAction("Index");
        }

        ModelState.AddModelError(string.Empty, "Ongeldige login gegevens");
        return View(studentLoginViewModel);
    }

    [HttpGet]
    public IActionResult AddContract()
    {
        string? studentIdString = HttpContext.Session.GetString("StudentId");
        if (string.IsNullOrEmpty(studentIdString)) return RedirectToAction("Login");

        StudentAddContractViewModel studentAddContractViewModel = new ()
        {
            Departments = _formBuilder.GetDepartmentsList(),
            InternshipTypes = _formBuilder.GetInternshipTypesList(),
            Mentors = _formBuilder.GetMentorsList(),
            Articles = _formBuilder.GetArticlesList(),

            StartDate = DateTime.Today,
            EndDate = DateTime.Today
        };
        return View(studentAddContractViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddContract(StudentAddContractViewModel studentAddContractViewModel)
    {

        if (!string.Equals(studentAddContractViewModel.InternshipEmail?.Trim(), "info@azoostende.be"))
        {
            ModelState.AddModelError("InternshipEmail", "Deze organisatie is niet geregistreerd.");
        }
            
        if (!ModelState.IsValid)
        {
            studentAddContractViewModel.Departments = _formBuilder.GetDepartmentsList();
            studentAddContractViewModel.InternshipTypes = _formBuilder.GetInternshipTypesList();
            studentAddContractViewModel.Mentors = _formBuilder.GetMentorsList();
            studentAddContractViewModel.Articles = _formBuilder.GetArticlesList(); 
            return View(studentAddContractViewModel);
        }
        
        string? studentIdString = HttpContext.Session.GetString("StudentId");
        if (string.IsNullOrEmpty(studentIdString)) return RedirectToAction("Login");

        var department = _internshipContractsDbContext.Departments
            .Include(d => d.Cluster)
            .FirstOrDefault(d => d.Id == studentAddContractViewModel.SelectedDepartment);
        
        if (department == null)
        {
            ModelState.AddModelError("SelectedDepartment", "De geselecteerde opleiding bestaat niet.");
            studentAddContractViewModel.Departments = _formBuilder.GetDepartmentsList();
            studentAddContractViewModel.InternshipTypes = _formBuilder.GetInternshipTypesList();
            studentAddContractViewModel.Mentors = _formBuilder.GetMentorsList();
            studentAddContractViewModel.Articles = _formBuilder.GetArticlesList();
            return View(studentAddContractViewModel);
        }
        
        var articles = _internshipContractsDbContext.Articles.ToList();
        
        var supervisor = _internshipContractsDbContext.ApplicationUsers
            .FirstOrDefault(u => u.Email == studentAddContractViewModel.InternshipEmail);
        
        var organisationCheck = _internshipContractsDbContext.StageOrganizations
            .FirstOrDefault(o => supervisor != null && o.RepresentativeId == supervisor.Id);

        Guid organizationId;
        
        if (organisationCheck != null)
        {
            organizationId = organisationCheck.Id;
        }
        else
        {
            var newOrganisation = new StageOrganization()
            {
                Id = Guid.NewGuid(),
                BaseName = "AZ Oostende",
                OrganizationNumber = "BE 0800.023.336",
                Street = studentAddContractViewModel.InternshipStreet,
                Number = studentAddContractViewModel.InternshipStreetNumber,
                PostalCode = studentAddContractViewModel.InternshipPostalCode,
                Municipality = studentAddContractViewModel.InternshipMunicipality,
                RepresentativeId = supervisor?.Id,
                Created = DateTime.Now
            };
            
            _internshipContractsDbContext.StageOrganizations.Add(newOrganisation);
            _internshipContractsDbContext.SaveChanges();

            organizationId = newOrganisation.Id;
        }

        var contract = new InternshipContract()
        {
            Id = Guid.NewGuid(),
            BaseName = studentAddContractViewModel.ContractTitle,
            Created = DateTime.Now,
            StudentId = Guid.Parse(studentIdString),
            DepartmentId = studentAddContractViewModel.SelectedDepartment,
            InternshipTypeId = studentAddContractViewModel.SelectedInternshipType,
            InternshipAttendantId = department.StageCoordinatorId,
            InternshipMentorId = null,
            InternshipOrganizationId = organizationId,
            StartDate = studentAddContractViewModel.StartDate.Date,
            EndDate = studentAddContractViewModel.EndDate.Date,
            InternshipStreet = studentAddContractViewModel.InternshipStreet,
            InternshipStreetNumber = studentAddContractViewModel.InternshipStreetNumber,
            InternshipPostalCode = studentAddContractViewModel.InternshipPostalCode,
            InternshipMunicipality = studentAddContractViewModel.InternshipMunicipality,
            Articles = articles,
            InternshipCoordinatorRemarks = "",
            MentorIsSigned = false,
            StudentIsSigned = false,
            SupervisourIsSigned = false,
            HasRiskAnalysis = false,
        };
        
        _internshipContractsDbContext.InternshipContracts.Add(contract);
        _internshipContractsDbContext.SaveChanges();

        TempData["ContractSend"] = "Het contract is succesvol doorgestuurd naar de stageplaats!";
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ShowContractDetails(Guid id)
    {
        string? studentIdString = HttpContext.Session.GetString("StudentId");
        if (string.IsNullOrEmpty(studentIdString)) return RedirectToAction("Login");
        
        var contract = _internshipContractsDbContext.InternshipContracts
            .Include(c => c.Department)
            .Include(c => c.InternshipType)
            .Include(c => c.Student)
            .Include(c => c.InternshipMentor)
            .Include(c => c.InternshipAttendant)
            .Include(c => c.Articles)
            .FirstOrDefault(c => c.Id == id && c.StudentId == Guid.Parse(studentIdString));

        if (contract == null)
        {
            return NotFound();
        }

        StudentShowContractDetailsViewModel studentShowContractDetailsViewModel = new()
        {
            Id = contract.Id,
            InternshipContractName = contract.BaseName,
            DepartmentName = contract.Department?.BaseName,
            InternshipTypeName = contract.InternshipType?.BaseName,
            StudentName = $"{contract.Student?.FirstName} {contract.Student?.LastName}",
            MentorName = contract.InternshipMentor != null
                ? $"{contract.InternshipMentor.FirstName} {contract.InternshipMentor.LastName}"
                : "Nog geen mentor toegewezen",
            AttendantName = contract.InternshipAttendant != null
                ? $"{contract.InternshipAttendant.FirstName} {contract.InternshipAttendant.LastName}"
                : "Nog geen begeleider toegewezen",
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            InternshipStreet = contract.InternshipStreet,
            InternshipStreetNumber = contract.InternshipStreetNumber,
            InternshipPostalCode = contract.InternshipPostalCode,
            InternshipMunicipality = contract.InternshipMunicipality,
            NeedsMedical = contract.NeedsMedical,
            MentorIsSigned = contract.MentorIsSigned,
            StudentIsSigned = contract.StudentIsSigned,
            SupervisourIsSigned = contract.SupervisourIsSigned,
            HasRiskAnalysis = contract.HasRiskAnalysis,
            Remarks = contract.InternshipCoordinatorRemarks,
            CoordinatorValidation = contract.CoordinatorValidation,
            MedicalResearchDate = contract.StartDate2,
            MedicalResearchLocation = contract.MedicalResearchLocation,
            ContractId = contract.Id,
            RiskAnalysis = contract.RiskAnalysis,

            PageUrl = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}"),
            Articles = contract.Articles.Select(a => new ContractArticleViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description
            }).ToList(),
        };        
        return View(studentShowContractDetailsViewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ShowContractDetails(StudentShowContractDetailsViewModel studentShowContractDetailsViewModel)
    {
        var studentContract = _internshipContractsDbContext.InternshipContracts
        .Include(c => c.Department)
        .Include(c => c.InternshipType)
        .Include(c => c.Student)
        .Include(c => c.InternshipMentor)
        .Include(c => c.InternshipAttendant)
        .Include(c => c.Articles)
           .FirstOrDefault(s => s.Id.Equals(studentShowContractDetailsViewModel.ContractId));

        if (studentContract == null) return NotFound();

        if (studentShowContractDetailsViewModel.StudentSignature != null)
        {
            var filename = $"{Guid.NewGuid()}_{Path.GetFileName(studentShowContractDetailsViewModel.StudentSignature.FileName)}";
            var pathToDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "files");

            if (!Directory.Exists(pathToDirectory))
            {
                Directory.CreateDirectory(pathToDirectory);
            }

            var pathToFile = Path.Combine(pathToDirectory, filename);

            using (FileStream fileStream = new FileStream(pathToFile, FileMode.Create))
            {
                studentShowContractDetailsViewModel.StudentSignature.CopyTo(fileStream);
            }

            studentContract.StudentSignature = filename;
            studentContract.Updated = DateTime.Now;
            studentContract.StudentIsSigned = true;
        }

        _internshipContractsDbContext.InternshipContracts.Update(studentContract);

        try
        {
            _internshipContractsDbContext.SaveChanges();
        }

        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.Message);
        }

        // FIX: Geef het ID expliciet mee als anoniem object via 'new { id = ... }'
        return RedirectToAction(nameof(ShowContractDetails), new { id = studentContract.Id });
    }
    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        HttpContext.Session.Remove("StudentId");
        HttpContext.Session.Remove("StudentName");
        return RedirectToAction("Index", "Home", new { area = "" });
    }

    [HttpGet]
    public IActionResult DownloadContract(Guid id)
    {
        var contract = _internshipContractsDbContext.InternshipContracts
            .Include(c => c.Articles)
            .Include(c => c.Department)
            .Include(c => c.InternshipType)
            .Include(c => c.InternshipMentor)
            .Include(c => c.InternshipOrganization)
            .Include(c => c.Student)
            .FirstOrDefault(c => c.Id == id);
        
        var internshipOrganization = contract.InternshipOrganization;
        var student = contract.Student;
        
        if (contract == null)
        {
            return NotFound();
        }

        ContractPDFViewModel contractPDFViewModel = new ContractPDFViewModel
        {
            ContractTitle = contract.BaseName,
            Department = contract.Department?.BaseName ?? "Onbekend",
            InternshipType = contract.InternshipType?.BaseName ?? "Onbekend",
            Mentor = contract.InternshipMentor?.BaseName ?? "Nog geen mentor toegewezen",
            InternshipStreet = contract.InternshipStreet,
            InternshipStreetNumber = contract.InternshipStreetNumber,
            InternshipPostalCode = contract.InternshipPostalCode,
            InternshipMunicipality = contract.InternshipMunicipality,
            NeedsMedical = contract.NeedsMedical,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            StageOrganizationName = internshipOrganization.BaseName ?? "Onbekend",
            StudentName = student.FirstName + " " + student.LastName ?? "Onbekend",
            StudentEmail = student.Email ?? "Onbekend",
            StudentTelephone = student.TelephoneNumber,
            Articles = contract.Articles.Select(a => new ContractArticleViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description
            }).ToList()

        };

        var document = new PDFBuilder(contractPDFViewModel);
        byte[] pdfBytes = document.GeneratePdf();

        return File(pdfBytes, "application/pdf", $"Howest_Stageovereenkomst_{contract.Id}.pdf");
    }

}
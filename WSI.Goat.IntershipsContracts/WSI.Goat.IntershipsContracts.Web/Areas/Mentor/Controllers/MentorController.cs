using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Diagnostics.Contracts;
using WSI.Goat.InternshipContracts.Web.Controllers;
using WSI.Goat.InternshipContracts.Web.Data;
using WSI.Goat.InternshipContracts.Web.Models;
using WSI.Goat.InternshipsContracts.Web.Areas.Mentor.ViewModels;
using WSI.Goat.InternshipsContracts.Web.ViewModels;

namespace WSI.Goat.InternshipContracts.Web.Areas.Mentor.Controllers;

[Area("Mentor")]
public class MentorController : Controller
{
    private readonly InternshipContractsDbContext _internshipContractsDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    
    public MentorController(InternshipContractsDbContext internshipContractsDbContext, IWebHostEnvironment webHostEnvironment)
    {
        _internshipContractsDbContext = internshipContractsDbContext;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        string? mentorIdString = HttpContext.Session.GetString("MentorId");
        string? mentorName = HttpContext.Session.GetString("MentorName");

        if (string.IsNullOrEmpty(mentorIdString) || string.IsNullOrEmpty(mentorName))
        {
            return RedirectToAction("Login");
        }

        Guid mentorIdGuid = Guid.Parse(mentorIdString);

        var mentor = _internshipContractsDbContext.ApplicationUsers
            .FirstOrDefault(m => m.Id == mentorIdGuid);

        if (mentor == null) return NotFound();
        
        var mentorContracts = _internshipContractsDbContext.InternshipContracts
            .Include(c => c.InternshipOrganization)
            .Include(c => c.Department)
            .Include(c => c.InternshipType)
            .Include(c => c.Student)
            .Include(c => c.Articles)
            .Where(c => c.InternshipMentorId == mentorIdGuid &&
                    c.CoordinatorValidation == true)
            .ToList();

        MentorIndexViewModel mentorIndexViewModel = new ()
        {
            MentorName = mentor.FirstName + " " + mentor.LastName,
            Contracts = mentorContracts.Select(c => new MentorStudentsInfoViewModel()
            {
                ContractId = c.Id,
                StudentId = c.StudentId,
                UserName = c.BaseName,
                StudentName = c.Student.FirstName + " " + c.Student.LastName,
                StudentEmail = c.Student?.Email ?? "Geen e-mail",
                StudentTelephoneNumber = c.Student?.TelephoneNumber ?? "Geen telefoonnummer",
                StudentSignature = c.Student?.Signature,
                StudentRepresentedOrganisation = c.InternshipOrganization?.BaseName ?? "Geen organisatie gekoppeld",
                IsSigned = c.MentorIsSigned,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Articles = c.Articles.Select(a => new ContractArticleViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description
                }).ToList()

            }).ToList()
        };
        return View(mentorIndexViewModel);
    }

    [HttpGet]
    public IActionResult ViewStudentContract(Guid contractId)
    {
        var studentContract = _internshipContractsDbContext.InternshipContracts
                .Include(c => c.Department)
                .Include(c => c.InternshipType)
                .Include(c => c.Student)
                .Include(c => c.InternshipMentor)
                .Include(c => c.InternshipAttendant)
                .Include(c => c.Articles)
            .FirstOrDefault(c => c.Id == contractId);

        if (studentContract == null) return NotFound();

        MentorViewStudentContractViewModel mentorViewStudentContractViewModel = new()
        {

            ContractId = studentContract.Id,
            InternshipContractName = studentContract.BaseName,
            DepartmentName = studentContract.Department?.BaseName,
            InternshipTypeName = studentContract.InternshipType?.BaseName,
            StudentName = $"{studentContract.Student?.FirstName} {studentContract.Student?.LastName}",
            MentorName = studentContract.InternshipMentor != null
                ? $"{studentContract.InternshipMentor.FirstName} {studentContract.InternshipMentor.LastName}"
                 : "Nog geen mentor toegewezen",
            AttendantName = studentContract.InternshipAttendant != null
                 ? $"{studentContract.InternshipAttendant.FirstName} {studentContract.InternshipAttendant.LastName}"
                 : "Nog geen begeleider toegewezen",
            StartDate = studentContract.StartDate,
            EndDate = studentContract.EndDate,
            InternshipStreet = studentContract.InternshipStreet,
            InternshipStreetNumber = studentContract.InternshipStreetNumber,
            InternshipPostalCode = studentContract.InternshipPostalCode,
            InternshipMunicipality = studentContract.InternshipMunicipality,
            NeedsMedical = studentContract.NeedsMedical,
            MentorIsSigned = studentContract.MentorIsSigned,
            StudentIsSigned = studentContract.StudentIsSigned,
            CoordinatorValidation = studentContract.CoordinatorValidation,
            SupervisourIsSigned = studentContract.SupervisourIsSigned,
            HasRiskAnalysis = studentContract.HasRiskAnalysis,
            Articles = studentContract.Articles.Select(a => new ContractArticleViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description
            }).ToList()
        };

        return View(mentorViewStudentContractViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ViewStudentContract(MentorViewStudentContractViewModel mentorViewStudentContractViewModel)
    {
        var studentContract = _internshipContractsDbContext.InternshipContracts
        .Include(c => c.Department)
        .Include(c => c.InternshipType)
        .Include(c => c.Student)
        .Include(c => c.InternshipMentor)
        .Include(c => c.InternshipAttendant)
        .Include(c => c.Articles)
                .FirstOrDefault(c => c.Id == mentorViewStudentContractViewModel.ContractId);

        if (studentContract == null) return NotFound();

        if (mentorViewStudentContractViewModel.MentorSignature != null)
        {
            var filename = $"{mentorViewStudentContractViewModel.MentorSignature.FileName}";
            var pathToDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "files");

            if (!Directory.Exists(pathToDirectory))
            {
                Directory.CreateDirectory(pathToDirectory);
            }

            var pathToFile = Path.Combine(pathToDirectory, filename);

            using (FileStream fileStream = new FileStream(pathToFile, FileMode.Create))
            {
                mentorViewStudentContractViewModel.MentorSignature.CopyTo(fileStream);
            }

            studentContract.MentorSignature = filename;
            studentContract.Updated = DateTime.Now;
            studentContract.MentorIsSigned = true;
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

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("MentorName")))
        {
            return RedirectToAction("Index", "Mentor", new { area = "Mentor" });
        }
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(MentorLoginViewModel mentorLoginViewModel)
    {
        if (!ModelState.IsValid) return View(mentorLoginViewModel);

        var mentor = _internshipContractsDbContext.ApplicationUsers
            .FirstOrDefault(u => u.Email == mentorLoginViewModel.Email && u.Password == mentorLoginViewModel.Password);

        if (mentor != null)
        {
            HttpContext.Session.SetString("MentorId", mentor.Id.ToString());
            HttpContext.Session.SetString("MentorName", mentor.FirstName + " " + mentor.LastName);
            return RedirectToAction("Index");
        }
        
        ModelState.AddModelError(string.Empty, "Ongeldige login gegevens");
        return View(mentorLoginViewModel);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        HttpContext.Session.Remove("MentorId");
        HttpContext.Session.Remove("MentorName");
        return RedirectToAction("Index", "Home", new { area = "" });
    }
}
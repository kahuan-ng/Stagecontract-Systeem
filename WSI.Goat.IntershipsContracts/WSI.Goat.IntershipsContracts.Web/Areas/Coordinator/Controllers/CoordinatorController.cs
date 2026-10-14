using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using QuestPDF.Fluent;
using WSI.Goat.InternshipContracts.Web.Data;
using WSI.Goat.InternshipsContracts.Web.Areas.Coordinator.ViewModels;
using WSI.Goat.InternshipsContracts.Web.Areas.Mentor.ViewModels;
using WSI.Goat.InternshipsContracts.Web.Services;
using WSI.Goat.InternshipsContracts.Web.Services.Interfaces;
using WSI.Goat.InternshipsContracts.Web.ViewModels;

namespace WSI.Goat.InternshipsContracts.Web.Areas.coordinator.Controllers
{
    [Area("Coordinator")]
    public class CoordinatorController : Controller
    {
        private readonly InternshipContractsDbContext _internshipContractsDbContext;
        private readonly IFormBuilder _formbuilder;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CoordinatorController(InternshipContractsDbContext internshipContractsDbContext, IFormBuilder formbuilder, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            _internshipContractsDbContext = internshipContractsDbContext;
            _formbuilder = formbuilder;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender; 
        }
        
        public IActionResult Index()
        {
            string? coordinatorIdString = HttpContext.Session.GetString("CoordinatorId");
            string? coordinatorName = HttpContext.Session.GetString("CoordinatorName");

            if (string.IsNullOrEmpty(coordinatorIdString) || string.IsNullOrEmpty(coordinatorName))
            {
                return RedirectToAction("Login");
            }

            Guid coordinatorIdGuid = Guid.Parse(coordinatorIdString);

            var coordinator = _internshipContractsDbContext.ApplicationUsers
                                .FirstOrDefault(c => c.Id == coordinatorIdGuid);

            var department = _internshipContractsDbContext.Departments
                                .FirstOrDefault(d => d.StageCoordinatorId == coordinator.Id);

            if (coordinator == null) return NotFound();

            var contracts = _internshipContractsDbContext.InternshipContracts
            .Include(c => c.InternshipOrganization)
            .Include(c => c.Department)
            .Include(c => c.InternshipType)
            .Include(c => c.Student)
            .Where(c => c.Department == department)
            .ToList();

            CoordinatorIndexViewModel coordinatorIndexViewModel = new()
            {
                CoordinatorName = coordinator.FirstName + " " + coordinator.LastName,
                Contracts = contracts.Select(c => new CoordinatorStudentInfoViewModel()
                {
                    ContractId = c.Id,
                    StudentId = c.StudentId,
                    UserName = c.BaseName,
                    StudentName = c.Student.FirstName + " " + c.Student.LastName,
                    StudentEmail = c.Student?.Email ?? "Geen e-mail",
                    StudentTelephoneNumber = c.Student?.TelephoneNumber ?? "Geen telefoonnummer",
                    StudentSignature = c.Student?.Signature,
                    StudentRepresentedOrganisation = c.InternshipOrganization?.BaseName ?? "Geen organisatie gekoppeld",
                    OrganizationEmail = "info@azoostende.be",
                    IsSigned = c.MentorIsSigned,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate
                }).ToList()
            };
            
            return View(coordinatorIndexViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("CoordinatorName")))
            {
                return RedirectToAction("Index", "Coordinator", new { area = "Coordinator" });
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(CoordinatorLoginViewModel coordinatorLoginViewModel)
        {
            if (!ModelState.IsValid) return View(coordinatorLoginViewModel);

            var coordinatorRole = _internshipContractsDbContext.ApplicationRoles
            .FirstOrDefault(ar => ar.BaseName == "Coordinator");

            if (coordinatorRole == null)
            {
                ModelState.AddModelError(string.Empty, "Systeemfout: Supervisor rol niet gevonden.");
                return View(coordinatorLoginViewModel);
            }

            var coordinator = _internshipContractsDbContext.ApplicationUsers
                .FirstOrDefault(u => u.Email == coordinatorLoginViewModel.Email
                                && u.Password == coordinatorLoginViewModel.Password
                                && u.ApplicationRoleId == coordinatorRole.Id
                                );

            if (coordinator != null)
            {
                HttpContext.Session.SetString("CoordinatorId", coordinator.Id.ToString());
                HttpContext.Session.SetString("CoordinatorName", coordinator.FirstName + " " + coordinator.LastName);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Ongeldige login gegevens");
            return View(coordinatorLoginViewModel);
        }
        
        public IActionResult ViewStudentContract(Guid id)
        {
            var studentContract = _internshipContractsDbContext.InternshipContracts
             .Include(c => c.Department)
             .Include(c => c.InternshipType)
             .Include(c => c.Student)
             .Include(c => c.InternshipMentor)
             .Include(c => c.InternshipAttendant)
            .Include(c => c.Articles)
         .FirstOrDefault(s => s.Id == id);

            if (studentContract == null) return NotFound();

            CoordinatorViewStudentContractViewModel coordinatorViewStudentContractViewModel = new()
            {
                Id = studentContract.Id,
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
                SupervisourIsSigned = studentContract.SupervisourIsSigned,
                CoordinatorValidation = studentContract.CoordinatorValidation,
                HasRiskAnalysis = studentContract.HasRiskAnalysis,
                CoordinatorRemarks = studentContract.InternshipCoordinatorRemarks,
                Articles = studentContract.Articles.Select(a => new ContractArticleViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description
                }).ToList()
            };

            return View(coordinatorViewStudentContractViewModel);
        }

        [HttpGet]
        public IActionResult EditStudentContract(Guid id)
        {
            var studentContract = _internshipContractsDbContext.InternshipContracts
                                 .Include(c => c.Department)
                                 .Include(c => c.InternshipType)
                                 .Include(c => c.Student)
                                 .Include(c => c.InternshipMentor)
                                 .Include(c => c.InternshipAttendant)
                                 .Include(c => c.Articles)
                                .FirstOrDefault(c => c.Id == id);

            if (studentContract == null) return NotFound();

            CoordinatorEditStudentContractViewModel coordinatorEditStudentContractViewModel = new CoordinatorEditStudentContractViewModel()
            {
                Id = studentContract.Id,
                StudentId = studentContract.StudentId,
                InternshipContractName = studentContract.BaseName,
                StudentName = $"{studentContract.Student?.FirstName} {studentContract.Student?.LastName}",
                AttendantName = studentContract.InternshipAttendant != null
                                ? $"{studentContract.InternshipAttendant.FirstName} {studentContract.InternshipAttendant.LastName}" :
                                    "Nog geen begeleider toegewezen",
                StartDate = studentContract.StartDate,
                EndDate = studentContract.EndDate,
                InternshipStreet = studentContract.InternshipStreet,
                InternshipStreetNumber = studentContract.InternshipStreetNumber,
                InternshipPostalCode = studentContract.InternshipPostalCode,
                InternshipMunicipality = studentContract.InternshipMunicipality,

                SelectedDepartment = studentContract.DepartmentId,
                SelectedInternshipType = studentContract.InternshipTypeId,
                SelectedMentor = studentContract.InternshipMentorId,
                OrganisationEmail = "info@azoostende.be",

                NeedsMedical = studentContract.NeedsMedical,
                MentorIsSigned = studentContract.MentorIsSigned,
                StudentIsSigned = studentContract.StudentIsSigned,
                SupervisourIsSigned = studentContract.SupervisourIsSigned,
                CoordinatorValidation = studentContract.CoordinatorValidation,
                HasRiskAnalysis = studentContract.HasRiskAnalysis,
                InternshipCoordinatorRemarks = studentContract.InternshipCoordinatorRemarks,
                MedicalResearchDate = studentContract.StartDate2,
                MedicalResearchLocation = studentContract.MedicalResearchLocation,
                
                
                Articles = studentContract.Articles.Select(a => new ContractArticleViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description
                }).ToList(),

                Mentors = _formbuilder.GetMentorsList(),
                Departments = _formbuilder.GetDepartmentsList(),
                InternshipTypes = _formbuilder.GetInternshipTypesList()
            };
            
            return View(coordinatorEditStudentContractViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudentContract(CoordinatorEditStudentContractViewModel coordinatorEditStudentContractViewModel)
        {
            if (!ModelState.IsValid)
            {
                coordinatorEditStudentContractViewModel.Mentors = _formbuilder.GetMentorsList();
                coordinatorEditStudentContractViewModel.Departments = _formbuilder.GetDepartmentsList();
                coordinatorEditStudentContractViewModel.InternshipTypes = _formbuilder.GetInternshipTypesList();
                
                return View(coordinatorEditStudentContractViewModel);
            }

            var updatedStudentContract = _internshipContractsDbContext.InternshipContracts
                                .Include(c => c.Department)
                                .Include(c => c.InternshipType)
                                .Include(c => c.Student)
                                .Include(c => c.InternshipMentor)
                                .Include(c => c.InternshipAttendant)
                                .Include(c => c.Articles)
                               .FirstOrDefault(c => c.Id == coordinatorEditStudentContractViewModel.Id);

            updatedStudentContract.BaseName = coordinatorEditStudentContractViewModel.InternshipContractName;
            updatedStudentContract.Created = coordinatorEditStudentContractViewModel.StartDate;
            updatedStudentContract.EndDate = coordinatorEditStudentContractViewModel.EndDate;
            updatedStudentContract.HasRiskAnalysis = coordinatorEditStudentContractViewModel.HasRiskAnalysis;
            updatedStudentContract.InternshipMunicipality = coordinatorEditStudentContractViewModel.InternshipMunicipality;
            updatedStudentContract.InternshipStreetNumber = coordinatorEditStudentContractViewModel.InternshipStreetNumber;

            updatedStudentContract.StudentId = coordinatorEditStudentContractViewModel.StudentId;
            updatedStudentContract.InternshipMentorId = coordinatorEditStudentContractViewModel.SelectedMentor;
            updatedStudentContract.DepartmentId = coordinatorEditStudentContractViewModel.SelectedDepartment;
            updatedStudentContract.InternshipTypeId = coordinatorEditStudentContractViewModel.SelectedInternshipType;
            updatedStudentContract.InternshipCoordinatorRemarks = coordinatorEditStudentContractViewModel.InternshipCoordinatorRemarks;
            updatedStudentContract.CoordinatorValidation = coordinatorEditStudentContractViewModel.CoordinatorValidation;
            updatedStudentContract.StartDate2 = coordinatorEditStudentContractViewModel.MedicalResearchDate;
            updatedStudentContract.MedicalResearchLocation = coordinatorEditStudentContractViewModel.MedicalResearchLocation;
            updatedStudentContract.InternshipCoordinatorRemarks = coordinatorEditStudentContractViewModel.InternshipCoordinatorRemarks;

            foreach(var article in coordinatorEditStudentContractViewModel.Articles)
            {
                var dbArticle = updatedStudentContract.Articles.FirstOrDefault(a => a.Id == article.Id);

                if(dbArticle != null)
                {
                    dbArticle.Title = article.Title;
                    dbArticle.Description = article.Description;
                }
            }

            _internshipContractsDbContext.InternshipContracts.Update(updatedStudentContract);


            if (updatedStudentContract.InternshipCoordinatorRemarks != null)
            {
                var receiver = "dylan.hourez@student.howest.be"; // IN PRODUCTION CHANGE TO updatedStudentContract.InternshipOrganization.Representative.Email;
                var pageUrl = Url.Action(
                    action: "Index",
                    controller: "Supervisor",
                    values: new { Area = "Supervisor"},
                    protocol: Request.Scheme
                    );
                
                var subject = $"Nieuwe Opmerkingen van Stagecontract {updatedStudentContract.BaseName}";
                var message = $"<p>Het stagecoördinator van Howest heeft uw contract met student "
            + $"<strong>{updatedStudentContract.Student.FirstName} {updatedStudentContract.Student.LastName}</strong> "
            + $"overlopen en heeft de volgende opmerkingen:</p>"
            + $"<blockquote>{updatedStudentContract.InternshipCoordinatorRemarks}</blockquote>"
            + $"<p>Klik op de onderstaande link om het contract te bekijken:<br />"
            + $"<a href='{pageUrl}' style='color: #0056b3; font-weight: bold;'>Bekijk Stagecontract</a></p>";

                if (updatedStudentContract.SupervisourIsSigned)
                {
                    TempData["EmailStatus"] = "Jouw opmerkingen zijn succesvol doorgestuurd naar het Stagebedrijf!";
                }

                await _emailSender.SendEmailAsync(receiver, subject, message);
            }

            try { _internshipContractsDbContext.SaveChanges(); }

            catch (DbUpdateException DbuE)
            {
                Debug.WriteLine(DbuE.Message);
            }

            return RedirectToAction(nameof(ViewStudentContract), new { id = updatedStudentContract.Id });
        }

        [HttpGet]
        public IActionResult Search()
        {
            CoordinatorSearchViewModel coordinatorSearchViewModel = new()
            {
                Results = new List<CoordinatorStudentInfoViewModel>()
            };
            
            return View(coordinatorSearchViewModel);
        }
        
        [HttpPost]
        public IActionResult Search(CoordinatorSearchViewModel coordinatorSearchViewModel)
        {
            string? coordinatorIdString = HttpContext.Session.GetString("CoordinatorId");
            
            if (string.IsNullOrEmpty(coordinatorIdString))
            {
                return RedirectToAction("Login");
            }

            Guid coordinatorIdGuid = Guid.Parse(coordinatorIdString);

            var department = _internshipContractsDbContext.Departments
                .FirstOrDefault(d => d.StageCoordinatorId == coordinatorIdGuid);

            if (department == null) return NotFound("Geen departement gevonden.");
            
            var contract = _internshipContractsDbContext.InternshipContracts
                .Include(c => c.InternshipOrganization)
                .Include(c => c.Student)
                .Where(c => c.DepartmentId == department.Id);

            if (!string.IsNullOrWhiteSpace(coordinatorSearchViewModel.StudentName))
            {
                string searchString = coordinatorSearchViewModel.StudentName.Trim().ToLower();
                contract = contract.Where(c => 
                    c.Student.FirstName.ToLower().Contains(searchString) || 
                    c.Student.LastName.ToLower().Contains(searchString));
            }

            coordinatorSearchViewModel.Results = contract.Select(c => new CoordinatorStudentInfoViewModel
            {
                ContractId = c.Id,
                StudentName = c.Student.FirstName + " " + c.Student.LastName,
                StudentEmail = c.Student.Email,
                StudentTelephoneNumber = c.Student.TelephoneNumber,
                StudentSignature = c.Student.Signature,
                StudentRepresentedOrganisation = c.InternshipOrganization.BaseName,
                StartDate = c.StartDate,
                EndDate = c.EndDate
            }).ToList();

            return View(coordinatorSearchViewModel);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            HttpContext.Session.Remove("CoordinatorId");
            HttpContext.Session.Remove("CoordinatorName");
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        public IActionResult DownloadRiskAnalysis()
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "files", "Voorbeeld_Risicoanalyse_stageovereenkomst.pdf");
            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "application/pdf", "Voorbeeld_Risicoanalyse_stageovereenkomst.pdf");
        }
        [HttpGet]
        
        public IActionResult DownloadStageContract(Guid id)
        {
            var contract = _internshipContractsDbContext.InternshipContracts
                .Include(c => c.Articles)
                .Include(c => c.Department)
                .Include(c => c.InternshipType)
                .Include(c => c.InternshipMentor)
                .Include(c => c.InternshipOrganization)
                .Include(c => c.Student)
                .FirstOrDefault(c => c.Id == id);
            
            if (contract == null)
            {
                return NotFound();
            }

            var internshipOrganization = contract.InternshipOrganization;
            var student = contract.Student;

            ContractPDFViewModel contractPDFViewModel = new ContractPDFViewModel
            {
                ContractTitle = contract.BaseName,
                Department = contract.Department?.BaseName ?? "Onbekend",
                InternshipType = contract.InternshipType?.BaseName ?? "Onbekend",
                Mentor = contract.InternshipMentor != null 
                    ? contract.InternshipMentor.FirstName + " " + contract.InternshipMentor.LastName 
                    : "Nog geen mentor toegewezen",
                InternshipStreet = contract.InternshipStreet,
                InternshipStreetNumber = contract.InternshipStreetNumber,
                InternshipPostalCode = contract.InternshipPostalCode,
                InternshipMunicipality = contract.InternshipMunicipality,
                NeedsMedical = contract.NeedsMedical,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                StageOrganizationName = internshipOrganization?.BaseName ?? "Onbekend",
                StudentName = student != null ? student.FirstName + " " + student.LastName : "Onbekend",
                StudentEmail = student?.Email ?? "Onbekend",
                StudentTelephone = student?.TelephoneNumber,
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
}
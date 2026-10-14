using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WSI.Goat.InternshipContracts.Core.Entities;
using WSI.Goat.InternshipsContracts.Web.ViewModels;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Coordinator.ViewModels
{
    public class CoordinatorEditStudentContractViewModel
    {
        public Guid Id { get; set; }
        [HiddenInput]
        public Guid? StudentId { get; set; }

        [Display(Name = "Contract Titel")]
        public string InternshipContractName { get; set; }

        [Display(Name = "Student")]
        public string StudentName { get; set; }

        [Required]
        public Guid? SelectedDepartment { get; set; }
        public IEnumerable<SelectListItem>? Departments { get; set; }

        [Display(Name = "Type Stage")]
        [Required]
        public Guid? SelectedInternshipType { get; set; }
        public IEnumerable<SelectListItem>? InternshipTypes { get; set; }

        [Display(Name = "Mentor")]
        [Required]
        public Guid? SelectedMentor { get; set; }

        public List<SelectListItem>? Mentors { get; set; }

        [Display(Name = "Stagebegeleider (Coördinator)")]
        public string AttendantName { get; set; }

        [Required(ErrorMessage = "Startdatum is verplicht")]
        [DataType(DataType.Date)]

        [Display(Name = "Start datum stage")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Einddatum is verplicht")]
        [DataType(DataType.Date)]
        [Display(Name = "Eind datum stage")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Straat")]
        public string InternshipStreet { get; set; }

        [Display(Name = "Huisnummer")]
        public int InternshipStreetNumber { get; set; }

        [Display(Name = "Postcode")]
        public string InternshipPostalCode { get; set; }

        [Display(Name = "Gemeente")]
        public string InternshipMunicipality { get; set; }

        [Display(Name = "Medische Keuring Vereist")]
        public bool NeedsMedical { get; set; }

        [Display(Name = "Getekent")]
        public bool MentorIsSigned { get; set; }

        [Display(Name = "Getekent")]
        public bool StudentIsSigned { get; set; }

        [Display(Name = "Getekent")]
        public bool SupervisourIsSigned { get; set; }

        [Display(Name = "Gekeurt")]
        public bool CoordinatorValidation { get; set; }

        [Display(Name = "Risicoanalyse Aanwezig")]
        public bool HasRiskAnalysis { get; set; }

        [Display(Name = "Organisatie Email")]
        public string OrganisationEmail { get; set; }

        [Display(Name = "Datum Medisch onderzoek")]
        public DateTime? MedicalResearchDate { get; set; }

        [Display(Name = "Medisch Onderzoek Locatie")]
        public string? MedicalResearchLocation { get; set; }

        [Display(Name = "Opmerkingen Stagecoördinator")]
        public string? InternshipCoordinatorRemarks { get; set; }

        public List<ContractArticleViewModel>? Articles { get; set; } = new List<ContractArticleViewModel>();
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WSI.Goat.InternshipsContracts.Web.ViewModels;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Mentor.ViewModels
{
    public class MentorViewStudentContractViewModel
    {

        [HiddenInput]
        public Guid ContractId { get; set; }

        [Display(Name = "Contract Titel")]
        public string InternshipContractName { get; set; }

        [Display(Name = "Opleiding")]
        public string DepartmentName { get; set; }

        [Display(Name = "Type Stage")]
        public string InternshipTypeName { get; set; }

        [Display(Name = "Student")]
        public string StudentName { get; set; }

        [Display(Name = "Stagementor")]
        public string MentorName { get; set; }

        [Display(Name = "Stagebegeleider (Coördinator)")]
        public string AttendantName { get; set; }

        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Einddatum")]
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

        [Display(Name = "Getekend")]
        public bool MentorIsSigned { get; set; }

        [Display(Name = "Getekend")]
        public bool StudentIsSigned { get; set; }

        [Display(Name = "Getekend")]
        public bool SupervisourIsSigned { get; set; }

        [Display(Name = "Gekeurd")]
        public bool CoordinatorValidation { get; set; }

        [Display(Name = "Risicoanalyse Aanwezig")]
        public bool HasRiskAnalysis { get; set; }

        [Display(Name = "Handtekening van mentor")]
        public IFormFile MentorSignature { get; set; }
        public IEnumerable<ContractArticleViewModel>? Articles { get; set; }
    }
}

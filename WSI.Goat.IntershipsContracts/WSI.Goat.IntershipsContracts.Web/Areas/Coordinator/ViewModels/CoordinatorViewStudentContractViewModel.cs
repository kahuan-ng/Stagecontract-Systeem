using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WSI.Goat.InternshipsContracts.Web.ViewModels;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Coordinator.ViewModels
{
    public class CoordinatorViewStudentContractViewModel
    {
        public Guid Id { get; set; }
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

        [Display(Name = "Getekent")]
        public bool MentorIsSigned { get; set; }
       
        [Display(Name = "Getekent")]
        public bool StudentIsSigned { get; set; }

        [Display(Name = "Gekeurt")]
        public bool SupervisourIsSigned { get; set; }

        [Display(Name = "Gekeurt")]
        public bool CoordinatorValidation { get; set; }

        [Display(Name = "Risicoanalyse Aanwezig")]
        public bool HasRiskAnalysis { get; set; }

        [Display(Name = "Organisatie Email")]
        public string OrganisationEmail { get; set; }

        public string CoordinatorRemarks { get; set; }

        public IEnumerable<ContractArticleViewModel>? Articles { get; set; }
    }
}

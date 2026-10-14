using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WSI.Goat.InternshipContracts.Core.Entities;
using WSI.Goat.InternshipContracts.Web.ViewModels;

namespace WSI.Goat.InternshipsContracts.Web.ViewModels;

public class StudentShowContractDetailsViewModel : BaseViewModel
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

    public string? Remarks { get; set; }

    [Display(Name = "Medische Keuring Vereist")]
    public bool NeedsMedical { get; set; }
    public IFormFile? StudentSignature { get; set; } // PATH IMAGE (SIGNATURE)
    public IFormFile? MentorSignature { get; set; } // PATH IMAGE (SIGNATURE)
    public string? SupervisourSignature { get; set; } // PATH IMAGE (SIGNATURE)

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

    [Display(Name = "Datum Medisch onderzoek")]
    public DateTime? MedicalResearchDate { get; set; }

    [Display(Name = "Locatie Medische onderzoek")]
    public string? MedicalResearchLocation { get; set; }

    public string? RiskAnalysis { get; set; }
    public Uri? PageUrl { get; set; }

    public ICollection<ContractArticleViewModel>? Articles { get; set; }
}
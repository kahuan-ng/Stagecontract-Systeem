using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WSI.Goat.InternshipsContracts.Web.ViewModels;

public class ContractPDFViewModel
{
    [Required(ErrorMessage = "Contract titel is verplicht")]
    [Display(Name = "Contract titel")]
    public string ContractTitle { get; set; }

    [Required(ErrorMessage = "Selecteer een opleiding")]
    [Display(Name = "Opleiding")]
    public string Department { get; set; }

    [Display(Name = "Type Stage")]
    public string InternshipType { get; set; }

    [EmailAddress(ErrorMessage = "Ongeldig e-mailadres")]
    [Display(Name = "E-mailadres Stageplaats (contact)")]
    public string InternshipEmail { get; set; }

    [Required(ErrorMessage = "Selecteer een stagementor")]
    [Display(Name = "Stagementor")]
    public string Mentor { get; set; }

    [Required(ErrorMessage = "Straatnaam is verplicht")]
    [Display(Name = "Straat stage adres")]
    public string InternshipStreet { get; set; }

    [Required(ErrorMessage = "Huisnummer is verplicht")]
    [Display(Name = "Huisnummer")]
    public int InternshipStreetNumber { get; set; }

    [Required(ErrorMessage = "Postcode is verplicht")]
    [Display(Name = "Postcode")]
    public string InternshipPostalCode { get; set; }

    [Required(ErrorMessage = "Gemeente is verplicht")]
    [Display(Name = "Gemeente")]
    public string InternshipMunicipality { get; set; }

    [Display(Name = "Is een medisch attest/onderzoek vereist voor deze stage?")]
    public bool NeedsMedical { get; set; }

    [Required(ErrorMessage = "Startdatum is verplicht")]
    [DataType(DataType.Date)]
    [Display(Name = "Start datum stage")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "Einddatum is verplicht")]
    [DataType(DataType.Date)]
    [Display(Name = "Eind datum stage")]
    public DateTime EndDate { get; set; }

    public string StageOrganizationName { get; set; }
    public string StudentName { get; set; }
    public string StudentEmail { get; set; }
    public string StudentTelephone { get; set; }




    public IEnumerable<ContractArticleViewModel>? Articles { get; set; }

}
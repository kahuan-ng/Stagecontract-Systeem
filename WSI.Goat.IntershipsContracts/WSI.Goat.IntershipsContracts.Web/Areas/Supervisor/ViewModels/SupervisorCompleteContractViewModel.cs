using System.ComponentModel.DataAnnotations;

namespace WSI.Goat.InternshipContracts.Web.Areas.Supervisor.ViewModels;

public class SupervisorCompleteContractViewModel
{
    public Guid ContractId { get; set; } 
    public string? ContractTitle { get; set; }
    public string? StudentName { get; set; }
    public string? StudentEmail { get; set; }
    public string? StudentPhone { get; set; }
    public string? DepartmentName { get; set; }
    public string? InternshipTypeName { get; set; }
    public string? InternshipStreet { get; set; }
    public int InternshipStreetNumber { get; set; }
    public string? InternshipPostalCode { get; set; }
    public string? InternshipMunicipality { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [Display(Name = "Medische onderzoek nodig.")]
    public bool NeedsMedical { get; set; }
    public bool HasRiskAnalysis { get; set; }
    public IFormFile? RiskAnalysis { get; set; }
    public string? InternshipCoordinatorRemarks { get; set; }
    public string? MedicalResearchLocation { get; set; }
    
    [Display(Name = "Handtekening (Afbeelding)")]
    public IFormFile? SupervisourSignature { get; set; }
    [Display(Name = "Getekent")]
    public bool MentorIsSigned { get; set; }
    [Display(Name = "Getekent")]
    public bool StudentIsSigned { get; set; }
    [Display(Name = "Getekent")]
    public bool SupervisourIsSigned { get; set; }
    [Display(Name = "Gekeurt")]
    public bool CoordinatorValidation { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace WSI.Goat.InternshipContracts.Web.Areas.Supervisor.ViewModels;

public class SupervisorShowContractDetailsViewModel
{
    public Guid ContractId { get; set; } 

    public string InternshipContractName { get; set; }
    public string StudentName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool HasRiskAnalysis { get; set; }
    public string InternshipMunicipality { get; set; }
    public IFormFile? SupervisourSignature { get; set; } // PATH IMAGE (SIGNATURE)

    [Display(Name = "Getekent")]
    public bool MentorIsSigned { get; set; }
    [Display(Name = "Getekent")]
    public bool StudentIsSigned { get; set; }
    [Display(Name = "Getekent")]
    public bool SupervisourIsSigned { get; set; }

}
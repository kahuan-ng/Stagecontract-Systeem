namespace WSI.Goat.InternshipsContracts.Web.ViewModels;

public class InternshipContractDetailViewModel
{
    public string InternshipContractId { get; set; }
    public string InternshipContractName { get; set; }
    
    public string InternshipStreet { get; set; }
    public int InternshipStreetNumber { get; set; }
    public string InternshipPostalCode { get; set; }
    public string InternshipMunicipality { get; set; }
        
    public string InternshipOrganization { get; set; }
        
    public string Department { get; set; }
    public string InternshipType { get; set; }
    public string Student { get; set; }
    public string InternshipMentor { get; set; }
    public string InternshipAttendant { get; set; }
        
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
        
    public string? InternshipCoordinatorRemarks { get; set; }
    public string? RiskAnalysis { get; set; }
    public string? StudentSignature { get; set; } // PATH IMAGE (SIGNATURE)
    public string? MentorSignature { get; set; } // PATH IMAGE (SIGNATURE)
    public string? SupervisourSignature { get; set; } // PATH IMAGE (SIGNATURE)

    public bool MentorIsSigned { get; set; }
    public bool StudentIsSigned { get; set; }
    public bool SupervisourIsSigned { get; set; }

    public bool NeedsMedical { get; set; }
    public bool HasRiskAnalysis { get; set; }
    public IEnumerable<ContractArticleViewModel>? Articles { get; set; }
}
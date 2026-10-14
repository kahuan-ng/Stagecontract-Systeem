namespace WSI.Goat.InternshipsContracts.Web.ViewModels
{
    public class InternshipContractViewModel
    {
        public Guid Id { get; set; }
        public Guid InternshipOrganizationId { get; set; }
        public Guid InternshipTypeId { get; set; }
        public string InternshipStreet { get; set; }
        public int InternshipStreetNumber { get; set; }
        public int InternshipPostalCode { get; set; }
        public string InternshipMunicipality { get; set; }
        public Guid StudentId { get; set; }
        public Guid InternshipMentorId { get; set; }
        public Guid InternshipAttendantId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate2 { get; set; }
        public DateTime EndDate2 { get; set; }
        public string InternshipCoordinatorRemarks { get; set; }
        public IFormFile RiskAnalysis { get; set; }
        public bool NeedsMedical { get; set; }
        public bool IsSigned { get; set; }
        public bool HasRiskAnalysis { get; set; }
        public string Signature { get; set; } //!!! RESEARCH NEEDED ON SVG IMAGE BUILDING !!!!
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Deleted { get; set; }
        public IEnumerable<ContractArticleViewModel>? Articles { get; set; }
    }
}

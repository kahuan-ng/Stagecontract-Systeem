namespace WSI.Goat.InternshipsContracts.Web.Areas.Coordinator.ViewModels
{
    public class CoordinatorStudentInfoViewModel
    {
        public Guid? StudentId { get; set; }
        public Guid ContractId { get; set; }
        public string UserName { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentTelephoneNumber { get; set; }
        public string? StudentSignature { get; set; }
        public string StudentRepresentedOrganisation { get; set; }

        public string OrganizationEmail { get; set; }
        public bool IsSigned { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

using WSI.Goat.InternshipContracts.Web.ViewModels;
using WSI.Goat.InternshipsContracts.Web.ViewModels;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Mentor.ViewModels
{
    public class MentorStudentsInfoViewModel
    {
        public Guid ContractId { get; set; }
        public Guid? StudentId { get; set; }
        public string UserName { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentTelephoneNumber { get; set; }
        public string? StudentSignature { get; set; }
        public string StudentRepresentedOrganisation { get; set; }
        public bool IsSigned { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IEnumerable<ContractArticleViewModel>? Articles { get; set; }
    }
}
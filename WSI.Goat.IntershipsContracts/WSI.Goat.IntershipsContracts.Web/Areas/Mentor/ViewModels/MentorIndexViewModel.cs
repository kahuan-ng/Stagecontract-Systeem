namespace WSI.Goat.InternshipsContracts.Web.Areas.Mentor.ViewModels
{
    public class MentorIndexViewModel
    {
        public string MentorName { get; set; }
        public ICollection<MentorStudentsInfoViewModel> Contracts { get; set; }
    }
}

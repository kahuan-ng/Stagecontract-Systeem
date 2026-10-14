namespace WSI.Goat.InternshipsContracts.Web.Areas.Coordinator.ViewModels
{
    public class CoordinatorIndexViewModel
    {
        public string CoordinatorName { get; set; }
        public ICollection<CoordinatorStudentInfoViewModel> Contracts { get; set; }
    }
}

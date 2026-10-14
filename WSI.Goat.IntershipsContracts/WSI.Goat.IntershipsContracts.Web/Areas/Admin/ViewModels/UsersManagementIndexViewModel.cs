namespace WSI.Goat.InternshipsContracts.Web.Areas.Admin.ViewModels
{
    public class UsersManagementIndexViewModel
    {
        public string? AdminName { get; set; }
        public IEnumerable<UserOverviewViewModel>? Users { get; set; }
    }
}

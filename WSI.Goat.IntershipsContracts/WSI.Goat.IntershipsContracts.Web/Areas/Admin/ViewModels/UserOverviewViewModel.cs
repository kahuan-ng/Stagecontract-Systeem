namespace WSI.Goat.InternshipsContracts.Web.Areas.Admin.ViewModels
{
    public class UserOverviewViewModel
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Function { get; set; }
        public string? Password { get; set; }
    }
}

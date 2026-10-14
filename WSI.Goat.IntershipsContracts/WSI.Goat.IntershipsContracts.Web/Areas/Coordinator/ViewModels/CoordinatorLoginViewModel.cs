using System.ComponentModel.DataAnnotations;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Coordinator.ViewModels
{
    public class CoordinatorLoginViewModel
    {
        [Required(ErrorMessage = "Email is verplicht")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is verplicht")]
        [MinLength(3, ErrorMessage = "Het wachtwoord moet minstens 3 tekens lang zijn.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}

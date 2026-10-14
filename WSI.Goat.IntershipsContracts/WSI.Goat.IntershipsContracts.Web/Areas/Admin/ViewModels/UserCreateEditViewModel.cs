using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Admin.ViewModels
{
    public class UserCreateEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Voornaam is verplicht")]
        [Display(Name = "Voornaam")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht")]
        [Display(Name = "Achternaam")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "E-mailadres is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres")]
        [Display(Name = "E-mailadres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [Display(Name = "Wachtwoord")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Functie is verplicht")]
        [Display(Name = "Functie")]
        public string? Function { get; set; }

        [Required(ErrorMessage = "Telefoonnummer is verplicht")]
        [Display(Name = "Telefoonnummer")]
        public string? TelephoneNumber { get; set; }

        [Required(ErrorMessage = "Selecteer een rol")]
        [Display(Name = "Rol")]
        public Guid? SelectedRoleId { get; set; }

        public IEnumerable<SelectListItem>? Roles { get; set; }
    }
}

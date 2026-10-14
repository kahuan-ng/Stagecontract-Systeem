using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Admin.ViewModels
{
    public class InternshipTypeCreateEditViewModel
    {
        public Guid Id { get; set; }
       
        [Required(ErrorMessage = "Naam van het type stage is verplicht")]
        [Display(Name = "Type Stage")]
        public string? BaseName { get; set; }
    }
}
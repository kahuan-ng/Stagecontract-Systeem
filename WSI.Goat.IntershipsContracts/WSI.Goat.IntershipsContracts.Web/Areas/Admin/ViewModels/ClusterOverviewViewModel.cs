using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Admin.ViewModels
{
    public class ClusterOverviewViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Clusternaam is verplicht")]
        [Display(Name = "Clusternaam")]
        public string? BaseName { get; set; }
        public IEnumerable<SelectListItem>? Supervisors { get; set; }
    }
}


using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Admin.ViewModels
{
    public class DepartmentCreateEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Department is verplicht")]
        [Display(Name = "Department")]
        public string BaseName { get; set; }

        [Required(ErrorMessage = "Locatie is verplicht")]
        [Display(Name = "Locatie")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Selecteer een cluster")]
        [Display(Name = "Cluster")]
        public Guid? ClusterId { get; set; }

        [Required(ErrorMessage = "Selecteer een stagecoördinator")]
        [Display(Name = "Stagecoördinator")]
        public Guid? StageCoordinatorId { get; set; }

        public IEnumerable<SelectListItem>? Clusters { get; set; }
        public IEnumerable<SelectListItem>? Departments { get; set; }

        public IEnumerable<SelectListItem>? StageCoordinators { get; set; }
    }
}
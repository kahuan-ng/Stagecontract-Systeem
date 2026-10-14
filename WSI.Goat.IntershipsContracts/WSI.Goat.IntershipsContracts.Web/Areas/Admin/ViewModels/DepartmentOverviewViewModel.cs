using System.ComponentModel.DataAnnotations;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Admin.ViewModels
{

    public class DepartmentOverviewViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Department is verplicht")]
        [Display(Name = "Department")]
        public string BaseName { get; set; } 

        [Required(ErrorMessage = "Locatie is verplicht")]
        [Display(Name = "Locatie")]
        public string? Location { get; set; }

        [Display(Name = "Cluster")]
        public string? ClusterName { get; set; }

        [Display(Name = "Stagecoördinator")]
        public string? StageCoordinatorName { get; set; }
    }
}


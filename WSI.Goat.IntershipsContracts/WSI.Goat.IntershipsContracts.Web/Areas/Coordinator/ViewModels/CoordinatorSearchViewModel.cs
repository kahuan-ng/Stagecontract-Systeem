using System.ComponentModel.DataAnnotations;

namespace WSI.Goat.InternshipsContracts.Web.Areas.Coordinator.ViewModels;

public class CoordinatorSearchViewModel
{
    public string StudentName { get; set; }
    public IEnumerable<CoordinatorStudentInfoViewModel> Results { get; set; }    
}
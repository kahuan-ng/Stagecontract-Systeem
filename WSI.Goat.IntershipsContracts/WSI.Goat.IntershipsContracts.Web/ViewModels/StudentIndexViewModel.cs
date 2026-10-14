using WSI.Goat.InternshipContracts.Core.Entities;

namespace WSI.Goat.InternshipsContracts.Web.ViewModels;

public class StudentIndexViewModel
{
    public string StudentName { get; set; }
    public IEnumerable<InternshipContractDetailViewModel> Contracts { get; set; }
}
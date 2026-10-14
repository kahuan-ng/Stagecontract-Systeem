namespace WSI.Goat.InternshipContracts.Web.Areas.Supervisor.ViewModels;

public class SupervisorIndexViewModel
{
    public string SupervisorName { get; set; }   
    
    public IEnumerable<SupervisorShowContractDetailsViewModel> Contracts { get; set; }
}
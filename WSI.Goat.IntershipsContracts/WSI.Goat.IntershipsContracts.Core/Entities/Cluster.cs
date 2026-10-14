using WSI.Goat.IntershipsContracts.Core.Entities;

namespace WSI.Goat.InternshipContracts.Core.Entities;

public class Cluster : BaseEntity
{
    public Guid SupervisorId { get; set; }
    public ApplicationUser Supervisor { get; set; } // opleiding manager
    
    public ICollection<Department> Departments { get; set; }
}
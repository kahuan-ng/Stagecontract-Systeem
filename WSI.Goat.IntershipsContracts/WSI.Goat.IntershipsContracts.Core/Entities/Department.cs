using WSI.Goat.InternshipContracts.Core.Entities;

namespace WSI.Goat.IntershipsContracts.Core.Entities;

public class Department : BaseEntity
{
    public string Location { get; set; }
    
    public Guid? ClusterId { get; set; }
    public Cluster Cluster { get; set; }
    
    public Guid? StageCoordinatorId { get; set; }
    public ApplicationUser StageCoordinator { get; set; }
    
    public ICollection<InternshipContract> InternshipContracts { get; set; } = new List<InternshipContract>();
}
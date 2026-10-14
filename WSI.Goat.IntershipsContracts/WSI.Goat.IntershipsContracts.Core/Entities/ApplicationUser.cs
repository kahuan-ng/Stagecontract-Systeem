using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSI.Goat.IntershipsContracts.Core.Entities;
namespace WSI.Goat.InternshipContracts.Core.Entities
{
    public class ApplicationUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }   
        public string Email { get; set; }
        public string Password { get; set; }
        public string TelephoneNumber { get; set; }
        public string Function { get; set; }
        public string? Signature { get; set; }
        public Guid ApplicationRoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
        public ICollection<StageOrganization> RepresentedOrganizations { get; set; }
    }
}
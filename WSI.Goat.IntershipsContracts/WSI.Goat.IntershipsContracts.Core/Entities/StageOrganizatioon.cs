using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSI.Goat.InternshipContracts.Core.Entities
{
    public class StageOrganization : BaseEntity 
    {
        public string OrganizationNumber { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string PostalCode { get; set; }
        public string Municipality { get; set; }
        public string? Signature { get; set; }
        
        public Guid? RepresentativeId { get; set; }
        public ApplicationUser Representative { get; set; }
        
        public ICollection<InternshipContract>? InternshipContracts { get; set; }
    }
}

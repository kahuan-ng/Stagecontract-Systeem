using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSI.Goat.InternshipContracts.Core.Entities;
using WSI.Goat.IntershipsContracts.Core.Entities;

namespace WSI.Goat.IntershipsContracts.Core.Entities
{
    public class ApplicationRole : BaseEntity
    {
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSI.Goat.InternshipContracts.Core.Entities
{
    public class InternshipType : BaseEntity
    {
        public ICollection<InternshipContract>? InternshipContracts { get; set; }
    }
}

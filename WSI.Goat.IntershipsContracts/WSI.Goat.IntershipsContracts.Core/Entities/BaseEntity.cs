using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSI.Goat.InternshipContracts.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public string BaseName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
    }
}

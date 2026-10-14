using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSI.Goat.InternshipContracts.Core.Entities
{
    public class Article
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public ICollection<InternshipContract>? InternshipContracts {  get; set; }
    }
}

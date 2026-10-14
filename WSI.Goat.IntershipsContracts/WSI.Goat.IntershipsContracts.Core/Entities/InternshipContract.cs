using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSI.Goat.IntershipsContracts.Core.Entities;

namespace WSI.Goat.InternshipContracts.Core.Entities
{
    public class InternshipContract : BaseEntity
    {
        public string InternshipStreet { get; set; }
        public int InternshipStreetNumber { get; set; }
        public string InternshipPostalCode { get; set; }
        public string InternshipMunicipality { get; set; }
        
        public Guid? InternshipOrganizationId { get; set; }
        public StageOrganization InternshipOrganization { get; set; }
        
        public Guid? DepartmentId { get; set; }
        public Department Department { get; set; }
        
        public Guid? InternshipTypeId { get; set; }
        public InternshipType InternshipType { get; set; }
        
        public Guid? StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        
        public Guid? InternshipMentorId { get; set; }
        public ApplicationUser? InternshipMentor { get; set; }
        public Guid? InternshipAttendantId { get; set; }
        public ApplicationUser? InternshipAttendant { get; set; }
        public string? MedicalResearchLocation { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? StartDate2 { get; set; }
        public DateTime? EndDate2 { get; set; }
        public string? InternshipCoordinatorRemarks { get; set; }
        public string? RiskAnalysis { get; set; } // PATH FILE (PDF)
        public string? StudentSignature { get; set; } // PATH IMAGE (SIGNATURE)
        public string? MentorSignature { get; set; } // PATH IMAGE (SIGNATURE)
        public string? SupervisourSignature { get; set; } // PATH IMAGE (SIGNATURE)
        public bool MentorIsSigned { get; set; }
        public bool StudentIsSigned { get; set; }
        public bool SupervisourIsSigned { get; set; }
        public bool CoordinatorValidation { get; set; }
        public bool HasRiskAnalysis { get; set; }
        public bool NeedsMedical { get; set; }

        public ICollection<Article>? Articles { get; set; }
    }
}
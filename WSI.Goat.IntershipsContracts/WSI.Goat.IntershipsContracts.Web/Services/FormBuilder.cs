using Microsoft.AspNetCore.Mvc.Rendering;
using WSI.Goat.InternshipContracts.Core.Entities;
using WSI.Goat.InternshipContracts.Web.Data;
using WSI.Goat.InternshipsContracts.Web.Services.Interfaces;
using WSI.Goat.InternshipsContracts.Web.ViewModels;

namespace WSI.Goat.InternshipsContracts.Web.Services
{
    public class FormBuilder : IFormBuilder
    {
        private readonly InternshipContractsDbContext _internshipContractsDbContext;

        public FormBuilder(InternshipContractsDbContext internshipContractsDbContext)
        {
            _internshipContractsDbContext = internshipContractsDbContext;
        }
        
        public List<SelectListItem> GetDepartmentsList()
        {
            return _internshipContractsDbContext.Departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.BaseName
            }).ToList();
        }
        
        public List<SelectListItem> GetInternshipTypesList()
        {
            return _internshipContractsDbContext.InternshipTypes.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.BaseName
            }).ToList();
        }
        
        public List<SelectListItem> GetMentorsList()
        {
            return _internshipContractsDbContext.ApplicationUsers
                .Where(au => au.ApplicationRoleId == new Guid("00000000-0000-0000-0000-000000000002"))
                .Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.FirstName + " " + d.LastName
            }).ToList();
        }

        public List<ContractArticleViewModel> GetArticlesList()
        {
            return _internshipContractsDbContext.Articles.Select(a => new ContractArticleViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
            }).ToList();
        }

    }
}

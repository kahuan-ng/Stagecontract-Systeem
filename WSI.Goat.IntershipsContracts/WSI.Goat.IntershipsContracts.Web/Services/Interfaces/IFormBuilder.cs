using Microsoft.AspNetCore.Mvc.Rendering;
using WSI.Goat.InternshipContracts.Core.Entities;
using WSI.Goat.InternshipsContracts.Web.ViewModels;

namespace WSI.Goat.InternshipsContracts.Web.Services.Interfaces
{
    public interface IFormBuilder
    {
        List<SelectListItem> GetDepartmentsList();
        List<SelectListItem> GetInternshipTypesList();
        List<SelectListItem> GetMentorsList();
        List<ContractArticleViewModel> GetArticlesList();
    }
}

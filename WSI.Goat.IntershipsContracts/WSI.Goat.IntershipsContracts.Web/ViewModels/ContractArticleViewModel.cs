using System.ComponentModel.DataAnnotations;

namespace WSI.Goat.InternshipsContracts.Web.ViewModels
{
    public class ContractArticleViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Een artikel moet een titel hebben.")]
        [StringLength(100, ErrorMessage = "De titel mag maximaal 100 tekens lang zijn.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "De artikelomschrijving is verplicht.")]
        public string Description {  get; set; }
    }
}

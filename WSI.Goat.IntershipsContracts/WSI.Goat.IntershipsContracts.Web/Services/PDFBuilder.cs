using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using WSI.Goat.InternshipsContracts.Web.ViewModels;

namespace WSI.Goat.InternshipsContracts.Web.Services
{
    public class PDFBuilder : IDocument
    {
        private readonly ContractPDFViewModel _contractPDFViewModel;
        
        public PDFBuilder(ContractPDFViewModel contractPDFViewModel)
        {
            _contractPDFViewModel = contractPDFViewModel;
        }
        
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11).FontColor(Colors.Black));

                page.Header().Text(_contractPDFViewModel.ContractTitle).SemiBold().FontSize(22).FontColor(Colors.Blue.Darken3);

                page.Content().PaddingVertical(1, Unit.Centimetre).Column(column =>
                {
                    column.Spacing(10);
                    
                    column.Item().Text($"Startdatum: {_contractPDFViewModel.StartDate}");
                    column.Item().Text($"Einddatum: {_contractPDFViewModel.EndDate}");
                    
                    column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().Text("Student:").Bold();

                    column.Item().Text($"{_contractPDFViewModel.StudentName}");
                    column.Item().Text($"{_contractPDFViewModel.StudentTelephone}");
                    column.Item().Text($"{_contractPDFViewModel.StudentEmail}");

                    column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    column.Item().Text("Stageplaats:").Bold();
                    
                    column.Item().Text($"{_contractPDFViewModel.StageOrganizationName}");
                    column.Item().Text($"{_contractPDFViewModel.InternshipStreet} {_contractPDFViewModel.InternshipStreetNumber}");
                    column.Item().Text($"{_contractPDFViewModel.InternshipPostalCode} {_contractPDFViewModel.InternshipMunicipality}");


                    column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                    if (_contractPDFViewModel.Articles != null)
                    {
                        column.Item().Text("Contractvoorwaarden:").Bold().FontSize(14);

                        foreach (var article in _contractPDFViewModel.Articles)
                        {
                            column.Item().PaddingTop(5).Text(article.Title).Bold();

                            column.Item().Text(article.Description);
                        }
                    }
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
        }
    }
}

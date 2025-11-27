//using LabPort.Backend.Application.Interfaces;
//using LabPort.Backend.Contracts.DTOs.Reports;
//using QuestPDF.Fluent;
//using QuestPDF.Helpers;
//using QuestPDF.Infrastructure;
//using System.ComponentModel;
//using System.Globalization;
//using System.Reflection.Metadata;

//// Fix: Add explicit using alias for QuestPDF.Infrastructure.IContainer
//using QPdfContainer = QuestPDF.Infrastructure.IContainer;

//namespace LabPort.Backend.Infrastructure.Integration.Reporting
//{
//    public class PdfReportService : IPdfReportService
//    {
//        public PdfReportService()
//        {
//            // На всякий случай, если нужно явно указать тип лицензии
//            QuestPDF.Settings.License = LicenseType.Community;
//        }

//        public byte[] GenerateSampleReportPdf(SampleReportModel model)
//        {
//            var document = QuestPDF.Fluent.Document.Create(container =>
//            {
//                container.Page(page =>
//                {
//                    page.Size(PageSizes.A4);
//                    page.Margin(30);
//                    page.PageColor(Colors.White);
//                    page.DefaultTextStyle(x => x.FontSize(11));

//                    page.Header().Element(c => ComposeHeader(c, model));
//                    page.Content().Element(c => ComposeContent(c, model));
//                    page.Footer().AlignCenter().Text(txt =>
//                    {
//                        txt.Span("LabPort • ");
//                        txt.Span(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture))
//                           .FontSize(9).FontColor(Colors.Grey.Medium);
//                    });
//                });
//            });

//            return document.GeneratePdf();
//        }

//        private void ComposeHeader(QPdfContainer container, SampleReportModel model)
//        {
//            container.Row(row =>
//            {
//                row.RelativeItem().Column(col =>
//                {
//                    col.Item().Text("Звіт по зразку")
//                        .FontSize(20)
//                        .SemiBold();

//                    col.Item().Text($"ID зразка: {model.SampleId}")
//                        .FontSize(10)
//                        .FontColor(Colors.Grey.Darken2);

//                    col.Item().Text($"Назва: {model.SampleName}")
//                        .FontSize(11);

//                    col.Item().Text($"Дата відбору: {model.CollectedAt:yyyy-MM-dd HH:mm}")
//                        .FontSize(10);

//                    col.Item().Text($"Користувач: {model.UserName} ({model.UserEmail})")
//                        .FontSize(10);
//                });

//                // Можно добавить какой-нибудь “логотип”
//                row.ConstantItem(80).AlignRight().Column(col =>
//                {
//                    col.Item().AlignRight().Text("LabPort")
//                        .FontSize(18)
//                        .SemiBold()
//                        .FontColor(Colors.Blue.Medium);
//                });
//            });
//        }

//        private void ComposeContent(QPdfContainer container, SampleReportModel model)
//        {
//            container.PaddingVertical(10).Column(col =>
//            {
//                // Секция: джерело
//                col.Item().Element(c => ComposeSourceSection(c, model));

//                // Секция: контейнер / умови
//                col.Item().Element(c => ComposeContainerSection(c, model));

//                // Секция: результати тестів
//                col.Item().Element(c => ComposeTestsSection(c, model));

//                // Секция: висновок
//                col.Item().Element(c => ComposeConclusionSection(c, model));
//            });
//        }

//        private void ComposeSourceSection(QPdfContainer container, SampleReportModel model)
//        {
//            container.PaddingBottom(10).Column(col =>
//            {
//                col.Item().Text("1. Інформація про зразок")
//                    .FontSize(14)
//                    .SemiBold()
//                    .FontColor(Colors.Blue.Darken2);

//                col.Item().PaddingTop(4).Table(table =>
//                {
//                    table.ColumnsDefinition(columns =>
//                    {
//                        columns.ConstantColumn(120);
//                        columns.RelativeColumn();
//                    });

//                    table.Cell().Element(LabelCell).Text("Назва зразка:");
//                    table.Cell().Element(ValueCell).Text(model.SampleName);

//                    table.Cell().Element(LabelCell).Text("Дата відбору:");
//                    table.Cell().Element(ValueCell).Text(
//                        model.CollectedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));

//                    table.Cell().Element(LabelCell).Text("Джерело:");
//                    table.Cell().Element(ValueCell).Text(
//                        string.IsNullOrEmpty(model.SourceName) ? "—" : model.SourceName);

//                    table.Cell().Element(LabelCell).Text("Локація джерела:");
//                    table.Cell().Element(ValueCell).Text(
//                        string.IsNullOrEmpty(model.SourceLocation) ? "—" : model.SourceLocation);
//                });
//            });

//            static QPdfContainer LabelCell(QPdfContainer container) =>
//                container.PaddingVertical(2)
//                         .PaddingRight(5)
//                         .AlignLeft()
//                         .TextStyle(x => x.SemiBold());

//            static QPdfContainer ValueCell(QPdfContainer container) =>
//                container.PaddingVertical(2);
//        }

//        private void ComposeContainerSection(QPdfContainer container, SampleReportModel model)
//        {
//            container.PaddingVertical(10).Column(col =>
//            {
//                col.Item().Text("2. Умови зберігання (контейнер)")
//                    .FontSize(14)
//                    .SemiBold()
//                    .FontColor(Colors.Blue.Darken2);

//                col.Item().PaddingTop(4).Table(table =>
//                {
//                    table.ColumnsDefinition(columns =>
//                    {
//                        columns.ConstantColumn(120);
//                        columns.RelativeColumn();
//                    });

//                    table.Cell().Element(LabelCell).Text("Контейнер:");
//                    table.Cell().Element(ValueCell).Text(model.ContainerLabel);

//                    table.Cell().Element(LabelCell).Text("Температура, °C:");
//                    table.Cell().Element(ValueCell).Text(
//                        $"{model.TemperatureMin:0.##} … {model.TemperatureMax:0.##}");

//                    table.Cell().Element(LabelCell).Text("Вологість, %:");
//                    table.Cell().Element(ValueCell).Text(
//                        $"{model.HumidityMin:0.##} … {model.HumidityMax:0.##}");
//                });
//            });

//            static QPdfContainer LabelCell(QPdfContainer container) =>
//                container.PaddingVertical(2)
//                         .PaddingRight(5)
//                         .AlignLeft()
//                         .TextStyle(x => x.SemiBold());

//            static QPdfContainer ValueCell(QPdfContainer container) =>
//                container.PaddingVertical(2);
//        }

//        private void ComposeTestsSection(QPdfContainer container, SampleReportModel model)
//        {
//            container.PaddingVertical(10).Column(col =>
//            {
//                col.Item().Text("3. Результати тестів")
//                    .FontSize(14)
//                    .SemiBold()
//                    .FontColor(Colors.Blue.Darken2);

//                if (!model.Tests.Any())
//                {
//                    col.Item().PaddingTop(4).Text("Для цього зразка ще не виконано жодного тесту.")
//                        .FontColor(Colors.Grey.Darken2);
//                    return;
//                }

//                col.Item().PaddingTop(4).Table(table =>
//                {
//                    table.ColumnsDefinition(columns =>
//                    {
//                        columns.ConstantColumn(80);   // Дата
//                        columns.RelativeColumn();     // Тип тесту
//                        columns.ConstantColumn(80);   // Назва результату
//                        columns.ConstantColumn(70);   // Значення
//                        columns.ConstantColumn(60);   // Статус
//                        columns.RelativeColumn();     // Коментар
//                    });

//                    // Header
//                    table.Header(header =>
//                    {
//                        header.Cell().Element(HeaderCell).Text("Дата");
//                        header.Cell().Element(HeaderCell).Text("Тип тесту");
//                        header.Cell().Element(HeaderCell).Text("Показник");
//                        header.Cell().Element(HeaderCell).Text("Значення");
//                        header.Cell().Element(HeaderCell).Text("Статус");
//                        header.Cell().Element(HeaderCell).Text("Коментар");
//                    });

//                    // Rows
//                    foreach (var t in model.Tests)
//                    {
//                        var statusColor = t.ResultStatus switch
//                        {
//                            "Unexpected" => Colors.Red.Medium,
//                            "Pending" => Colors.Orange.Medium,
//                            "Expected" => Colors.Green.Darken1,
//                            _ => Colors.Grey.Darken2
//                        };

//                        table.Cell().Element(Cell).Text(t.TestedAt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
//                        table.Cell().Element(Cell).Text(t.TestTypeName);
//                        table.Cell().Element(Cell).Text(t.ResultName ?? "—");
//                        table.Cell().Element(Cell).Text(
//                            t.ValueNumeric.HasValue
//                                ? $"{t.ValueNumeric:0.###} {t.Unit}"
//                                : (string.IsNullOrEmpty(t.ValueText) ? "—" : $"{t.ValueText} {t.Unit}"));

//                        table.Cell().Element(c => CellStatus(c, statusColor)).Text(t.ResultStatus);
//                        table.Cell().Element(Cell).Text(t.Note ?? "");
//                    }
//                });
//            });

//            static QPdfContainer HeaderCell(QPdfContainer container) =>
//                container.PaddingVertical(2)
//                         .PaddingHorizontal(3)
//                         .Background(Colors.Grey.Lighten3)
//                         .BorderBottom(1)
//                         .BorderColor(Colors.Grey.Medium)
//                         .TextStyle(x => x.SemiBold().FontSize(10));

//            static QPdfContainer Cell(QPdfContainer container) =>
//                container.PaddingVertical(2)
//                         .PaddingHorizontal(3)
//                         .BorderBottom(0.5f)
//                         .BorderColor(Colors.Grey.Lighten2)
//                         .TextStyle(x => x.FontSize(9));

//            static QPdfContainer CellStatus(QPdfContainer container, string color) =>
//                container.PaddingVertical(2)
//                         .PaddingHorizontal(3)
//                         .BorderBottom(0.5f)
//                         .BorderColor(Colors.Grey.Lighten2)
//                         .Background(color + "20")  // чуть прозрачный фон
//                         .TextStyle(x => x.FontSize(9).SemiBold());
//        }

//        private void ComposeConclusionSection(QPdfContainer container, SampleReportModel model)
//        {
//            container.PaddingVertical(10).Column(col =>
//            {
//                col.Item().Text("4. Висновок")
//                    .FontSize(14)
//                    .SemiBold()
//                    .FontColor(Colors.Blue.Darken2);

//                col.Item().PaddingTop(4).Text(model.SummaryConclusion)
//                    .FontSize(11);
//            });
//        }
//    }
//}

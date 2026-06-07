using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.Reports;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Globalization;

using QPdfContainer = QuestPDF.Infrastructure.IContainer;

namespace LabPort.Backend.Infrastructure.Integration.Reporting
{
    public class PdfReportService : IPdfReportService
    {
        public PdfReportService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GenerateSampleReportPdf(SampleReportModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Element(c => ComposeHeader(c, model));
                    page.Content().Element(c => ComposeContent(c, model));
                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("LabPort • ");
                        txt.Span(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture))
                           .FontSize(9).FontColor(Colors.Grey.Medium);
                    });
                });
            });

            return document.GeneratePdf();
        }

        private void ComposeHeader(QPdfContainer container, SampleReportModel model)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("Звіт по зразку")
                        .FontSize(20)
                        .SemiBold();

                    col.Item().Text($"ID зразка: {model.SampleId}")
                        .FontSize(10)
                        .FontColor(Colors.Grey.Darken2);

                    col.Item().Text($"Назва: {model.SampleName}")
                        .FontSize(11);

                    col.Item().Text($"Дата відбору: {model.CollectedAt:yyyy-MM-dd HH:mm}")
                        .FontSize(10);

                    col.Item().Text($"Користувач: {model.UserName} ({model.UserEmail})")
                        .FontSize(10);
                });

                row.ConstantItem(80).AlignRight().Column(col =>
                {
                    col.Item().AlignRight().Text("LabPort")
                        .FontSize(18)
                        .SemiBold()
                        .FontColor(Colors.Blue.Medium);
                });
            });
        }

        private void ComposeContent(QPdfContainer container, SampleReportModel model)
        {
            container.PaddingVertical(10).Column(col =>
            {
                col.Item().Element(c => ComposeSourceSection(c, model));
                col.Item().Element(c => ComposeContainerSection(c, model));
                col.Item().Element(c => ComposeConclusionSection(c, model));
            });
        }

        private void ComposeSourceSection(QPdfContainer container, SampleReportModel model)
        {
            container.PaddingBottom(10).Column(col =>
            {
                col.Item().Text("1. Інформація про зразок")
                    .FontSize(14)
                    .SemiBold()
                    .FontColor(Colors.Blue.Darken2);

                col.Item().PaddingTop(4).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(120);
                        columns.RelativeColumn();
                    });

                    table.Cell().Element(LabelCell).Text("Назва зразка:");
                    table.Cell().Element(ValueCell).Text(model.SampleName);

                    table.Cell().Element(LabelCell).Text("Дата відбору:");
                    table.Cell().Element(ValueCell).Text(model.CollectedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));

                    table.Cell().Element(LabelCell).Text("Джерело:");
                    table.Cell().Element(ValueCell).Text(model.SourceName ?? "—");

                    table.Cell().Element(LabelCell).Text("Локація джерела:");
                    table.Cell().Element(ValueCell).Text(model.SourceLocation ?? "—");
                });
            });

            static QPdfContainer LabelCell(QPdfContainer c) =>
                c.PaddingVertical(2)
                 .PaddingRight(5)
                 .AlignLeft()
                 .DefaultTextStyle(x => x.SemiBold());

            static QPdfContainer ValueCell(QPdfContainer c) =>
                c.PaddingVertical(2);
        }

        private void ComposeContainerSection(QPdfContainer container, SampleReportModel model)
        {
            container.PaddingVertical(10).Column(col =>
            {
                col.Item().Text("2. Умови зберігання (контейнер)")
                    .FontSize(14)
                    .SemiBold()
                    .FontColor(Colors.Blue.Darken2);

                col.Item().PaddingTop(4).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(120);
                        columns.RelativeColumn();
                    });

                    table.Cell().Element(LabelCell).Text("Контейнер:");
                    table.Cell().Element(ValueCell).Text(model.ContainerLabel);

                    table.Cell().Element(LabelCell).Text("Температура, °C:");
                    table.Cell().Element(ValueCell).Text($"{model.TemperatureMin:0.##} … {model.TemperatureMax:0.##}");

                    table.Cell().Element(LabelCell).Text("Вологість, %:");
                    table.Cell().Element(ValueCell).Text($"{model.HumidityMin:0.##} … {model.HumidityMax:0.##}");
                });
            });

            static QPdfContainer LabelCell(QPdfContainer c) =>
                c.PaddingVertical(2)
                 .PaddingRight(5)
                 .AlignLeft()
                 .DefaultTextStyle(x => x.SemiBold());

            static QPdfContainer ValueCell(QPdfContainer c) =>
                c.PaddingVertical(2);
        }

        private void ComposeConclusionSection(QPdfContainer container, SampleReportModel model)
        {
            container.PaddingVertical(10).Column(col =>
            {
                col.Item().Text("3. Висновок")
                    .FontSize(14)
                    .SemiBold()
                    .FontColor(Colors.Blue.Darken2);

                col.Item().PaddingTop(4).Text(model.SummaryConclusion)
                    .FontSize(11);
            });
        }
    }
}

using System;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.PageElements.BarCoding;
using Integration.Shared.Model;
using IntegrationAPI.DTO.MedicineConsumption;
using IntegrationAPI.DTO.Prescription;
using IntegrationAPI.DTO.Shared;
using IntegrationApi.DTO.Tender;
using Path = System.IO.Path;
using ceTe.DynamicPDF.PageElements.Charting;
using ceTe.DynamicPDF.PageElements.Charting.Series;
using ceTe.DynamicPDF.PageElements.Charting.Axes;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationAPI.Adapters.PDF.Implementation
{
    public class DynamicPDFAdapter : IPDFAdapter
    {
        protected float _fontSize;
        protected Page _lastPage;
        protected float _currentY;
        protected Document _document;

        public DynamicPDFAdapter()
        {
            _document = new Document();
            _lastPage = new Page();
            _document.Pages.Add(_lastPage);
            _currentY = 0;
            _fontSize = 12;
        }

        public void AddNewPage()
        {
            Page page = new Page();
            _document.Pages.Add(page);
            _lastPage = page;
            _currentY = 0;
        }

        public void SaveDocument(string destination)
        {
            _document.Draw(destination);
        }

        public void SetFontSize(float size)
        {
            _fontSize = size;
        }

        public void WriteLine(float xMargin, float yMargin, string text)
        {
            Label label = new Label(text, xMargin, _currentY + yMargin, 512, 14, Font.HelveticaBold, _fontSize, TextAlign.Left, RgbColor.Black);
            _currentY = _currentY + yMargin;
            _lastPage.Elements.Add(label);
        }

        public string MakeMedicineConsumptionReportPdf(MedicineConsumptionReportToPdfDTO dto)
        {
            MakeTitle("Medicine consumption report");
            WriteLine(0, 20, "Time period, from " 
                             + dto.StartDate.ToShortDateString()
                             + " to " + dto.EndDate.ToShortDateString());
            WriteLine(0, 20, "Medicine with most amount spent: "
                             + dto.MedicineConsumptions[0].MedicineName
                             + ". Amount spent: " + dto.MedicineConsumptions[0].Amount);
            int lastIndex = dto.MedicineConsumptions.Count - 1;
            WriteLine(0, 20, "Medicine with least amount spent: "
                             + dto.MedicineConsumptions[lastIndex].MedicineName
                             + ". Amount spent: " + dto.MedicineConsumptions[lastIndex].Amount);
            WriteLine(0, 40, "List of medications spent:");

            var table = GenerateConsumptionTable(dto);

            if (_document.Pages.Count > 1) _currentY = table.Height;
            else _currentY = 100 + table.Height;
            WriteLine(350,30, dto.HospitalName);

            string fileName = "Report-" + dto.CreatedDate.Ticks.ToString() + ".pdf";
            SaveDocument("MedicineReports" + Path.DirectorySeparatorChar + fileName);
            return fileName;
        }

        private Table2 GenerateConsumptionTable(MedicineConsumptionReportToPdfDTO dto)
        {
            Table2 table2 = new Table2(0, _currentY + 20, 503, dto.MedicineConsumptions.Count * 31 + 42);
            table2.CellDefault.Border.Color = RgbColor.Blue;
            table2.CellSpacing = 1.0f;
            table2.Columns.Add(350);
            table2.Columns.Add(150);

            AddHeaderRowToMedicationConsumptionTable(table2);

            for (int i = 0; i < dto.MedicineConsumptions.Count; i++)
            {
                MedicationExpenditureDTO medicationExpenditure = dto.MedicineConsumptions[i];
                _currentY += 32;
                if (_currentY >= 650)
                {
                    table2 = NewPageConsumptionReport(dto, table2, i);
                }

                AddRowToMedicationConsumptionTable(table2, medicationExpenditure);
            }

            _lastPage.Elements.Add(table2);
            return table2;
        }

        private void AddHeaderRowToMedicationConsumptionTable(Table2 table2)
        {
            Row2 row2 = table2.Rows.Add(40, Font.HelveticaBold, 16, RgbColor.Black,
                RgbColor.Gray);
            row2.CellDefault.Align = TextAlign.Center;
            row2.CellDefault.VAlign = VAlign.Center;
            row2.Cells.Add("Medication name");
            row2.Cells.Add("Amount spent");
            _currentY += 50;
        }

        private static void AddRowToMedicationConsumptionTable(Table2 table2, MedicationExpenditureDTO medicationExpenditure)
        {
            Row2 row = table2.Rows.Add(30);
            row.CellDefault.Align = TextAlign.Center;
            row.CellDefault.VAlign = VAlign.Center;
            row.Cells.Add(medicationExpenditure.MedicineName);
            row.Cells.Add(Convert.ToString(medicationExpenditure.Amount));
        }

        private Table2 NewPageConsumptionReport(MedicineConsumptionReportToPdfDTO dto, Table2 table2, int i)
        {
            _lastPage.Elements.Add(table2);
            AddNewPage();
            _currentY = 0;

            table2 = new Table2(0, _currentY + 20, 503, (dto.MedicineConsumptions.Count - i) * 31 + 2);
            table2.CellDefault.Border.Color = RgbColor.Blue;
            table2.CellSpacing = 1.0f;
            table2.Columns.Add(350);
            table2.Columns.Add(150);
            return table2;
        }

        public string MakePrescriptionPdf(PrescriptionDTO dto, string requestType)
        {
            MakeTitle("Prescription");
            WriteLine(0, 60, "Patient first name: "
                             + dto.PatientFirstName);
            WriteLine(0, 20, "Patient last name: "
                             + dto.PatientLastName);
            WriteLine(0, 20, "Medicine name: "
                             + dto.MedicineName);
            WriteLine(0, 20, "Consumption, from "
                             + dto.StartDate.ToShortDateString()
                             + " to " + dto.EndDate.ToShortDateString());
            WriteLine(0, 20, "Issued: "
                             + dto.IssuedDate.ToShortDateString());

            WriteLine(0, 40, "Hospital info");
            HospitalDTO hospitalDto = new HospitalDTO()
            {
                Name = "Heaven's Pass Medicare",
                StreetName = "Dunavska",
                StreetNumber = "17",
                CityName = "Novi Sad"
            };
            WriteLine(0, 20, "Hospital name: "
                             + hospitalDto.Name);
            WriteLine(0, 20, "Address: " + hospitalDto.StreetName + " " + hospitalDto.StreetNumber + ", " + hospitalDto.CityName);

            string fileName = "";
            switch (requestType)
            {
                case "http":
                    AddQrCodePrescription(dto, hospitalDto, 400, 30);
                    fileName = "Prescription-http-" + dto.IssuedDate.Ticks.ToString() + ".pdf";
                    SaveDocument("Prescriptions" + Path.DirectorySeparatorChar + "Http" + Path.DirectorySeparatorChar + fileName);
                    break;
                case "sftp":
                    fileName = "Prescription-sftp-" + dto.IssuedDate.Ticks.ToString() + ".pdf";
                    SaveDocument("Prescriptions" + Path.DirectorySeparatorChar + "Sftp" + Path.DirectorySeparatorChar + fileName);
                    break;
            }

            return fileName;
        }

        public string MakeTenderStatisticsPdf(TenderStatisticsDto tenderStatisticsDto, TimeRange timeRange)
        {
            MakeTitle("Tender statistics");
            WriteLine(0, 60, "Start date: " + timeRange.StartDate.ToShortDateString());
            WriteLine(0, 20, "End date: " + timeRange.EndDate.ToShortDateString());

            List<string> labelsTenderOffers = new List<string>();
            List<float> valuesTenderOffers = new List<float>();
            List<string> labelsTendersEntered = new List<string>();
            List<float> valuesTendersEntered = new List<float>();
            List<string> labelsTendersWon = new List<string>();
            List<float> valuesTendersWon = new List<float>();
            List<string> labelsProfit = new List<string>();
            List<float> valuesProfit = new List<float>();

            foreach (var pharmacyStatistic in tenderStatisticsDto.PharmacyStatistics)
            {
                valuesTenderOffers.Add((float)pharmacyStatistic.TenderOffersMade);
                valuesTendersEntered.Add((float)pharmacyStatistic.TendersEntered);
                valuesTendersWon.Add((float)pharmacyStatistic.TendersWon);
                valuesProfit.Add((float)pharmacyStatistic.Profit.Amount);

                labelsTenderOffers.Add(pharmacyStatistic.PharmacyName);
                labelsTendersEntered.Add(pharmacyStatistic.PharmacyName);
                labelsTendersWon.Add(pharmacyStatistic.PharmacyName);
                labelsProfit.Add(pharmacyStatistic.PharmacyName);
            }

            DrawChart("Tender offers made", "Number of offers", labelsTenderOffers, valuesTenderOffers); 
            DrawChart("Tenders entered", "Number of tenders entered", labelsTendersEntered, valuesTendersEntered);
            AddNewPage();
            DrawChart("Tenders won", "Number of tenders won", labelsTendersWon, valuesTendersWon);
            DrawChart("Profit made", "Amount", labelsProfit, valuesProfit);

            string fileName = "TenderStatistics-" + timeRange.StartDate.Ticks.ToString()
                                                  + "-" + timeRange.EndDate.Ticks.ToString() + ".pdf";
            string filePath = "TenderStatistics" + Path.DirectorySeparatorChar + fileName;
            SaveDocument(filePath);
            return fileName;
        }

        public void MakeTitle(string text)
        {
            Label naslov = new Label(text, _currentY, 0, 512, 16, Font.HelveticaBold, 16, TextAlign.Center, RgbColor.Black);
            _lastPage.Elements.Add(naslov);
        }

        private void AddQrCodePrescription(PrescriptionDTO prescDto, HospitalDTO hospDto, float xMargin, float yMargin)
        {
            string text = MakePrescriptionTextForQr(prescDto, hospDto);
            QrCode qrCode = new QrCode(text, xMargin, _currentY + yMargin);
            _currentY = _currentY + yMargin;
            _lastPage.Elements.Add(qrCode);
        }

        private string MakePrescriptionTextForQr(PrescriptionDTO prescDto, HospitalDTO hospDto)
        {
            string text = "";
            text += prescDto.PatientFirstName + ";";
            text += prescDto.PatientLastName + ";";
            text += prescDto.StartDate.ToShortDateString() + ";";
            text += prescDto.EndDate.ToShortDateString() + ";";
            text += prescDto.IssuedDate.ToShortDateString() + ";";
            text += hospDto.Name + ";";
            text += hospDto.StreetName + " " + hospDto.StreetNumber + ", " + hospDto.CityName + ";";
            return text;
        }

        private void DrawChart(string titleText, string label, List<string> labels, List<float> values)
        {
            _currentY = _currentY + 50;
            Chart chart = new Chart(0, _currentY, 500, 250);
            _currentY = _currentY + 250;
            PlotArea plotArea = chart.PrimaryPlotArea;

            Title title = new Title(titleText);
            chart.HeaderTitles.Add(title);

            IndexedColumnSeries firstColumn = FillDataAndXAxis(labels, values, plotArea); ;

            Title lTitle = new Title(label);
            if (labels.Count > 0)
                firstColumn.YAxis.Titles.Add(lTitle);

            _lastPage.Elements.Add(chart);
        }

        private static IndexedColumnSeries FillDataAndXAxis(List<string> labels, List<float> values, PlotArea plotArea)
        {
            IndexedColumnSeries firstColumn = new IndexedColumnSeries("");
            for (int i = 0; i < labels.Count; i++)
            {
                IndexedColumnSeries columnSeries = new IndexedColumnSeries(labels.ElementAt(i));
                if (i == 0)
                    firstColumn = columnSeries;
                columnSeries.Values.Add(new float[] {values.ElementAt(i)});

                AutoGradient autogradient = GetAutogradient(i);

                columnSeries.Color = autogradient;

                plotArea.Series.Add(columnSeries);

                firstColumn.XAxis.Labels.Add(new IndexedXAxisLabel(labels.ElementAt(i), i));
            }

            return firstColumn;
        }

        private static AutoGradient GetAutogradient(int i)
        {
            AutoGradient autogradient;
            switch (i % 3)
            {
                case 0:
                    autogradient = new AutoGradient(180f, CmykColor.Red, CmykColor.IndianRed);
                    break;
                case 1:
                    autogradient = new AutoGradient(180f, CmykColor.Green, CmykColor.YellowGreen);
                    break;
                default:
                    autogradient = new AutoGradient(180f, CmykColor.Blue, CmykColor.LightBlue);
                    break;
            }

            return autogradient;
        }
    }
}

using System;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.PageElements.BarCoding;
using IntegrationAPI.DTO.MedicineConsumption;
using IntegrationAPI.DTO.Prescription;
using IntegrationAPI.DTO.Shared;
using Path = System.IO.Path;

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
            Table2 table = new Table2(0, _currentY + 20, 503, dto.MedicineConsumptions.Count * 31 + 42);
            table.CellDefault.Border.Color = RgbColor.Blue;
            table.CellSpacing = 1.0f;
            table.Columns.Add(350);
            table.Columns.Add(150);
            Row2 row1 = table.Rows.Add(40, Font.HelveticaBold, 16, RgbColor.Black,
                RgbColor.Gray);
            row1.CellDefault.Align = TextAlign.Center;
            row1.CellDefault.VAlign = VAlign.Center;
            row1.Cells.Add("Medication name");
            row1.Cells.Add("Amount spent");
            _currentY += 50;
            for (int i = 0; i < dto.MedicineConsumptions.Count; i++)
            {
                MedicationExpenditureDTO medicationExpenditure = dto.MedicineConsumptions[i];
                _currentY += 32;
                if (_currentY >= 650)
                {
                    _lastPage.Elements.Add(table);
                    AddNewPage();
                    _currentY = 0;
                    table = new Table2(0, _currentY + 20, 503, (dto.MedicineConsumptions.Count - i) * 31 + 2);
                    table.CellDefault.Border.Color = RgbColor.Blue;
                    table.CellSpacing = 1.0f;
                    table.Columns.Add(350);
                    table.Columns.Add(150);
                }
                Row2 row = table.Rows.Add(30);
                row.CellDefault.Align = TextAlign.Center;
                row.CellDefault.VAlign = VAlign.Center;
                row.Cells.Add(medicationExpenditure.MedicineName);
                row.Cells.Add(Convert.ToString(medicationExpenditure.Amount));
            }
            _lastPage.Elements.Add(table);
            if (_document.Pages.Count > 1) _currentY = table.Height;
            else _currentY = 100 + table.Height;
            WriteLine(350,30, dto.HospitalName);
            string fileName = "Report-" + dto.CreatedDate.Ticks.ToString() + ".pdf";
            SaveDocument("MedicineReports" + Path.DirectorySeparatorChar + fileName);
            return fileName;
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
                Name = "Nasa bolnica",
                StreetName = "Vojvode Stepe",
                StreetNumber = "14",
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
    }
}

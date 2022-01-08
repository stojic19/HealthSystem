using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using PharmacyApi.DTO;
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

        public void MakeTitle(string text)
        {
            Label naslov = new Label(text, _currentY, 0, 512, 16, Font.HelveticaBold, 16, TextAlign.Center, RgbColor.Black);
            _lastPage.Elements.Add(naslov);
        }

        public string MakeMedicineSpecificationPdf(MedicineDTO dto)
        {
            MakeTitle(dto.Name + " Specification");
            WriteLine(0, 20, "Manufacturer: " + dto.Manufacturer);
            WriteLine(0, 20, "Type: " + dto.Type);
            WriteLine(0, 20, "Usage: " + dto.Usage);
            CreateTextArea(0, 20, "Side effects: " + dto.SideEffects);
            CreateTextArea(0, 20, "Reactions: " + dto.Reactions);
            CreateTextArea(0, 20, "Precautions: " + dto.Precautions);
            CreateTextArea(0, 20, "Potential dangers: " + dto.MedicinePotentialDangers);
            WriteLine(0, 20, "Substances: ");
            Table2 table = new Table2(50, _currentY + 20, 402, dto.Substances.Count * 31 + 42);
            table.CellDefault.Border.Color = RgbColor.Blue;
            table.CellSpacing = 1.0f;
            table.Columns.Add(400);
            Row2 row1 = table.Rows.Add(40, Font.HelveticaBold, 16, RgbColor.Black,
                RgbColor.Gray);
            row1.CellDefault.Align = TextAlign.Center;
            row1.CellDefault.VAlign = VAlign.Center;
            row1.Cells.Add("Substance name");
            _currentY += 40;
            for (int i = 0; i < dto.Substances.Count; i++)
            {
                string substance = dto.Substances[i];
                _currentY += 32;
                if (_currentY >= 650)
                {
                    _lastPage.Elements.Add(table);
                    AddNewPage();
                    _currentY = 0;
                    table = new Table2(0, _currentY + 20, 503, (dto.Substances.Count - i) * 31 + 2);
                    table.CellDefault.Border.Color = RgbColor.Blue;
                    table.CellSpacing = 1.0f;
                    table.Columns.Add(400);
                }
                Row2 row = table.Rows.Add(30);
                row.CellDefault.Align = TextAlign.Center;
                row.CellDefault.VAlign = VAlign.Center;
                row.Cells.Add(substance);
            }
            _lastPage.Elements.Add(table);
            if (_document.Pages.Count > 1) _currentY = table.Height;
            //else _currentY = 200 + table.Height;
            WriteLine(400, 30, "Pharmacy name");
            string fileName = dto.Name + DateTime.Now.Ticks + ".pdf";
            SaveDocument("MedicineSpecifications" + Path.DirectorySeparatorChar + fileName);
            return fileName;
        }

        public void CreateTextArea(float xMargin, float yMargin, string text)
        {
            int rows = (int)(text.Length / 75) + 1;
            TextArea textArea = new TextArea(text, xMargin, _currentY + yMargin, 512, rows * 20, Font.HelveticaBold, _fontSize,
                TextAlign.Left, RgbColor.Black);
            _currentY = _currentY + rows * 20;
            _lastPage.Elements.Add(textArea);
        }
    }
}

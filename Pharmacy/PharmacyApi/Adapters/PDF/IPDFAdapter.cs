using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApi.DTO;

namespace IntegrationAPI.Adapters.PDF
{
    public interface IPDFAdapter
    {
        public void AddNewPage();
        public void WriteLine(float xMargin, float yMargin, string text);
        public void SaveDocument(string destination);
        public void SetFontSize(float size);
        public void MakeTitle(string text);
        public string MakeMedicineSpecificationPdf(MedicineDTO dto);
        public void CreateTextArea(float xMargin, float yMargin, string text);
    }
}

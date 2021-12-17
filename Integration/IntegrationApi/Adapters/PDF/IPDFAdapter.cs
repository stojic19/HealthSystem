using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationAPI.DTO;
using IntegrationAPI.DTO.MedicineConsumption;
using IntegrationAPI.DTO.Prescription;

namespace IntegrationAPI.Adapters.PDF
{
    public interface IPDFAdapter
    {
        public void AddNewPage();
        public void WriteLine(float xMargin, float yMargin, string text);
        public void SaveDocument(string destination);
        public void SetFontSize(float size);
        public string MakeMedicineConsumptionReportPdf(MedicineConsumptionReportToPdfDTO dto);
        public string MakePrescriptionPdf(PrescriptionDTO dto, string requestType);
        public void MakeTitle(string text);
    }
}

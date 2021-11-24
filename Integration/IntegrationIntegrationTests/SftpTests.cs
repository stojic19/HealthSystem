using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Integration.MasterServices;
using Integration.Model;
using Xunit;

namespace IntegrationClassLibTests
{
    public class SftpTests
    {
        [Fact]
        public void Save_medicine_consumption()
        {
            List<Medicine> medicines = TestData.GetMedicines();
            List<MedicineConsumption> medicineConsumptions = new List<MedicineConsumption>();
            medicineConsumptions.Add(new MedicineConsumption
            {
                Medicine = medicines[2],
                Amount = 5
            });
            medicineConsumptions.Add(new MedicineConsumption
            {
                Medicine = medicines[1],
                Amount = 6
            });
            medicineConsumptions.Add(new MedicineConsumption
            {
                Medicine = medicines[0],
                Amount = 2
            });
            MedicineConsumptionReport consumptionReport = new MedicineConsumptionReport
            {
                startDate = new DateTime(2020, 11, 1),
                endDate = new DateTime(2021, 11, 1),
                createdDate = DateTime.Now,
                MedicineConsumptions = medicineConsumptions
            };
            SftpMasterService service = new SftpMasterService();
            bool exceptionThrown = false;
            try
            {
                service.SaveMedicineReportToSftpServer(consumptionReport);
            }
            catch (Exception e)
            {
                exceptionThrown = true;
            }
            Assert.False(exceptionThrown);
        }
    }
}

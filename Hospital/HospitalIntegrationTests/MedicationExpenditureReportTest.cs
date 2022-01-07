using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Model.Wrappers;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace HospitalIntegrationTests
{
    public class MedicationExpenditureReportTest : BaseTest
    {
        public MedicationExpenditureReportTest(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Get_medication_expenditure_report_should_return_4()
        {
            var medications = AddDataToDataBase();
            var content = GetContent(new TimePeriod(new DateTime(2020, 9, 1), new DateTime(2020, 10, 1)));
            var response = await Client.PostAsync(BaseUrl + "api/MedicationExpenditureReport/GetMedicationExpenditureReport", content);
            var responseContentString = await response.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<MedicationExpenditureReportDTO>(responseContentString);
            DeleteDataFromDataBase(medications);
            responseContent.MedicationExpenditureDTO.Count.ShouldBe(4);
            responseContent.MedicationExpenditureDTO[0].Amount.ShouldBe(5);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        private void DeleteDataFromDataBase(List<Medication> medications)
        {
            var medDelRepo = UoW.GetRepository<IMedicationWriteRepository>();
            foreach (Medication medication in medications)
            {
                medDelRepo.Delete(medication);
            }
        }

        private List<Medication> AddDataToDataBase()
        {
            List<Medication> medications = new List<Medication>();
            medications.Add(new Medication
            {
                Name = "EXPENDITURE_REPORT_MED1"
            });
            medications.Add(new Medication
            {
                Name = "EXPENDITURE_REPORT_MED2"
            });
            medications.Add(new Medication
            {
                Name = "EXPENDITURE_REPORT_MED3"
            });
            medications.Add(new Medication
            {
                Name = "EXPENDITURE_REPORT_MED4"
            });
            var medRepo = UoW.GetRepository<IMedicationWriteRepository>();
            foreach (Medication medication in medications)
            {
                medRepo.Add(medication);
            }

            List<MedicationExpenditureLog> logs = new List<MedicationExpenditureLog>();
            logs.Add(new MedicationExpenditureLog
            {
                Medication = medications[0],
                MedicationId = medications[0].Id,
                AmountSpent = 5,
                Date = new DateTime(2020, 9, 10)
            });
            logs.Add(new MedicationExpenditureLog
            {
                Medication = medications[1],
                MedicationId = medications[1].Id,
                AmountSpent = 3,
                Date = new DateTime(2020, 9, 12)
            });
            logs.Add(new MedicationExpenditureLog
            {
                Medication = medications[3],
                MedicationId = medications[3].Id,
                AmountSpent = 1,
                Date = new DateTime(2020, 9, 13)
            });
            logs.Add(new MedicationExpenditureLog
            {
                Medication = medications[1],
                MedicationId = medications[1].Id,
                AmountSpent = 1,
                Date = new DateTime(2020, 9, 15)
            });
            logs.Add(new MedicationExpenditureLog
            {
                Medication = medications[2],
                MedicationId = medications[2].Id,
                AmountSpent = 1,
                Date = new DateTime(2020, 9, 29)
            });
            logs.Add(new MedicationExpenditureLog
            {
                Medication = medications[3],
                MedicationId = medications[3].Id,
                AmountSpent = 3,
                Date = new DateTime(2020, 9, 20)
            });
            logs.Add(new MedicationExpenditureLog
            {
                Medication = medications[0],
                MedicationId = medications[0].Id,
                AmountSpent = 12,
                Date = new DateTime(2020, 10, 10)
            });
            logs.Add(new MedicationExpenditureLog
            {
                Medication = medications[2],
                MedicationId = medications[2].Id,
                AmountSpent = 16,
                Date = new DateTime(2020, 9, 10)
            });
            var logRepo = UoW.GetRepository<IMedicationExpenditureLogWriteRepository>();
            foreach (MedicationExpenditureLog log in logs)
            {
                logRepo.Add(log);
            }

            return medications;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.SharedModel.Model.Wrappers;
using HospitalUnitTests.Base;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace HospitalUnitTests
{
    public class MedicationConsumptionTests : BaseTest
    {
        public MedicationConsumptionTests(BaseFixture fixture) : base(fixture)
        {
            ClearDbContext();
            MakeLogs();
            Context.SaveChanges();
        }
        
        [Theory]
        [MemberData(nameof(GetLogsData))]
        public void Get_receipts_in_time_range(TimePeriod timePeriod, int shouldBe)
        {
            var logs = UoW.GetRepository<IMedicationExpenditureLogReadRepository>()
                .GetMedicationExpenditureLogsInTimePeriod(timePeriod);
            logs.Count().ShouldBe(shouldBe);
        }

        public static IEnumerable<object[]> GetLogsData()
        {
            List<object[]> retVal = new List<object[]>();
            retVal.Add(new object[]
                {new TimePeriod(new DateTime(2021, 9, 1), new DateTime(2021, 10, 1)), 3});
            retVal.Add(new object[]
                {new TimePeriod (new DateTime(2020, 9, 1), new DateTime(2021, 11, 1)), 5});
            retVal.Add(new object[]
                {new TimePeriod(new DateTime(2021, 10, 1), new DateTime(2021, 11, 1)), 1});
            retVal.Add(new object[]
                {new TimePeriod(new DateTime(2021, 11, 1), new DateTime(2021, 12, 1)), 1});
            return retVal;
        }

        [Fact]
        public void Calculate_medicine_consumptions()
        {
            var logs = UoW.GetRepository<IMedicationExpenditureLogReadRepository>().GetAll().Include(x => x.Medication);
            MedicationConsumptionService medicationConsumptionService = new MedicationConsumptionService();
            IEnumerable<MedicationConsumption> medicationConsumptions = medicationConsumptionService.CalculateMedicationConsumptions(logs);
            medicationConsumptions.Count().ShouldBe(3);
        }
        [Theory]
        [MemberData(nameof(GetTimePeriods))]
        public void Create_medication_report(TimePeriod timePeriod, int shouldBe)
        {
            TimePeriod september = new TimePeriod(new DateTime(2021, 9, 1), new DateTime(2021, 10, 1));
            MedicationConsumptionReportService service = new MedicationConsumptionReportService(UoW);
            MedicationConsumptionReport report = service.CreateMedicationExpenditureReportInTimePeriod(timePeriod);
            report.MedicationConsumptions.Count().ShouldBe(shouldBe);
        }
        public static IEnumerable<object[]> GetTimePeriods()
        {
            TimePeriod september = new TimePeriod(new DateTime(2021, 9, 1), new DateTime(2021, 10, 1));
            TimePeriod november = new TimePeriod(new DateTime(2021, 11, 1), new DateTime(2021, 12, 1));
            TimePeriod december = new TimePeriod(new DateTime(2021, 12, 1), new DateTime(2022, 1, 1));
            List<object[]> retVal = new List<object[]>();
            retVal.Add(new object[] { september, 3 });
            retVal.Add(new object[] { november, 1 });
            retVal.Add(new object[] { december, 2 });
            return retVal;
        }

        private void MakeLogs()
        {
            Medication aspirin = new Medication { Id = 1, Name = "Aspirin" };
            Medication probiotik = new Medication { Id = 2, Name = "Probiotik" };
            Medication brufen = new Medication { Id = 3, Name = "Brufen" };
            Context.Medications.Add(aspirin);
            Context.Medications.Add(probiotik);
            Context.Medications.Add(brufen);
            MedicationExpenditureLog log1 = new MedicationExpenditureLog
            {
                Id = 1,
                Date = new DateTime(2021, 9, 30),
                Medication = brufen,
                AmountSpent = 4
            };
            MedicationExpenditureLog log2 = new MedicationExpenditureLog
            {
                Id = 2,
                Date = new DateTime(2021, 5, 19),
                Medication = probiotik,
                AmountSpent = 2
            };
            MedicationExpenditureLog log3 = new MedicationExpenditureLog
            {
                Id = 3,
                Date = new DateTime(2021, 9, 19),
                Medication = probiotik,
                AmountSpent = 4
            };
            MedicationExpenditureLog log4 = new MedicationExpenditureLog
            {
                Id = 4,
                Date = new DateTime(2021, 10, 19),
                Medication = brufen,
                AmountSpent = 1
            };
            MedicationExpenditureLog log5 = new MedicationExpenditureLog
            {
                Id = 5,
                Date = new DateTime(2021, 9, 5),
                Medication = aspirin,
                AmountSpent = 2
            };
            MedicationExpenditureLog log6 = new MedicationExpenditureLog
            {
                Id = 6,
                Date = new DateTime(2021, 11, 5),
                Medication = aspirin,
                AmountSpent = 5
            };
            MedicationExpenditureLog log7 = new MedicationExpenditureLog
            {
                Id = 7,
                Date = new DateTime(2021, 12, 5),
                Medication = brufen,
                AmountSpent = 8
            };
            MedicationExpenditureLog log8 = new MedicationExpenditureLog
            {
                Id = 8,
                Date = new DateTime(2021, 12, 6),
                Medication = aspirin,
                AmountSpent = 12
            };
            Context.MedicationExpenditureLogs.Add(log1);
            Context.MedicationExpenditureLogs.Add(log2);
            Context.MedicationExpenditureLogs.Add(log3);
            Context.MedicationExpenditureLogs.Add(log4);
            Context.MedicationExpenditureLogs.Add(log5);
            Context.MedicationExpenditureLogs.Add(log6);
            Context.MedicationExpenditureLogs.Add(log7);
            Context.MedicationExpenditureLogs.Add(log8);
        }
    }
}

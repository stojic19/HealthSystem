using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly MedicationConsumptionService _medicationConsumptionService;
        private readonly MedicationConsumptionReportService _medicationConsumptionReportService;
        public MedicationConsumptionTests(BaseFixture fixture) : base(fixture)
        {
            _medicationConsumptionService = new();
            _medicationConsumptionReportService = new(UoW);
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
            List<object[]> retVal = new()
            {
                new object[]
                {new TimePeriod(new DateTime(2021, 9, 1), new DateTime(2021, 10, 1)), 3},
                new object[]
                {new TimePeriod (new DateTime(2020, 9, 1), new DateTime(2021, 11, 1)), 5},
                new object[]
                {new TimePeriod(new DateTime(2021, 10, 1), new DateTime(2021, 11, 1)), 1},
                new object[]
                {new TimePeriod(new DateTime(2021, 11, 1), new DateTime(2021, 12, 1)), 1}
            };
            return retVal;
        }

        /**
         * DONE:
         * Test ne testira dobro da li se povecao consumption 
         * Lose sve i resistence to refactioring
         * Lose Protection against regression 
         * Important domain logic
         * Ne ispunjava Protection against regressions 
         * Does not validate the outcome
         * 
         */
        [Theory]
        [MemberData(nameof(Data))]
        public void Calculate_medicine_consumptions(Medication medication, int expectedAmount)
        {

            var logs = UoW.GetRepository<IMedicationExpenditureLogReadRepository>().GetAll().Include(x => x.Medication);
            IEnumerable<MedicationConsumption> medicationConsumptions = _medicationConsumptionService.CalculateMedicationConsumptions(logs);
            medicationConsumptions.Count().ShouldBe(3);
            medicationConsumptions.Where(x => x.Medication.Name.Equals(medication.Name)).FirstOrDefault().Amount.ShouldBe(expectedAmount);
        }
        public static List<object[]> Data()
        {
            Medication brufen = new() { Id = 3, Name = "Brufen" };
            Medication aspirin = new() { Id = 1, Name = "Aspirin" };
            Medication probiotik = new() { Id = 2, Name = "Probiotik" };
            return new List<object[]>
            {
            new object[] { brufen, 13 },
            new object[] { aspirin, 19 },
            new object[] { probiotik, 6 },

            };
        }
    
        [Theory]
        [MemberData(nameof(GetTimePeriods))]
        public void Create_medication_report_for_a_time_period(TimePeriod timePeriod, int shouldBe)
        {     
            MedicationConsumptionReport report = _medicationConsumptionReportService.CreateMedicationExpenditureReportInTimePeriod(timePeriod);
            report.MedicationConsumptions.Count().ShouldBe(shouldBe);
            report.StartDate.ShouldBe(timePeriod.StartTime);
            report.EndDate.ShouldBe(timePeriod.EndTime);
        }
        public static IEnumerable<object[]> GetTimePeriods()
        {
            TimePeriod september = new(new DateTime(2021, 9, 1), new DateTime(2021, 10, 1));
            TimePeriod november = new(new DateTime(2021, 11, 1), new DateTime(2021, 12, 1));
            TimePeriod december = new(new DateTime(2021, 12, 1), new DateTime(2022, 1, 1));
            List<object[]> retVal = new();
            retVal.Add(new object[] { september, 3 });
            retVal.Add(new object[] { november, 1 });
            retVal.Add(new object[] { december, 2 });
            return retVal;
        }

        private void MakeLogs()
        {
            Medication aspirin = new() { Id = 1, Name = "Aspirin" };
            Medication probiotik = new() { Id = 2, Name = "Probiotik" };
            Medication brufen = new() { Id = 3, Name = "Brufen" };
            Context.Medications.Add(aspirin);
            Context.Medications.Add(probiotik);
            Context.Medications.Add(brufen);
            MedicationExpenditureLog log1 = new()
            {
                Id = 1,
                Date = new DateTime(2021, 9, 30),
                Medication = brufen,
                AmountSpent = 4
            };
            MedicationExpenditureLog log2 = new()
            {
                Id = 2,
                Date = new DateTime(2021, 5, 19),
                Medication = probiotik,
                AmountSpent = 2
            };
            MedicationExpenditureLog log3 = new()
            {
                Id = 3,
                Date = new DateTime(2021, 9, 19),
                Medication = probiotik,
                AmountSpent = 4
            };
            MedicationExpenditureLog log4 = new()
            {
                Id = 4,
                Date = new DateTime(2021, 10, 19),
                Medication = brufen,
                AmountSpent = 1
            };
            MedicationExpenditureLog log5 = new()
            {
                Id = 5,
                Date = new DateTime(2021, 9, 5),
                Medication = aspirin,
                AmountSpent = 2
            };
            MedicationExpenditureLog log6 = new()
            {
                Id = 6,
                Date = new DateTime(2021, 11, 5),
                Medication = aspirin,
                AmountSpent = 5
            };
            MedicationExpenditureLog log7 = new()
            {
                Id = 7,
                Date = new DateTime(2021, 12, 5),
                Medication = brufen,
                AmountSpent = 8
            };
            MedicationExpenditureLog log8 = new()
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

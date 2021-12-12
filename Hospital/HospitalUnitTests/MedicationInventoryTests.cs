using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using HospitalUnitTests.Base;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalUnitTests
{
    public class MedicationInventoryTests : BaseTest
    {
        public MedicationInventoryTests(BaseFixture fixture) : base(fixture)
        {

        }
        [Fact]
        public void Get_all_medicines()
        {
            ClearAndMakeData();
            var medicines = UoW.GetRepository<IMedicationReadRepository>()
                .GetAll();

            medicines.ShouldNotBeNull();
            medicines.Count().ShouldBe(2);
        }
        [Fact]
        public void Get_all_medicine_inventory()
        {
            ClearAndMakeData();
            var medicineInventory = UoW.GetRepository<IMedicationInventoryReadRepository>()
                .GetAll();

            medicineInventory.ShouldNotBeNull();
            medicineInventory.Count().ShouldBe(2);
        }
        [Fact]
        public void Add_existing_medication_check_quantity_in_inventory()
        {
            ClearAndMakeData();
            MedicationInventoryMasterService medicineInventoryMasterService = new MedicationInventoryMasterService(UoW);
            medicineInventoryMasterService.AddMedicineToInventory("Brufen", 10);

            var medicineInventory = UoW.GetRepository<IMedicationInventoryReadRepository>().GetById(1);
            medicineInventory.Quantity.ShouldBe(20);
        }
        [Fact]
        public void Add_existing_medication_check_count_in_medicines()
        {
            ClearAndMakeData();
            MedicationInventoryMasterService medicineInventoryMasterService = new MedicationInventoryMasterService(UoW);
            medicineInventoryMasterService.AddMedicineToInventory("Brufen", 10);

            var medicineInventory = UoW.GetRepository<IMedicationReadRepository>().GetAll();
            medicineInventory.Count().ShouldBe(2);
        }
        [Fact]
        public void Add_existing_medication_check_count_in_medicine_inventory()
        {
            ClearAndMakeData();
            MedicationInventoryMasterService medicineInventoryMasterService = new MedicationInventoryMasterService(UoW);
            medicineInventoryMasterService.AddMedicineToInventory("Brufen", 10);

            var medicineInventory = UoW.GetRepository<IMedicationInventoryReadRepository>().GetAll();
            medicineInventory.Count().ShouldBe(2);
        }
        [Fact]
        public void Add_new_medication_check_name_in_medicines()
        {
            ClearAndMakeData();
            MedicationInventoryMasterService medicineInventoryMasterService = new MedicationInventoryMasterService(UoW);
            medicineInventoryMasterService.AddMedicineToInventory("Diklofenat", 10);

            var medicineInventory = UoW.GetRepository<IMedicationInventoryReadRepository>().GetById(3);
            medicineInventory.Medication.Name.ShouldBe("Diklofenat");

        }
        private void ClearAndMakeData()
        {
            ClearDbContext();
            MakeMedicineInventory();
        }
        private void MakeMedicineInventory()
        {
            Context.MedicationInventory.Add(new MedicationInventory()
            {
                Id = 1,
                MedicationId = 1,
                Quantity = 10,
                Medication = new Medication()
                {
                    Id = 1,
                    Name = "Brufen"
                }
            });
            Context.MedicationInventory.Add(new MedicationInventory()
            {
                Id = 2,
                MedicationId = 2,
                Quantity = 10,
                Medication = new Medication()
                {
                    Id = 2,
                    Name = "Aspirin"
                }
            });

            Context.SaveChanges();
        }
    }
}

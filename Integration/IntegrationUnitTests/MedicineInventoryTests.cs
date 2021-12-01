using IntegrationUnitTests.Base;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Partnership.Model;
using Integration.Partnership.Repository;
using Integration.Partnership.Service;
using Integration.Shared.Model;
using Integration.Shared.Repository;
using Xunit;

namespace IntegrationUnitTests
{
    public class MedicineInventoryTests : BaseTest
    {
        public MedicineInventoryTests(BaseFixture fixture) : base(fixture)
        {
            
        }
        [Fact]
        public void Get_all_medicines()
        {
            ClearAndMakeData();
            var medicines = UoW.GetRepository<IMedicineReadRepository>()
                .GetAll();

            medicines.ShouldNotBeNull();
            medicines.Count().ShouldBe(2);
        }
        [Fact]
        public void Get_all_medicine_inventory()
        {
            ClearAndMakeData();
            var medicineInventory = UoW.GetRepository<IMedicineInventoryReadRepository>()
                .GetAll();

            medicineInventory.ShouldNotBeNull();
            medicineInventory.Count().ShouldBe(2);
        }
        [Fact]
        public void Add_existing_medication_check_quantity_in_inventory()
        {
            ClearAndMakeData();
            MedicineInventoryMasterService medicineInventoryMasterService = new MedicineInventoryMasterService(UoW);
            medicineInventoryMasterService.AddMedicineToInventory("Brufen", 10);

            var medicineInventory = UoW.GetRepository<IMedicineInventoryReadRepository>().GetById(1);
            medicineInventory.Quantity.ShouldBe(20);
        }
        [Fact]
        public void Add_existing_medication_check_count_in_medicines()
        {
            ClearAndMakeData();
            MedicineInventoryMasterService medicineInventoryMasterService = new MedicineInventoryMasterService(UoW);
            medicineInventoryMasterService.AddMedicineToInventory("Brufen", 10);

            var medicineInventory = UoW.GetRepository<IMedicineReadRepository>().GetAll();
            medicineInventory.Count().ShouldBe(2);
        }
        [Fact]
        public void Add_existing_medication_check_count_in_medicine_inventory()
        {
            ClearAndMakeData();
            MedicineInventoryMasterService medicineInventoryMasterService = new MedicineInventoryMasterService(UoW);
            medicineInventoryMasterService.AddMedicineToInventory("Brufen", 10);

            var medicineInventory = UoW.GetRepository<IMedicineInventoryReadRepository>().GetAll();
            medicineInventory.Count().ShouldBe(2);
        }
        [Fact]
        public void Add_new_medication_check_name_in_medicines()
        {
            ClearAndMakeData();
            MedicineInventoryMasterService medicineInventoryMasterService = new MedicineInventoryMasterService(UoW);
            medicineInventoryMasterService.AddMedicineToInventory("Diklofenat", 10);

            var medicineInventory = UoW.GetRepository<IMedicineInventoryReadRepository>().GetById(3);
            medicineInventory.Medicine.Name.ShouldBe("Diklofenat");

        }
        private void ClearAndMakeData()
        {
            ClearDbContext();
            MakeMedicineInventory();
        }
        private void MakeMedicineInventory()
        {
            Context.MedicineInventory.Add(new MedicineInventory()
            {
                Id = 1,
                MedicineId = 1,
                Quantity = 10,
                Medicine = new Medicine()
                {
                    Id = 1,
                    Name = "Brufen"
                }
            });
            Context.MedicineInventory.Add(new MedicineInventory()
            {
                Id = 2,
                MedicineId = 2,
                Quantity = 10,
                Medicine = new Medicine()
                {
                    Id = 2,
                    Name = "Aspirin"
                }
            });

            Context.SaveChanges();
        }
    }
}

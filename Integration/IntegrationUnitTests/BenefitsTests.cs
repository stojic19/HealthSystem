using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Xunit;
using IntegrationUnitTests.Base;
using Integration.Partnership.Repository;
using Integration.Pharmacies.Model;
using Integration.Shared.Model;
using Integration.Partnership.Model;

namespace IntegrationUnitTests
{
    public class BenefitsTests : BaseTest
    {
        public BenefitsTests(BaseFixture fixture) : base(fixture)
        {
            
        }

        [Fact]
        public void Get_all_benefits()
        {
            ClearDbContext();
            MakeBenefits();

            var benefits = UoW.GetRepository<IBenefitReadRepository>()
                .GetAll();

            benefits.ShouldNotBeNull();
            benefits.Count().ShouldBe(3);
        }

        [Fact]
        public void Get_visible_benefits()
        {
            ClearDbContext();
            MakeBenefits();

            var benefits = UoW.GetRepository<IBenefitReadRepository>()
                .GetVisibleBenefits();

            benefits.ShouldNotBeNull();
            benefits.Count().ShouldBe(3);
            benefits.ElementAt(0).Pharmacy.Name.ShouldBe("Test pharmacy");
            benefits.ElementAt(0).Pharmacy.City.Name.ShouldBe("Test city");
            benefits.ElementAt(0).Pharmacy.City.Country.Name.ShouldBe("Test country");
        }

        [Fact]
        public void Get_published_benefits()
        {
            ClearDbContext();
            MakeBenefits();

            var benefits = UoW.GetRepository<IBenefitReadRepository>()
                .GetPublishedBenefits();

            benefits.ShouldNotBeNull();
            benefits.Count().ShouldBe(2);
            benefits.ElementAt(0).Pharmacy.Name.ShouldBe("Test pharmacy");
            benefits.ElementAt(0).Pharmacy.City.Name.ShouldBe("Test city");
            benefits.ElementAt(0).Pharmacy.City.Country.Name.ShouldBe("Test country");
        }

        [Fact]
        public void Get_relevant_benefits()
        {
            ClearDbContext();
            MakeBenefits();

            var benefits = UoW.GetRepository<IBenefitReadRepository>()
                .GetRelevantBenefits();

            benefits.ShouldNotBeNull();
            benefits.Count().ShouldBe(1);
            benefits.ElementAt(0).Pharmacy.Name.ShouldBe("Test pharmacy");
            benefits.ElementAt(0).Pharmacy.City.Name.ShouldBe("Test city");
            benefits.ElementAt(0).Pharmacy.City.Country.Name.ShouldBe("Test country");
            benefits.ElementAt(0).Description.ShouldBe("Description 2");
        }

        [Fact]
        public void Find_benefit_by_id()
        {
            ClearDbContext();
            MakeBenefits();

            var benefit = UoW.GetRepository<IBenefitReadRepository>()
                .GetById(1);

            benefit.ShouldNotBeNull();
            benefit.Description.ShouldBe("Description 1");
            benefit.Title.ShouldBe("Title 1");
            benefit.Pharmacy.Name.ShouldBe("Test pharmacy");
            benefit.Pharmacy.City.Name.ShouldBe("Test city");
            benefit.Pharmacy.City.Country.Name.ShouldBe("Test country");
        }

        [Fact]
        public void Find_no_benefit_by_id()
        {
            ClearDbContext();
            MakeBenefits();

            var benefit = UoW.GetRepository<IBenefitReadRepository>()
                .GetById(111);

            benefit.ShouldBeNull();
        }

        [Fact]
        public void Publish_benefit()
        {
            ClearDbContext();
            MakeBenefits();

            var benefit = UoW.GetRepository<IBenefitReadRepository>()
                .GetById(1);

            benefit.ShouldNotBeNull();
            benefit.Published = true;

            UoW.GetRepository<IBenefitWriteRepository>().Update(benefit);

            var benefitInBase = UoW.GetRepository<IBenefitReadRepository>()
                .GetById(1);

            benefitInBase.ShouldNotBeNull();
            benefitInBase.Description.ShouldBe("Description 1");
            benefitInBase.Title.ShouldBe("Title 1");
            benefitInBase.Published.ShouldBe(true);
        }

        [Fact]
        public void Hide_benefit()
        {
            ClearDbContext();
            MakeBenefits();

            var benefit = UoW.GetRepository<IBenefitReadRepository>()
                .GetById(1);

            benefit.ShouldNotBeNull();
            benefit.Hidden = true;

            UoW.GetRepository<IBenefitWriteRepository>().Update(benefit);

            var benefitInBase = UoW.GetRepository<IBenefitReadRepository>()
                .GetById(1);

            benefitInBase.ShouldNotBeNull();
            benefitInBase.Description.ShouldBe("Description 1");
            benefitInBase.Title.ShouldBe("Title 1");
            benefitInBase.Hidden.ShouldBe(true);
        }

        private void MakeBenefits()
        {
            Context.Pharmacies.Add(new Pharmacy()
            {
                City = new City()
                {
                    Id = 1,
                    Country = new Country()
                    {
                        Name = "Test country",
                        Id = 1
                    },
                    Name = "Test city"
                },
                Id = 1,
                ApiKey = new Guid(),
                BaseUrl = "baseUrl",
                Name = "Test pharmacy",
                StreetName = "Test Street Name",
                StreetNumber = "1"
            });
            Context.Benefits.Add(new Benefit()
            {
                Description = "Description 1",
                Id = 1,
                PharmacyId = 1,
                Published = false,
                Hidden = false,
                Title = "Title 1",
                StartTime = new DateTime(2021, 11, 17),
                EndTime = new DateTime(2021, 11, 20)
            });
            Context.Benefits.Add(new Benefit()
            {
                Description = "Description 2",
                Id = 2,
                PharmacyId = 1,
                Published = true,
                Hidden = false,
                Title = "Title 2",
                StartTime = new DateTime(2020, 11, 27),
                EndTime = new DateTime(2028, 12, 6)
            });
            Context.Benefits.Add(new Benefit()
            {
                Description = "Description 3",
                Id = 3,
                PharmacyId = 1,
                Published = true,
                Hidden = false,
                Title = "Title 3",
                StartTime = new DateTime(2021, 10, 27),
                EndTime = new DateTime(2021, 11, 6)
            });

            Context.SaveChanges();
        }
    }
}

using Integration.MasterServices;
using Integration.Model;
using Integration.Repositories;
using Integration.Repositories.Base;
using IntegrationClassLibTests.Base;
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

namespace IntegrationClassLibTests.UnitTests
{
    public class ActionsAndBenefitsTests : BaseTest
    {
        public ActionsAndBenefitsTests(BaseFixture fixture) : base(fixture)
        {
            if(Context.Pharmacies.IsNullOrEmpty())
                MakeBenefits();
        }

        [Fact]
        public void Get_all_benefits()
        {
            var benefits = UoW.GetRepository<IBenefitReadRepository>()
                .GetAll();

            benefits.ShouldNotBeNull();
            benefits.Count().ShouldBe(2);
        }

        [Fact]
        public void Find_benefit_by_id()
        {
            var benefit = UoW.GetRepository<IBenefitReadRepository>()
                .GetById(1);

            benefit.ShouldNotBeNull();
            benefit.Description.ShouldBe("Description 1");
            benefit.Title.ShouldBe("Title 1");
        }

        [Fact]
        public void Find_no_benefit_by_id()
        {
            var benefit = UoW.GetRepository<IBenefitReadRepository>()
                .GetById(3);

            benefit.ShouldBeNull();
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
                Name = "Test Pharmacy",
                StreetName = "Test Street Name",
                StreetNumber = "1"
            });
            Context.Benefits.Add(new Benefit()
            {
                Description = "Description 1",
                Id = 1,
                PharmacyId = 1,
                Published = false,
                Title = "Title 1"
            });
            Context.Benefits.Add(new Benefit()
            {
                Description = "Description 2",
                Id = 2,
                PharmacyId = 1,
                Published = true,
                Title = "Title 2"
            });
            
            Context.SaveChanges();
        }
    }
}

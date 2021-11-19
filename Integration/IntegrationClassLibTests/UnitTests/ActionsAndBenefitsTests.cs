﻿using Integration.MasterServices;
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
            benefit.Pharmacy.Name.ShouldBe("Test pharmacy");
            benefit.Pharmacy.City.Name.ShouldBe("Test city");
            benefit.Pharmacy.City.Country.Name.ShouldBe("Test country");
        }

        [Fact]
        public void Find_no_benefit_by_id()
        {
            var benefit = UoW.GetRepository<IBenefitReadRepository>()
                .GetById(3);

            benefit.ShouldBeNull();
        }

        [Fact]
        public void Publish_benefit()
        {
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
                EndTime = new DateTime(2021, 11, 29)
            });
            Context.Benefits.Add(new Benefit()
            {
                Description = "Description 2",
                Id = 2,
                PharmacyId = 1,
                Published = true,
                Hidden = false,
                Title = "Title 2",
                StartTime = new DateTime(2021, 11, 27),
                EndTime = new DateTime(2021, 12, 6)
            });
            
            Context.SaveChanges();
        }
    }
}
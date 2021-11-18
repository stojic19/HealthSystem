using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.EfStructures;
using Hospital.Repositories.Base;
using Xunit;

namespace HospitalIntegrationTests.Base
{
    public class BaseTest : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;

        protected BaseTest(BaseFixture fixture)
        {
            _fixture = fixture;
        }

        protected IUnitOfWork UoW => _fixture.UoW;
    }
}
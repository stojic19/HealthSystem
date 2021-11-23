using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.EfStructures;
using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;
using Xunit;

namespace IntegrationUnitTests.Base
{
    public class BaseTest : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;
        protected BaseTest(BaseFixture fixture)
        {
            _fixture = fixture;
        }
        protected IUnitOfWork UoW => _fixture.UoW;
        protected AppDbContext Context => _fixture.Context;
    }
}

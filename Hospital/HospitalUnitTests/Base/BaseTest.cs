using System;
using System.Collections.Generic;
using System.Text;
using Hospital.EfStructures;
using Hospital.Repositories.Base;
using Xunit;

namespace HospitalUnitTests.Base
{
    public abstract class BaseTest : IClassFixture<BaseFixture>
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

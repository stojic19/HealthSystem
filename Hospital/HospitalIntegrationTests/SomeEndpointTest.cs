using HospitalIntegrationTests.Authentication;
using HospitalIntegrationTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalIntegrationTests
{
    public class SomeEndpointTest : BaseTest
    {
        private readonly BaseFixture _fixture;
        public SomeEndpointTest(BaseFixture fixture) : base(fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public async Task Test1Async()
        {
         
            var client  = _fixture
                .AuthenticatedInstance(
                new Claim("CustomRoleType", "God"))
                .CreateClient();

            var result =await client.GetAsync(BaseUrl + "api/ScheduledEvent/GetFinishedUserEvents/" + "andji");  
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            //TODO : Check Content
        }

        [Fact]
        public async Task Test2Async()
        {
            var client = _fixture.AuthenticatedInstance().CreateClient();
            var result = await client.GetAsync(BaseUrl + "api/ScheduledEvent/GetEventsForSurvey/" + "andji");       
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}

using System.Net;
using System.Threading.Tasks;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace HospitalIntegrationTests
{
    public class SurveyStatisticsIntegrationTests : BaseTest
    {
        public SurveyStatisticsIntegrationTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Get_survey_statistics_code_200OK()
        {
            //Arange

            //Act
            var response = await Client.GetAsync(BaseUrl + "api/SurveyStatistics/GetSurveyStatistics");
            var responseContent = await response.Content.ReadAsStringAsync();
            var sureyStatisticsDTO = JsonConvert.DeserializeObject<SurveyStatisticDTO>(responseContent);
            //Asert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBe(null);
            responseContent.Length.ShouldNotBe(0);
            sureyStatisticsDTO.ShouldBeAssignableTo<SurveyStatisticDTO>();
            sureyStatisticsDTO.CategoriesStatistic.ShouldNotBe(null);
            sureyStatisticsDTO.CategoriesStatistic[0].ShouldBeAssignableTo<CategoryStatisticsDTO>();
            sureyStatisticsDTO.CategoriesStatistic[0].QuestionsStatistic.ShouldNotBe(null);
            sureyStatisticsDTO.CategoriesStatistic[0].QuestionsStatistic[0].ShouldBeAssignableTo<QuestionStatisticsDTO>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.DTOs
{
    public class SurveyStatisticDTO
    {
        public IEnumerable<QuestionStatisticDTO> QuestionsStatistic { get; set; }
        public IEnumerable<double> CategoryAverageRatings { get; set; }
    }
}

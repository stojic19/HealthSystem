using System;
using System.Collections.Generic;

namespace HospitalApi.DTOs
{
    public class SurveyStatisticDTO
    {
        public List<CategoryStatisticsDTO> CategoriesStatistic { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Id { get; set; }
    }
}

import { ISurveySectionStatistic } from "./survey-section-statistic";

export interface ISurveyStatistic {
    id: number;
    createdDate: Date;
    categoriesStatistic: ISurveySectionStatistic[];
}

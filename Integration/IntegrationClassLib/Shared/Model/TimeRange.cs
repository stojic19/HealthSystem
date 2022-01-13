﻿using System;

namespace Integration.Shared.Model
{
    public class TimeRange
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        private TimeRange(){}
        public TimeRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            Validate();
        }
        private void Validate()
        {
            if (StartDate > EndDate) throw new ArgumentException("Start Date should be less than End Date");
        }

        public bool OverlapsWith(TimeRange timeRange)
        {
            return timeRange.Includes(StartDate) || Includes(timeRange.StartDate);
        }

        public bool Includes(DateTime dateTime)
        {
            return StartDate < dateTime && EndDate > dateTime;
        }

        public bool IsInPast()
        {
            return EndDate < DateTime.Now;
        }
    }
}

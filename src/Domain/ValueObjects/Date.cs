using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class Date
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime">You can use and DateTime.</param>

        public Date(int day, int month, int year)
            => (Day, Month, Year) = (day, month, year);

        public Date(DateTime dateTime)
            : this(dateTime.Day, dateTime.Month, dateTime.Year) { }

        public int Day { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }

        public bool EqualTo(Date date)
            => this.Day == date.Day
            && this.Month == date.Month
            && this.Year == date.Year;

        public bool EquelTo(DateTime dateTime)
            => this.Day == dateTime.Day 
            && this.Month == dateTime.Month 
            && this.Year == dateTime.Year;

        public Date ConvertFrom(DateTime dateTime)
        {
            this.Day = dateTime.Day;
            this.Month = dateTime.Month;
            this.Year = dateTime.Year;
            return this;
        }

    }
}

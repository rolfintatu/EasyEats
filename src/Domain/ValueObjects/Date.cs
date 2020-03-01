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
        public Date(DateTime dateTime)
        {
            this.Day = dateTime.Day;
            this.Month = dateTime.Month;
            this.Year = dateTime.Year;
        }

        public Date(int day
            ,int month
            ,int year)
        {
            this.Day = day;
            this.Month = month;
            this.Year = year;
        }

        public int Day { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }

        public bool EqualTo(Date date)
        {
            if (
                this.Day == date.Day
                && this.Month == date.Month
                && this.Year == date.Year
                )
            {
                return true;
            }

            return false;
        }

        public bool EquelTo(DateTime dateTime)
        {
            if (
                this.Day == dateTime.Day 
                && this.Month == dateTime.Month
                && this.Year == dateTime.Year
                )
            {
                return true;
            }

            return false;
        }

        public Date ConvertFrom(DateTime dateTime)
        {
            this.Day = dateTime.Day;
            this.Month = dateTime.Month;
            this.Year = dateTime.Year;

            return this;
        }

    }
}

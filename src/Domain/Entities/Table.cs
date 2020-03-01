using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Table
    {
        public Table()
        {
            this.Reservations = new List<Reservation>();
        }

        public Table(List<Reservation> reservationsList)
        {
            this.Reservations = reservationsList;
        }

        public Table(int chairsCount
            ,TableStatus tableStatus)
        {
            this.ChairsCount = chairsCount;
            this.Status = tableStatus;

            this.Reservations = new List<Reservation>();
        }

        public Table(List<Reservation> reservations
            ,int chairsCount
            , TableStatus tableStatus)
        {
            this.ChairsCount = chairsCount;
            this.Status = tableStatus;
            this.Reservations = reservations;
        }

        private const int MaxChairs = 4;

        public int Id { get; private set; }

        /// <summary>
        /// Number of chairs for this table.
        /// </summary>
        public int ChairsCount { get; private set; }

        /// <summary>
        /// Table status.
        /// </summary>
        public TableStatus Status { get; private set; }

        /// <summary>
        /// List of reservation for this table.
        /// </summary>
        public List<Reservation> Reservations { get; private set; }

        /// <summary>
        /// Change chairs number for table.
        /// </summary>
        /// <param name="numberOfChairs"> The value must be smaller then 5 and not smaller then 1. </param>
        public Table NumberOfChairs(int numberOfChairs)
        {
            switch (numberOfChairs)
            {
                case 0:
                    throw new NotValidNumber(nameof(this.ChairsCount), numberOfChairs);
                case 5:
                    throw new NotValidNumber(nameof(this.ChairsCount), numberOfChairs, MaxChairs);
                default:
                    this.ChairsCount = numberOfChairs;
                    break;
            }

            return this;

        }

        /// <summary>
        /// Change table status.
        /// </summary>
        /// <param name="status"> For define status for a table you can use TableStatus enum. </param>
        /// <returns></returns>
        public Table ChangeStatus(TableStatus status)
        {
            this.Status = status;
            return this;
        }
    }
}

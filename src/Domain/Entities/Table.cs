using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Exceptions;
using Domain.Common;

namespace Domain.Entities
{
    public class Table : AuditableEntity
    {
        public Table()
        : this(new List<Reservation>()) {}
        
        public Table(List<Reservation> reservationsList)
            => (Reservations) = (reservationsList);

        public Table(int chairsCount, TableStatus tableStatus)
            : this(new List<Reservation>())
            => (ChairsCount, Status) = (chairsCount, tableStatus);

        public Table(List<Reservation> reservations, int chairsCount, TableStatus tableStatus)
            : this(reservations)
            => (ChairsCount, Status) = (chairsCount, tableStatus);

        public const int MaxChairs = 4;

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
            _ = numberOfChairs switch
            {
                0 => throw new NotValidNumber(nameof(this.ChairsCount), numberOfChairs),
                5 => throw new NotValidNumber(nameof(this.ChairsCount), numberOfChairs, MaxChairs),
                _ => ChairsCount = numberOfChairs
            };

            return this;
        }

        /// <summary>
        /// Change table status.
        /// </summary>
        /// <param name="status"> "TableStatus" is an enum.</param>
        /// <returns></returns>
        public void ChangeStatus(TableStatus status)
            => this.Status = status;
         
    }
}

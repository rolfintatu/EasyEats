using AppCore.Dtos;
using AppCore.Reservation.Queries.ReservationDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Reservation.Queries.ReservationsList
{
    public class ReservationsList
    {
        public List<ComplexReservationDto> Reservations { get; set; }
    }
}

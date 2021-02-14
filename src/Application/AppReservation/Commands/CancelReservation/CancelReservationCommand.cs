using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AppReservation.Commands.CancelReservation
{
    public class CancelReservationCommand : IRequest<bool>
    {
        public CancelReservationCommand(Guid reservationId)
        {
            ReservationId = reservationId;
        }

        public Guid ReservationId { get; set; }

    }
}

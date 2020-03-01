using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Reservation.Commands.DeleteReservation
{
    public class CancelReservationValidator : AbstractValidator<CancelReservationCom>
    {
        public CancelReservationValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(0);
        }
    }
}

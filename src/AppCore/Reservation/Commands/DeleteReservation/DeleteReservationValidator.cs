using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace AppCore.Reservation.Commands.DeleteReservation
{
    public class DeleteReservationValidator : AbstractValidator<DeleteReservationCom>
    {
        public DeleteReservationValidator()
        {
            RuleFor(x => x.Id)
                .LessThanOrEqualTo(0);
        }
    }
}

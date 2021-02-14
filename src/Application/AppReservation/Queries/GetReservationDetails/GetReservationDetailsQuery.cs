using Application.Common.Dtos;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AppReservation.Queries.GetReservationDetails
{
    public class GetReservationDetailsQuery : IRequest<ReservationDetailsDto>
    {
        public Guid ReservationId { get; set; }
    }

    //public class GetReservationDetailsValidation : AbstractValidator<GetReservationDetailsQuery>
    //{
    //    public GetReservationDetailsValidation()
    //    {
    //        RuleFor(x => x.ReservationId)
    //            .NotEmpty()
    //            .NotEqual(default(Guid))
    //            .WithMessage("Please provide a valid Id.");
    //    }
    //}
}

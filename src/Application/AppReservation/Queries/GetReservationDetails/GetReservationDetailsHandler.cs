using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AppReservation.Queries.GetReservationDetails
{
    public class GetReservationDetailsHandler : IRequestHandler<GetReservationDetailsQuery, ReservationDetailsDto>
    {
        protected readonly IReservationRepository _repo;
        protected readonly IMapper _mapper;
        public GetReservationDetailsHandler(IReservationRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ReservationDetailsDto> Handle(GetReservationDetailsQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _repo.GetReservationById(request.ReservationId);
            var reservationDetails = _mapper.Map<ReservationDetailsDto>(reservation);

            return reservationDetails;
        }
    }
}

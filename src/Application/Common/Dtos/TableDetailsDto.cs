using Application.Common.Mapping;
using AutoMapper;
using System.Collections.Generic;
using Entities = Domain.Entities;

namespace Application.Common.Dtos
{
    public class TableDetailsDto : IMapFrom<Entities.Table>
    {

        public TableDetailsDto() { }

        public TableDetailsDto(int tableNumber, int chairCount) 
            => (this.TableNumber, this.ChairsCount) = (tableNumber, chairCount);

        public int TableNumber { get; set; }
        public int ChairsCount { get; set; }

        //public TableStatus Status { get; set; }

        private void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Table, TableDetailsDto>()
            .ForMember(x => x.TableNumber, x => x.MapFrom(o => o.Id));
        }

    }

    public class ComplexTableDto : TableDetailsDto
    {

        public ComplexTableDto() { }

        public ComplexTableDto(int tableNumber, int chairCount, List<ComplexReservationDto> reservations)
            : base(tableNumber, chairCount) 
            => (Reservations) = (reservations);

        public List<ComplexReservationDto> Reservations { get; set; }

        private void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Reservation, ComplexReservationDto>();
        }

    }

}

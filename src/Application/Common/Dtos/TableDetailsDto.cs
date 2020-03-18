using Domain.Enums;
using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
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

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Table, TableDetailsDto>()
            .ForMember(x => x.TableNumber, x => x.MapFrom(o => o.Id));
        }

    }

    public class ComplexTableDto : TableDetailsDto
    {

        public ComplexTableDto() { }

        public ComplexTableDto(int tableNumber, int chairCount, List<Entities.Reservation> reservations)
            : base(tableNumber, chairCount) 
            => (Reservations) = (reservations);

        public List<Entities.Reservation> Reservations { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Table, ComplexCustomerDto>();
        }

    }

}

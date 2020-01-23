using AppCore.Mapping;
using AppCore.ValueObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Customer.Queries.CustomerDetails
{
    public class CustomerDetails : IMapFrom<Entities.Customer>
    {
        public string Name { get; set; }
        public int Phone { get; set; }
        public Address Address { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Customer, CustomerDetails>();
        }
    }
}

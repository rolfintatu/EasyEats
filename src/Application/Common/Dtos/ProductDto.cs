using Application.Common.Mapping;
using AutoMapper;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos
{
    public class ProductDto : IMapFrom<Domain.Entities.Product>
    {

        public ProductDto(){}

        public ProductDto(string name
            , decimal price
            , int quantity
            , Category category
            , int calories
            , string description
            )
        {
            this.Name = name;
            this.Price = price;
            this.Category = category;
            this.Calories = calories;
            this.Description = description;
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public Category Category { get; private set; }
        public int Calories { get; private set; }
        public string Description { get; private set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductDto, Domain.Entities.Product>();
        }

    }
}

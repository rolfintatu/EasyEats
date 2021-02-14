using Application.Common.Mapping;
using AutoMapper;
using Domain.Aggregates.CatalogAggregate;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos
{
    public class ProductDto : IMapFrom<Product>
    {

        public ProductDto(){}

        public ProductDto(string name
            , decimal price
            , int quantity
            , Category category
            , int calories
            , string description
            ) =>
            (Name, Price, Quantity, Category, Calories, Description) =
            (name, price, quantity, category, calories, description);

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }
        public List<ImageDto> Images { get; set; }

        private void Mapping(Profile profile)
        {
            profile.CreateMap<ProductDto, Product>();
            profile.CreateMap<Product, ProductDto>();
        }

    }
}

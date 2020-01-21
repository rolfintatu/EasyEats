using AppCore.Enums;
using AppCore.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Food.Queries.FoodDetails
{
    public class FoodDetails : IMapFrom<Entities.Food>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Food, FoodDetails>();
        }
    }
}

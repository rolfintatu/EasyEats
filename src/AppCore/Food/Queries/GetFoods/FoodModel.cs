using AppCore.Enums;
using AppCore.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Food.Queries.GetFoods
{
    public class FoodModel : IMapFrom<Entities.Food>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Food, FoodModel>();
        }

    }
}

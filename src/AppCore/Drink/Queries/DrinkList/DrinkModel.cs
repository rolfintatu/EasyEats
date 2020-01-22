using AppCore.Enums;
using AppCore.Mapping;
using AutoMapper;
using System; 
using System.Collections.Generic;
using System.Text;

namespace AppCore.Drink.Queries.DrinkList
{
    public class DrinkModel : IMapFrom<Entities.Drink>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Calories { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Drink, DrinkModel>();
        }

    }
}

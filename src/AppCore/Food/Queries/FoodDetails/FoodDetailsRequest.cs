using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Food.Queries.FoodDetails
{
    public class FoodDetailsRequest : IRequest<FoodDetails>
    {
        public int Id { get; set; }
    }
}

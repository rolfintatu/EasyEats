using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ChangeProductPriceModel
    {
        public int ProductId { get; set; }
        public decimal NewPrice { get; set; }
    }
}

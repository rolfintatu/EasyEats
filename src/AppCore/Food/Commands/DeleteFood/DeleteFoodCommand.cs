using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Food.Commands.DeleteFood

{
    public class DeleteFoodCommand : IRequest
    {
        public int Id { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.OrderingAPI.Applications.Commands
{
    //Gelen command valid mi degil mi onun icin Generic class hazirladik cunku bu islemi birden fazla command icin kullanicaz
    public class IdentifiedCommand<T> : IRequest<bool> where T : IRequest<bool>
    {
        public T Command { get; }
        public int CustomerId { get; }
        public IdentifiedCommand(T command, int customerId)
        {
            Command = command;
            CustomerId = customerId;
        }
    }
}

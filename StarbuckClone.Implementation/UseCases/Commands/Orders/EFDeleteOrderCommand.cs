using StarbuckClone.Domain;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Commands.Orders;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.Orders
{
    public class EFDeleteOrderCommand : IDeleteOrderCommand
    {
        private readonly SCContext _context;


        public EFDeleteOrderCommand(SCContext context)
        {
            _context = context;


        }
        public int Id => 13;

        public string Name => "Soft delete order";

        public void Execute(DeleteOrderDto data)
        {
            var orderForRemoving = _context.Orders.Find(data.OrderId);
            if (orderForRemoving == null)
            {
                throw new NotFoundException(typeof(Order).ToString(), data.OrderId);
            }
            orderForRemoving.IsActive = false;
            _context.SaveChanges();
        }
    }
}

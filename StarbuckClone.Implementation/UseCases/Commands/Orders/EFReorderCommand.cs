using Microsoft.EntityFrameworkCore;
using StarbuckClone.Domain;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Commands.Orders;
using StarbucksClone.DataAccess;

namespace StarbuckClone.Implementation.UseCases.Commands.Orders
{
    public class EFReorderCommand : IReorderCommand
    {
        private readonly SCContext _context;


        public EFReorderCommand(SCContext context)
        {
            _context = context;
        }
        public int Id => 28;

        public string Name => "Reorder";

        public void Execute(int data)
        {

            var order = _context.Orders.Include(o => o.OrderLines).ThenInclude(ol => ol.OrderLineAddIns).FirstOrDefault(o=>o.Id==data);

            if (order == null)
            {
                throw new NotFoundException(typeof(Order).ToString(), data);
            }

            var newOrder = new Order
            {
                UserId = order.UserId,
                PickUpOption = order.PickUpOption,
                Address = order.Address,
                PaymentOption = order.PaymentOption,
                CardNumber = order.CardNumber,
                TotalPrice = order.TotalPrice,
                OrderLines = order.OrderLines.Select(ol => new OrderLine
                {
                    ProductId = ol.ProductId,
                    SizeVolume = ol.SizeVolume,
                    ProductSizeId = ol.ProductSizeId,
                    IsFavourite = false,
                    OrderLineAddIns = ol.OrderLineAddIns
                        .Where(addIn => addIn.OrderLineId == ol.Id)
                        .Select(addIn => new OrderLineAddIn
                        {
                            AddIn=addIn.AddIn,
                            AddInId = addIn.AddInId,
                            Pump = addIn.Pump,
                            AddInPrice = addIn.AddInPrice
                        }).ToList()
                }).ToList()
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();
        }
    }
}

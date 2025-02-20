using FluentValidation;
using StarbuckClone.Domain;
using StarbucksClone.Application;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Command.Orders;
using StarbucksClone.DataAccess;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;


namespace StarbuckClone.Implementation.UseCases.Commands.Orders
{
    public class EFCreateOrderCommand : ICreateOrderCommand
    {
        private SCContext _context;
        private IApplicationActor _actor;

        public EFCreateOrderCommand(SCContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;

        }
        public int Id => 9;

        public string Name => "Create order";

        public void Execute(CreateOrderDto data)
        {
            var validationFailures = new List<ValidationFailure>();

            if (string.IsNullOrEmpty(data.Address))
            {
                validationFailures.Add(new ValidationFailure(nameof(data.Address), "Address is required."));
                
            }
            else
            {
                if (data.Address.Length > 60)
                {
                    validationFailures.Add(new ValidationFailure(nameof(data.Address), "Address cannot exceed 60 characters."));
                }
            }
           
            if(!data.TotalPrice.HasValue)
            {
                validationFailures.Add(new ValidationFailure(nameof(data.TotalPrice), "Price is required."));
                if (data.TotalPrice <= 0)
                {
                    validationFailures.Add(new ValidationFailure(nameof(data.TotalPrice), "Price must be greater then 0."));
                }
            }
           

            if (validationFailures.Any())
            {
                throw new ValidationException(validationFailures);
            }



            var currentUserCartLines = _context.CartLines.Where(c => c.UserId == _actor.Id);

            if (!currentUserCartLines.Any())
            {
                throw new ConflictException("Your cart is empty");
            }

            Order newOrder = new Order
            {
                UserId = _actor.Id,
                PickUpOption=data.PickUpOption,
                Address = data.Address,
                PaymentOption=data.PaymentOption,
                CardNumber=data.CardNumber,
                TotalPrice = data.TotalPrice.Value,
                OrderLines = currentUserCartLines.Select(x => new OrderLine
                {
                    ProductId = x.ProductId,
                    SizeVolume = x.SizeVolume,
                    ProductSizeId= x.SizeId,
                    IsFavourite= x.IsFavourite,
                    OrderLineAddIns = x.CartLinesAddIns.Select(ca => new OrderLineAddIn
                    {
                        OrderLineId = x.Id,
                        AddInId = ca.AddInId,
                        AddIn = ca.AddIn.Name,
                        Pump = ca.Pump,
                        AddInPrice = ca.AddInPrice
                    }).ToList()

                }).ToList() 
        };



            _context.Orders.Add(newOrder);
            _context.CartLines.RemoveRange(currentUserCartLines);
            _context.SaveChanges();
        }
    }
}

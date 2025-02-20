using FluentValidation;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Command.CartLines;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.CartLines
{
    public class EFAddCartLineCommand : IAddCartLineCommand
    {
        private readonly SCContext _context;
        private readonly AddCartLineDtoValidator _validator;
        private readonly IApplicationActor _actor;

        public EFAddCartLineCommand(SCContext context, AddCartLineDtoValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }
        public int Id => 8;

        public string Name => "Add a product to a cart";

        public void Execute(AddCartLineDto data)
        {
            _validator.ValidateAndThrow(data);

            CartLine cartLineToAdd = new CartLine
            {
                UserId = _actor.Id,
                ProductId = data.ProductId,
                SizeVolume = _context.Sizes.Where(s => s.Id == data.SizeId).FirstOrDefault().Name,
                SizeId = data.SizeId,
               
                CartLinesAddIns = data.AddIns.Select(a => new CartLinesAddIn
                {
                    AddInId= a.Id,
                    Pump=a.Pump,
                    AddInPrice = a.Pump==null? _context.AddIns.Where(ad => ad.Id == a.Id).FirstOrDefault().Price : _context.AddIns.Where(ad => ad.Id == a.Id).FirstOrDefault().Price * a.Pump.Value
                }).ToList()
            };

            _context.CartLines.Add(cartLineToAdd);
            _context.SaveChanges();

        }
    }
}

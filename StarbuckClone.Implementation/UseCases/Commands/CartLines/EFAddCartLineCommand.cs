using FluentValidation;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Commands.CartLines;
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

        public string Name => "Add one product to a cart";

        public void Execute(AddCartLineDto data)
        {
            _validator.ValidateAndThrow(data);

            CartLine cartLineToAdd = new CartLine
            {
                UserId = _actor.Id,
                ProductId = data.ProductId,
                SizeVolume = _context.Sizes.Where(s => s.Id == data.SizeId).FirstOrDefault().Name,
               
                CartLinesAddIns = data.AddIns.Select(a => new CartLinesAddIn
                {
                    AddIn= _context.AddIns.Where(ad => ad.Id == a.Id).FirstOrDefault().Name,
                    Pump=a.Pump,
                    AddInPrice = a.Pump==0? _context.AddIns.Where(ad => ad.Id == a.Id).FirstOrDefault().Price : _context.AddIns.Where(ad => ad.Id == a.Id).FirstOrDefault().Price * a.Pump
                }).ToList()
            };

            _context.CartLines.Add(cartLineToAdd);
            _context.SaveChanges();

            var initialProductPrice = _context.Products.Where(p => p.Id == data.ProductId).FirstOrDefault().InitialPrice;
            var allProductAddInsPrice = _context.CartLinesAddIns.Where(cla => cla.CartLineId == cartLineToAdd.Id).Sum(x => x.AddInPrice);
            var newPrice = initialProductPrice + allProductAddInsPrice;

            var cartLineToModify = _context.CartLines.Find(cartLineToAdd.Id);

            cartLineToModify.Price = newPrice;

            _context.SaveChanges();
        }
    }
}

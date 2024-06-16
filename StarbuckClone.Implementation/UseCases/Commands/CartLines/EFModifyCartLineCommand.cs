using FluentValidation;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Commands.CartLines;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.CartLines
{
    public class EFModifyCartLineCommand : IModifyCartLineCommand
    {
        private readonly SCContext _context;
        private readonly ModifyCartLineDtoValidator _validator;
    

        public EFModifyCartLineCommand(SCContext context, ModifyCartLineDtoValidator validator)
        {
            _context = context;
            _validator = validator;
  
        }
        public int Id => 11;

        public string Name => "Change in-cart product options";

        public void Execute(ModifyCartLineDto data)
        {
            var cartLineForModification = _context.CartLines.Find(data.CartLineId);
            if (cartLineForModification == null)
            {
                throw new NotFoundException(typeof(CartLine).ToString(), data.CartLineId);
            }

            _validator.ValidateAndThrow(data);

            cartLineForModification.SizeVolume = _context.Sizes.Where(s => s.Id == data.SizeId).FirstOrDefault().Name;
            var cartLineAddInsForRemoval = _context.CartLinesAddIns.Where(c => c.CartLineId == data.CartLineId);

            _context.CartLinesAddIns.RemoveRange(cartLineAddInsForRemoval);

            cartLineForModification.CartLinesAddIns = data.AddIns.Select(a => new CartLinesAddIn
            {
                AddIn = _context.AddIns.Where(ad => ad.Id == a.Id).FirstOrDefault().Name,
                Pump = a.Pump,
                AddInPrice = a.Pump == null ? _context.AddIns.Where(ad => ad.Id == a.Id).FirstOrDefault().Price : _context.AddIns.Where(ad => ad.Id == a.Id).FirstOrDefault().Price * a.Pump.Value
            }).ToList();

            _context.SaveChanges();

            }

    


   
    }
}

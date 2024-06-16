using StarbuckClone.Domain;
using StarbuckClone.Implementation.Validators;
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
    public class EFDeleteCartLineDto : IDeleteCartLineCommand
    {
        private readonly SCContext _context;


        public EFDeleteCartLineDto(SCContext context)
        {
            _context = context;
          

        }
        public int Id => 12;

        public string Name => "Remove product from cart";

        public void Execute(DeleteCartLineDto data)
        {
            var cartLineForRemoving = _context.CartLines.Find(data.CartLineId);
            if (cartLineForRemoving == null)
            {
                throw new NotFoundException(typeof(CartLine).ToString(), data.CartLineId);
            }

            _context.CartLines.Remove(cartLineForRemoving);
            _context.SaveChanges();
            
        }
    }
}

using StarbuckClone.Domain;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Command.CartLines;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.CartLines
{
    public class EFToggleIsFavouriteCommand : IToggleIsFavouriteCommand
    {

        private readonly SCContext _context;

        public EFToggleIsFavouriteCommand(SCContext context)
        {
            _context = context;


        }
        public int Id => 26;

        public string Name => "Toggle product's is favourite option";

        public void Execute(int data)
        {
            var productToToggle = _context.CartLines.Find(data);
            if (productToToggle == null)
            {
                throw new NotFoundException(typeof(CartLine).ToString(), data);
            }

            productToToggle.IsFavourite = !productToToggle.IsFavourite;
            _context.SaveChanges();
        }
    }
}

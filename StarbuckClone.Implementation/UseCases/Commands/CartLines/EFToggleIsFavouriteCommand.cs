using StarbuckClone.Domain;
using StarbucksClone.Application.DTO;
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

        public void Execute(ToggleIsFavoriteDto data)
        {
            if (data.TableName == "CartLine") {
                var productToToggle = _context.CartLines.Find(data.ProductId);
                if (productToToggle == null)
                {
                    throw new NotFoundException(typeof(CartLine).ToString(), data.ProductId);
                }

                productToToggle.IsFavourite = !productToToggle.IsFavourite;
                _context.SaveChanges();
            }
            else if (data.TableName == "OrderLine")
            {
                var productToToggle = _context.OrderLines.Find(data.ProductId);
                if (productToToggle == null)
                {
                    throw new NotFoundException(typeof(OrderLine).ToString(), data.ProductId);
                }

                productToToggle.IsFavourite = !productToToggle.IsFavourite;
                _context.SaveChanges();
            }
        }
    }
}

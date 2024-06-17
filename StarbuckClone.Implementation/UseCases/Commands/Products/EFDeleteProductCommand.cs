using StarbuckClone.Domain;
using StarbuckClone.Implementation.Validators;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Commands.Products;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.Products
{
    public class EFDeleteProductCommand : IDeleteProductCommand
    {
        private SCContext _context;
    

        public EFDeleteProductCommand(SCContext context)
        {
            _context = context;
        

        }
        public int Id => 17;

        public string Name => "Soft delete product";

        public void Execute(int data)
        {
            var productForRemoving = _context.Products.Find(data);
            if (productForRemoving == null)
            {
                throw new NotFoundException(typeof(Product).ToString(), data);
            }
            productForRemoving.IsActive = false;
            _context.SaveChanges();
        }
    }
}

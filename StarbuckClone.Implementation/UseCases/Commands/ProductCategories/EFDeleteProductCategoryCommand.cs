using StarbuckClone.Domain;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Commands.ProductCategories;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Commands.ProductCategories
{
    public class EFDeleteProductCategoryCommand : IDeleteProductCategoryCommand
    {
        private SCContext _context;


        public EFDeleteProductCategoryCommand(SCContext context)
        {
            _context = context;


        }
        public int Id =>24;

        public string Name => "Soft delete product category";

        public void Execute(int data)
        {
            var productCatForRemoving = _context.ProductCategories.Find(data);
            if (productCatForRemoving == null)
            {
                throw new NotFoundException(typeof(ProductCategory).ToString(), data);
            }
            productCatForRemoving.IsActive = false;
            _context.SaveChanges();
        }
    }
}

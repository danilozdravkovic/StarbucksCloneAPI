using Microsoft.EntityFrameworkCore;
using StarbuckClone.Domain;
using StarbucksClone.Application;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Queries.CartLines;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StarbuckClone.Implementation.UseCases.Queries.CartLInes
{
    public class EFGetProductFromCartQuery : IGetProductFromCartQuery
    {
        
        public readonly SCContext _context;

        public EFGetProductFromCartQuery(SCContext context)
        {
            _context = context;
        }
        public int Id => 15;
        public string Name => "Get single product from cart";

        public SingleCartLineDto Execute(IDCartLineDto search)
        {
            var cartLine = _context.CartLines.Include(c => c.CartLinesAddIns).ThenInclude(cl=>cl.AddIn).FirstOrDefault(c=>c.Id==search.CartLineId);
            if (cartLine == null)
            {
                throw new NotFoundException(typeof(CartLine).ToString(), search.CartLineId);
            }
            var initialCartLineAddIns = new List<AddIn>();
            var cartLineAddIns = new List<AddIn>();


            foreach (var cartLineAddIn in cartLine.CartLinesAddIns)
            {
                initialCartLineAddIns.Add(cartLineAddIn.AddIn);
                var rootParent = GetRootParent(cartLineAddIn.AddIn);
                FillChildCategories(rootParent);
                AddTopLevelAddIn(rootParent, cartLineAddIns);
            }


            var addInsForInterface = cartLineAddIns.Select(x => new GettingAddInsForInterface
            {
                Name = x.Name,
                Id = x.Id,
                Children= TransformAddIns(x.Children,initialCartLineAddIns),

            });

            return new SingleCartLineDto
            {
                Size = cartLine.SizeVolume,
                AddIns= addInsForInterface
            };
        }

        private void FillChildCategories(AddIn addIn)
        {
            int id = addIn.Id;

            addIn.Children = _context.AddIns.Where(x => x.ParentId == id).Select(c => new AddIn
            {
                Id=c.Id,
                Name = c.Name,
            }).ToList();


            foreach (AddIn child in addIn.Children)
            {
                FillChildCategories(child);
            }
        }

        private AddIn GetRootParent(AddIn addIn)
        {
            _context.Entry(addIn).Reference(a => a.Parent).Load();
            if (addIn.Parent == null)
            {
                return addIn;
            }

            return GetRootParent(addIn.Parent);
        }

        private void AddTopLevelAddIn(AddIn addIn, List<AddIn> topLevelAddIns)
        {
            if (addIn.Parent == null)
            {
                topLevelAddIns.Add(addIn);
                return;
            }

            AddTopLevelAddIn(addIn.Parent, topLevelAddIns);
        }

        public static IEnumerable<GettingAddInsForInterface> TransformAddIns(IEnumerable<AddIn> addIns, List<AddIn> initialCartLineAddIns)
        {
            if (addIns == null || !addIns.Any())
            {
                return Enumerable.Empty<GettingAddInsForInterface>();
            }

            return addIns.Select(x => new GettingAddInsForInterface
            {
                Id = x.Id,
                Name = x.Name,
                Selected=initialCartLineAddIns.Any(init => init.Id==x.Id),
                Children = TransformAddIns(x.Children, initialCartLineAddIns)
            });
        }

    }
}

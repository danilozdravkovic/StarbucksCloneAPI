using StarbuckClone.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.Application.DTO
{
    public class AddCartLineDto
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public IEnumerable<AddingAddInForCartDto> AddIns { get; set; }
    }

    public class AddingAddInForCartDto
    {
        public int Id { get; set; }
        public int? Pump { get; set; }
    }

    public class CartLineDto
    {
        public int CartLineId { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; } 
        public string ProductSize { get; set; }
        public bool isFavourite { get; set; }

        public decimal ProductPrice { get; set; }

        public IEnumerable<GetingAddInForCartDto> AddIns { get; set; }
    }

    public class GetingAddInForCartDto
    {
        public string AddInName { get; set; }
        public int? Pump { get; set; }
    }

    public class ModifyCartLineDto
    {
        public int CartLineId { get; set; }
        public int SizeId { get; set; }

        public IEnumerable<AddingAddInForCartDto> AddIns { get; set; }

    }


    public class SingleCartLineDto
    {
        public string Size { get; set; }
        
        public IEnumerable<GettingAddInsForInterface> AddIns { get; set; }
    }

    public class GettingAddInsForInterface
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public bool Selected { get; set; }
        public IEnumerable<GettingAddInsForInterface> Children { get; set; }
    }

}

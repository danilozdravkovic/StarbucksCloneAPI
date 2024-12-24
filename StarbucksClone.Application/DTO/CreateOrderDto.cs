using StarbuckClone.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.Application.DTO
{
    public class CreateOrderDto
    { 
        public string Address { get; set; }

        public decimal? TotalPrice { get; set; }
        public string PickUpOption { get; set; }
        public string PaymentOption { get; set; }
        public string CardNumber { get; set; }
    }


    public class SearchOrderDto : PagedSearchDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class OrderDto
    {
        public int OrderId { get; set;}
        public string UserUserName { get; set; }
        public string UserEmail { get; set; }

        public IEnumerable<OrderProductDto> Products { get; set; }
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal TotalPrice { get; set; }


    }

    public class OrderProductDto
    {
        public string Name { get; set; }
        public IEnumerable<GetingAddInForCartDto> AddIns { get; set; }
    }
}

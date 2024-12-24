using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class OrderLine : Entity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string SizeVolume { get; set; }

        public bool IsFavourite { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<OrderLineAddIn> OrderLineAddIns { get; set; }
    }
}

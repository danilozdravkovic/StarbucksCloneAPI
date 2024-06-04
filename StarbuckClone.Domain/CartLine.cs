using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class CartLine : Entity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string SizeVolume { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<CartLinesAddIn> CartLinesAddIns { get; set; } = new HashSet<CartLinesAddIn>();
    }
}

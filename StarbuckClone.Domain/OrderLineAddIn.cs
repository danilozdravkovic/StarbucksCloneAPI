using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class OrderLineAddIn : Entity
    {
        public int OrderLineId { get; set; }
        public string AddIn { get; set; }
        public int? Pump { get; set; }

        public virtual OrderLine OrderLine { get; set; }
    }
}

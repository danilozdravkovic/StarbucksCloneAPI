using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class IncludedProductAddIn : Entity
    {
        public int AddInId { get;set; }
        public int ProductId { get;set; }

        public int? Pump { get; set; }
        public int? SelectedId { get; set; }

        public virtual AddIn AddIn { get; set; } 
        public virtual Product Product { get; set; }
        public virtual AddIn SelectedAddIn { get; set; }
    }
}

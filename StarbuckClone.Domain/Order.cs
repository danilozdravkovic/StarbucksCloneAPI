﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Domain
{
    public class Order : Entity
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }

        public string PickUpOption { get; set; }

        public string PaymentOption { get; set; }

        public string CardNumber { get; set; }

        public virtual User User {get;set;}
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new HashSet<OrderLine>();
    }

}

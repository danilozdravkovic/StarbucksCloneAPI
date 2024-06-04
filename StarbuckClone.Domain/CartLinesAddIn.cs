namespace StarbuckClone.Domain
{
    public class CartLinesAddIn : Entity
    {
        public int CartLineId { get; set; }
        public string AddIn { get; set; }
        public int? Pump { get; set; }

        public virtual CartLine CartLine { get; set; }
    }
}

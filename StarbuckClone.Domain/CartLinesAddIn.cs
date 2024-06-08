namespace StarbuckClone.Domain
{
    public class CartLinesAddIn
    {
        public int Id { get; set; }
        public int CartLineId { get; set; }
        public string AddIn { get; set; }
        public int Pump { get; set; }
        public decimal AddInPrice { get; set; }

        public virtual CartLine CartLine { get; set; }
    }
}

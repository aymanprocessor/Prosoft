namespace Shared
{
    public class Filter
    {
        public string? SearchByItemCode { get; set; } = string.Empty;
        public string? SearchByItemName { get; set; } = string.Empty;
        public string? FromCode { get; set; } = string.Empty;
        public string? ToCode { get; set; } = string.Empty;
        public DateTime? FromDate { get; set; }
      
        public DateTime? ToDate { get; set; }
        public int? SupplierId {  get; set; }

        public bool? NagativeQty { get; set; }
        public bool? ZeroQty { get; set; }
        public bool? PositiveQty { get; set; }


    }
}

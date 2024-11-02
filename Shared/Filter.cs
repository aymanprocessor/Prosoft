namespace Shared
{
    public class Filter
    {
        public string? SearchByItemCode { get; set; } = string.Empty;
        public string? SearchByItemName { get; set; } = string.Empty;
        public string? FromCode { get; set; } = string.Empty;
        public string? ToCode { get; set; } = string.Empty;
        public int? SupplierId {  get; set; }

    }
}

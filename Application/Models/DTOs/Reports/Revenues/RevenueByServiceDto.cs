namespace Application.Models.DTOs.Reports.Revenues
{
    public record RevenueByServiceDto
    {
        public string ServiceName { get; set; }
        public decimal Revenue { get; set; }
    }
}

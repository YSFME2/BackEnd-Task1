namespace Application.Models.DTOs.Reports
{
    public record RevenueByServiceDto
    {
        public string ServiceName { get; set; }
        public decimal Revenue { get; set; }
    }
}

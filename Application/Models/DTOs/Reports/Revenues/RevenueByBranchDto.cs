namespace Application.Models.DTOs.Reports.Revenues
{
    public record RevenueByBranchDto
    {
        public string BranchName { get; set; }
        public decimal Revenue { get; set; }
    }
}

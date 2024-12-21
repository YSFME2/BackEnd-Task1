namespace Application.Models.DTOs.Reports
{
    public record RevenueByBranchDto
    {
        public string BranchName { get; set; }
        public decimal Revenue { get; set; }
    }
}

namespace Application.Models.DTOs.Reports
{
    public record RevenueByPaymentMethodDto
    {
        public string PaymentMethod { get; set; }
        public decimal Revenue { get; set; }
    }
}

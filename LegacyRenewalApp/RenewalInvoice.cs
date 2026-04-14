using System;

public class RenewalInvoice
{
    public int CustomerId { get; set; }
    
    public string CustomerName { get; set; }
    
    public int InvoiceNumber { get; set; }
    
    public decimal FinalAmount { get; set; }
    public string PlanCode { get; set; } = string.Empty;

    public decimal BaseAmount { get; set; }
    public decimal Discount { get; set; }
    public decimal SupportFee { get; set; }
    public decimal PaymentFee { get; set; }
    public decimal Tax { get; set; }
    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public override string ToString()
    {
        return $"CustomerId={CustomerId}, Plan={PlanCode}, Total={TotalAmount:F2}";
    }
}
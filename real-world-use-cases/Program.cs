using real_world_use_cases;


// Generate certificate
CertificateGenerator.GenerateCertificate(new CertificateData
{
    RecipientName = "Jane Smith",
    CourseName = "Advanced C# Programming",
    CompletionDate = DateTime.Now,
    InstructorName = "Tech Academy"
});

// Create invoice data with items using collection initializer
InvoiceData invoiceData = new InvoiceData
{
    CompanyName = "Adventure Cycle Works",
    CompanyAddress = "Plot No. 18, Trailblazer Industrial Estate, Mountain View Road,",
    InvoiceNumber = "INV-1001",
    InvoiceDate = DateTime.Now,
    CustomerName = "John Doe",
    CustomerAddress = "456 Elm Street, Springfield, IL 62704",
    Items =
    [
        new InvoiceItem { Description = "Web Development Services", Quantity = 1, Rate = 1500.00m },
        new InvoiceItem { Description = "Hosting Services", Quantity = 1, Rate = 200.00m },
        new InvoiceItem { Description = "Domain Registration", Quantity = 1, Rate = 15.00m },
        new InvoiceItem { Description = "SSL Certificate", Quantity = 1, Rate = 100.00m },
        new InvoiceItem { Description = "Maintenance Services", Quantity = 1, Rate = 300.00m }
    ]
};

// Generate the invoice
InvoiceGenerator.GenerateBusinessInvoice(invoiceData);

using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace real_world_use_cases
{
    public class InvoiceGenerator
    {
        public static void GenerateBusinessInvoice(InvoiceData invoiceData)
        {
            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.Pages.Add();
                PdfGraphics graphics = page.Graphics;

                // Use using to ensure the font stream is disposed
                using (FileStream fontStream = new FileStream("../../../../data/arial.ttf", FileMode.Open, FileAccess.Read))
                {
                    // Define fonts for different elements
                    PdfTrueTypeFont companyFont = new PdfTrueTypeFont(fontStream, 20, PdfFontStyle.Bold);
                    PdfTrueTypeFont headerFont = new PdfTrueTypeFont(companyFont, 14);
                    PdfTrueTypeFont labelFont = new PdfTrueTypeFont(companyFont, 10);
                    PdfTrueTypeFont valueFont = new PdfTrueTypeFont(fontStream, 10, PdfFontStyle.Regular);
                    PdfTrueTypeFont totalFont = new PdfTrueTypeFont(companyFont, 12);

                    float yPos = 20;
                    float pageWidth = page.GetClientSize().Width;

                    // Company header
                    graphics.DrawString(invoiceData.CompanyName, companyFont, PdfBrushes.DarkBlue, new PointF(10, yPos));
                    yPos += 30;

                    graphics.DrawString(invoiceData.CompanyAddress, valueFont, PdfBrushes.Black, new PointF(10, yPos));
                    yPos += 40;

                    // Invoice title
                    graphics.DrawString("INVOICE", headerFont, PdfBrushes.Black, new PointF(pageWidth - 80, 20));

                    // Invoice details
                    graphics.DrawString($"Invoice #: {invoiceData.InvoiceNumber}", valueFont, PdfBrushes.Black, new PointF(pageWidth - 100, 50));
                    graphics.DrawString($"Date: {invoiceData.InvoiceDate:MM/dd/yyyy}", valueFont, PdfBrushes.Black, new PointF(pageWidth - 100, 70));

                    // Customer information
                    graphics.DrawString("Bill To:", labelFont, PdfBrushes.Black, new PointF(10, yPos));
                    yPos += 20;
                    graphics.DrawString(invoiceData.CustomerName, valueFont, PdfBrushes.Black, new PointF(10, yPos));
                    yPos += 15;
                    graphics.DrawString(invoiceData.CustomerAddress, valueFont, PdfBrushes.Black, new PointF(10, yPos));
                    yPos += 40;

                    // Items table
                    PdfGrid itemGrid = new PdfGrid
                    {
                        DataSource = invoiceData.Items
                    };
                    itemGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent5);

                    // Create and reuse string format
                    PdfStringFormat stringFormat = new PdfStringFormat
                    {
                        Alignment = PdfTextAlignment.Center,
                        LineAlignment = PdfVerticalAlignment.Middle
                    };

                    // Set column widths and formats in a loop for maintainability
                    float[] columnWidths = { 265, 70, 85, 85 };
                    for (int i = 0; i < itemGrid.Columns.Count; i++)
                    {
                        itemGrid.Columns[i].Width = columnWidths[i];
                        itemGrid.Columns[i].Format = stringFormat;
                    }

                    // Set row height and font for all rows
                    foreach (PdfGridRow row in itemGrid.Rows)
                    {
                        row.Height = 23;
                        row.Style.Font = valueFont;
                    }

                    // Set header row height, font, and format
                    var header = itemGrid.Headers[0];
                    header.Height = 30;
                    header.Style.Font = labelFont;
                    foreach (PdfGridCell cell in header.Cells)
                    {
                        cell.StringFormat = stringFormat;
                    }

                    PdfGridLayoutResult result = itemGrid.Draw(page, new PointF(10, yPos));

                    decimal total = invoiceData.Items.Sum(item => item.Amount);
                    graphics.DrawString("Total:", totalFont, PdfBrushes.Black, new PointF(370, result.Bounds.Bottom + 10));
                    graphics.DrawString($"${total:F2}", totalFont, PdfBrushes.DarkBlue, new PointF(440, result.Bounds.Bottom + 10));

                    document.Save($"Invoice_{invoiceData.InvoiceNumber}.pdf");
                }
                // PdfDocument is disposed automatically
            }       
        }
    }
    public class InvoiceData
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }

    public class InvoiceItem
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount => Quantity * Rate;
    }
}

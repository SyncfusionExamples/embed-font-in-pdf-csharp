using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

namespace real_world_use_cases
{
    public class CertificateGenerator
    {
        public static void GenerateCertificate(CertificateData certData)
        {
            // Use using statements for proper resource management
            using (PdfDocument document = new PdfDocument())
            {
                //Set the document orientation to landscape
                document.PageSettings.Orientation = PdfPageOrientation.Landscape;

                //Add a page to the document
                PdfPage page = document.Pages.Add();

                // Get the graphics object of the page
                PdfGraphics graphics = page.Graphics;

                //Get the page width and height
                float pageWidth = page.GetClientSize().Width;
                float pageHeight = page.GetClientSize().Height;

                // Use using for font streams to ensure disposal
                using (FileStream timesFontStream = new FileStream("../../../../data/times.ttf", FileMode.Open, FileAccess.Read))
                using (FileStream brushScriptFontStream = new FileStream("../../../../data/BRUSHSCI.ttf", FileMode.Open, FileAccess.Read))
                {
                    // Certificate fonts
                    PdfFont titleFont = new PdfTrueTypeFont(timesFontStream, 28, PdfFontStyle.Bold);
                    PdfFont subtitleFont = new PdfTrueTypeFont(timesFontStream, 18, PdfFontStyle.Italic);
                    PdfFont nameFont = new PdfTrueTypeFont(brushScriptFontStream, 32, PdfFontStyle.Regular);
                    PdfFont bodyFont = new PdfTrueTypeFont(timesFontStream, 14, PdfFontStyle.Regular);
                    PdfFont signatureFont = new PdfTrueTypeFont(timesFontStream, 12, PdfFontStyle.Regular);

                    // Certificate border
                    graphics.DrawRectangle(PdfPens.DarkBlue, new RectangleF(20, 20, pageWidth - 40, pageHeight - 40));
                    graphics.DrawRectangle(PdfPens.DarkBlue, new RectangleF(25, 25, pageWidth - 50, pageHeight - 50));

                    float yPos = 80;

                    // Helper function for centered text
                    void DrawCentered(string text, PdfFont font, PdfBrush brush, ref float y, float yOffset = 0)
                    {
                        SizeF size = font.MeasureString(text);
                        graphics.DrawString(text, font, brush, new PointF((pageWidth - size.Width) / 2, y));
                        y += size.Height + yOffset;
                    }

                    // Certificate title
                    DrawCentered("CERTIFICATE OF COMPLETION", titleFont, PdfBrushes.DarkBlue, ref yPos, 32);

                    // Subtitle
                    DrawCentered("This is to certify that", subtitleFont, PdfBrushes.Black, ref yPos, 22);

                    // Recipient name
                    DrawCentered(certData.RecipientName, nameFont, PdfBrushes.DarkRed, ref yPos, 18);

                    // Achievement description
                    DrawCentered("has successfully completed the course", bodyFont, PdfBrushes.Black, ref yPos, 8);

                    // Course name
                    DrawCentered(certData.CourseName, bodyFont, PdfBrushes.DarkBlue, ref yPos, 18);

                    // Date
                    string dateText = $"on {certData.CompletionDate:MMMM dd, yyyy}";
                    DrawCentered(dateText, bodyFont, PdfBrushes.Black, ref yPos, 48);

                    // Signature line
                    graphics.DrawLine(PdfPens.Black, new PointF(pageWidth - 200, yPos), new PointF(pageWidth - 50, yPos));
                    yPos += 20;

                    // Signature text
                    graphics.DrawString("Authorized Signature", signatureFont, PdfBrushes.Black, new PointF(pageWidth - 180, yPos));

                    document.Save($"Certificate_{certData.RecipientName.Replace(" ", "_")}.pdf");
                }
            }
        }
    }

    public class CertificateData
    {
        public string RecipientName { get; set; }
        public string CourseName { get; set; }
        public DateTime CompletionDate { get; set; }
        public string InstructorName { get; set; }
    }
}

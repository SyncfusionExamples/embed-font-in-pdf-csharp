using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

//Create a new PDF document
using (PdfDocument document = new PdfDocument())
{
    //Add a page to the document
    PdfPage page = document.Pages.Add();
    //Create a font
    PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
    //Draw text on the page
    page.Graphics.DrawString("Hello, PDF with Standard Font!", font, PdfBrushes.Black, new PointF(10, 10));
    //Save the document
    document.Save("embed-standard-font.pdf");
}

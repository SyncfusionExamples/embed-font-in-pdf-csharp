using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

//Load the custom font file
using (FileStream fontStream = new FileStream("../../../../data/arial.ttf", FileMode.Open, FileAccess.Read))
{
    //Create a PDF font using the font stream.
    PdfFont font = new PdfTrueTypeFont(fontStream, 12);

    //Create a PDF document
    using (PdfDocument document = new PdfDocument())
    {
        //Add page
        PdfPage page = document.Pages.Add();       
        //Draw text using the arial font
        page.Graphics.DrawString("Text with Embedded TTF Font", font, PdfBrushes.Black, new PointF(10, 10));
        //Save the document
        document.Save("embed-custom-ttf-font.pdf");
    }
}


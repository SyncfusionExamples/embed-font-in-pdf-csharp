using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

//Create a new PDF document
using (PdfDocument document = new PdfDocument())
{
    //Add a page to the document
    PdfPage page = document.Pages.Add();

    //Create string format and enable complex script support
    PdfStringFormat format = new PdfStringFormat() { ComplexScript = true };

    //Load a TrueType font for Thai language    
    PdfTrueTypeFont thaiUnicodeFont = new PdfTrueTypeFont(new FileStream("../../../../data/tahoma.ttf", FileMode.Open), 14);

    //Draw the Thai text
    page.Graphics.DrawString("สวัสดีชาวโลก", thaiUnicodeFont, PdfBrushes.Black, new RectangleF(0, 130, 300, 50), format);

    //Create Unicode font for Indic language
    PdfTrueTypeFont indicUnicodeFont = new PdfTrueTypeFont(new FileStream("../../../../data/NotoSansTamil-Regular.ttf", FileMode.Open), 14);

    //Draw the Indic text
    page.Graphics.DrawString("வணக்கம் உலகம்", indicUnicodeFont, PdfBrushes.Black, new RectangleF(0, 180, 300, 50), format);

    //Load a Unicode font for Arabic language
    PdfTrueTypeFont arabicUnicodeFont = new PdfTrueTypeFont(new FileStream("../../../../data/arial.ttf", FileMode.Open), 14);

    //Set the text direction for right to left languages
    format.TextDirection = PdfTextDirection.RightToLeft;

    page.Graphics.DrawString("مرحبا بالعالم", arabicUnicodeFont, PdfBrushes.Black, new RectangleF(0, 230, 300, 50), format);

    //Save the document
    document.Save("complex-script-text-in-pdf.pdf");
}


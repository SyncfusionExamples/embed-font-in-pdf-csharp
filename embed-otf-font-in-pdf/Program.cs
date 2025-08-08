using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

//Text to draw         
string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

//Create a new PDF document
using (PdfDocument document = new PdfDocument())
{
    //Add a page
    PdfPage page = document.Pages.Add();

    //Create font
    using FileStream fontFileStream = new FileStream("../../../../data/NotoSerif-Black.otf", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    PdfFont font = new PdfTrueTypeFont(fontFileStream, 14);

    //Get the page client size 
    SizeF clipBounds = page.Graphics.ClientSize;
    //Create rect to draw text
    RectangleF rect = new RectangleF(0, 0, clipBounds.Width, clipBounds.Height);
    //Draw text.
    page.Graphics.DrawString(text, font, PdfBrushes.Black, rect);

    //Save the document to a file
    document.Save("embed-open-type-font-in-pdf.pdf");
    document.Close(true);
}

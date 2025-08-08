using SkiaSharp;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;

//Load the PDF document. 
using (PdfLoadedDocument loadedDocument = new PdfLoadedDocument("../../../../data/input.pdf"))
{

    //Subscribe to the SubstituteFont event to supply the required font.
    loadedDocument.SubstituteFont += LoadedDocument_SubstituteFont;

    //Create a PdfConformance option
    PdfConformanceOptions pdfConformanceOptions = new PdfConformanceOptions
    {
        // Set the conformance level to PDF/A-1B.
        ConformanceLevel = PdfConformanceLevel.Pdf_A1B,
        // Set the option to subset the embedded fonts.
        SubsetFonts = true
    };

    //Convert the PDF document to PDF/A-1B format to embed the fonts.
    loadedDocument.ConvertToPDFA(pdfConformanceOptions);

    //Save the PDF document to file.
    loadedDocument.Save("embed-fonts-in-existing-pdf.pdf");

    //Close the document.
    loadedDocument.Close(true);
}

void LoadedDocument_SubstituteFont(object sender, PdfFontEventArgs args)
{
    //Get the font name.
    string fontName = args.FontName.Split(',')[0];

    //Map PdfFontStyle to SKFontStyle
    SKFontStyle sKFontStyle = args.FontStyle switch
    {
        PdfFontStyle.Bold => SKFontStyle.Bold,
        PdfFontStyle.Italic => SKFontStyle.Italic,
        PdfFontStyle.Bold | PdfFontStyle.Italic => SKFontStyle.BoldItalic,
        _ => SKFontStyle.Normal
    };

    using SKTypeface typeface = SKTypeface.FromFamilyName(fontName, sKFontStyle);
    if (typeface == null)
    {
        args.FontStream = null;
        return;
    }

    using SKStreamAsset typeFaceStream = typeface.OpenStream();
    if (typeFaceStream == null || typeFaceStream.Length == 0)
    {
        args.FontStream = null;
        return;
    }

    byte[] fontData = new byte[typeFaceStream.Length];
    int bytesRead = typeFaceStream.Read(fontData, fontData.Length);
    if (bytesRead != fontData.Length)
    {
        args.FontStream = null;
        return;
    }

    //Create a new memory stream from the font data.
    args.FontStream = new MemoryStream(fontData);
}
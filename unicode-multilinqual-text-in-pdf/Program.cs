using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;


using (PdfDocument document = new PdfDocument())
{
    PdfPage page = document.Pages.Add();
    PdfGraphics graphics = page.Graphics;

    // Create fonts that support Unicode
    PdfTrueTypeFont unicodeFont = new PdfTrueTypeFont(new FileStream("../../../../data/arial.ttf", FileMode.Open), 14);

    // Ensure the font supports a wide range of characters
    PdfSolidBrush brush = new PdfSolidBrush(Color.Black);

    float yPosition = 50;

    // English text
    graphics.DrawString("English: Hello World!", unicodeFont, brush, new PointF(10, yPosition));
    yPosition += 30;

    // Spanish text with accents
    graphics.DrawString("Español: ¡Hola Mundo! Niño, José, María", unicodeFont, brush, new PointF(10, yPosition));
    yPosition += 30;

    // French text with accents
    graphics.DrawString("Français: Bonjour le Monde! Café, Naïve, Façade", unicodeFont, brush, new PointF(10, yPosition));
    yPosition += 30;

    // German text with umlauts
    graphics.DrawString("Deutsch: Hallo Welt! Müller, Größe, Weiß", unicodeFont, brush, new PointF(10, yPosition));
    yPosition += 30;

    // Russian text (Cyrillic)
    graphics.DrawString("Русский: Привет Мир! Москва, Россия", unicodeFont, brush, new PointF(10, yPosition));
    yPosition += 30;

    // Mathematical symbols
    graphics.DrawString("Mathematics: ∑, ∞, α, β, γ, π, Ω, ∆", unicodeFont, brush, new PointF(10, yPosition));

    using (FileStream stream = new FileStream("multilingual-pdf.pdf", FileMode.Create))
    {
        document.Save(stream);
    }
}

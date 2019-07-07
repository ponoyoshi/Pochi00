using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Subtitles;

public class FontLibrary
{
    private StoryboardObjectGenerator generator;
    private ProjectFont[] fontLibrary;
    public FontLibrary(StoryboardObjectGenerator generator)
    {
        this.generator = generator;
        SetFontLibrary();
    }
    public FontGenerator GetFont(string fontName)
    {
        foreach(var font in fontLibrary)
        {
            if(font.fontName == fontName)
                return font.font;
        }
        generator.Log($"!!! Font {fontName} Not Found !!!");
        return null;
    }
    private void SetFontLibrary()
    {
        ProjectFont[] library = new ProjectFont[]
        {
            SetFont("Bold"),
            SetFont("BoldItalic"),
            SetFont("Italic"),
            SetFont("Light"),
            SetFont("LightItalic"),
            SetFont("Medium"),
            SetFont("MediumItalic"),
            SetFont("Regular"),
            SetFont("SemiBold"),
            SetFont("SemiBoldItalic")
        };
        fontLibrary = library;
    }
    private ProjectFont SetFont(string fontName)
    {
        var font = generator.LoadFont($"sb/f/{fontName}", new FontDescription
        {
            FontPath = $"font/CormorantInfant-{fontName}.ttf",
            FontSize = 100,
            FontStyle = System.Drawing.FontStyle.Regular,
            Color = Color4.White
        });
        return new ProjectFont(font, fontName);
    }
    private class ProjectFont
    {
        public FontGenerator font;
        public string fontName;

        public ProjectFont(FontGenerator font, string fontName)
        {
            this.font = font;
            this.fontName = fontName;
        }
    }
}
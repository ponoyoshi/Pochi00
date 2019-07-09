using System;
using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;

public class TextManager
{
    private StoryboardObjectGenerator generator;
    private FontLibrary fontlibrary;
    public TextManager(StoryboardObjectGenerator generator) 
    {
        this.generator = generator;
    } 

    public void GenerateTextVerticlalLetter(string text, int startTime, int endTime, Vector2 position, float scale, string fontStyle = "Regular")
    {
        if(fontlibrary == null)
            fontlibrary = new FontLibrary(generator);

        var font = fontlibrary.GetFont(fontStyle);
        SentenceOptions SentenceOptions = new SentenceOptions(font, text, scale);

        int delay = 0;

        float letterX = position.X - SentenceOptions.Width/2;
        float letterY = position.Y - SentenceOptions.Height/2;

        foreach(var letter in text)
        {
            var texture = font.GetTexture(letter.ToString());
            
            var letterPosition = new Vector2(letterX, letterY)
                + texture.OffsetFor(OsbOrigin.Centre) *scale;

            
            var sprite = generator.GetLayer("TEXT").CreateSprite(texture.Path, OsbOrigin.Centre, letterPosition);
            sprite.Fade(startTime + delay, startTime + delay + 1000, 0, 1);
            sprite.Fade(endTime + delay, endTime + delay + 300, 1, 0);
            sprite.Scale(startTime, scale);
            sprite.MoveY(OsbEasing.OutExpo, startTime + delay, startTime + delay + 1000, letterPosition.Y + generator.Random(-30, 30), letterPosition.Y);

            delay += 100;
            letterX += texture.BaseWidth * scale;
        }
    }
    public void GenerateRotatingText(string text, int startTime, int endTime, Vector2 position, float scale, string fontStyle = "Regular")
    {
        if(fontlibrary == null)
            fontlibrary = new FontLibrary(generator);

        var font = fontlibrary.GetFont(fontStyle);
        SentenceOptions SentenceOptions = new SentenceOptions(font, text, scale);
        int delay = 0;
        float letterX = position.X - SentenceOptions.Width/2;
        float letterY = position.Y - SentenceOptions.Height/2;
        int duration = endTime - startTime;

        foreach(var letter in text)
        {
            
            var texture = font.GetTexture(letter.ToString());
            generator.Log(texture.Path);
            if(!texture.IsEmpty)
            {
                var letterPosition = new Vector2(letterX, letterY)
                + texture.OffsetFor(OsbOrigin.Centre) *scale;
           
                var sprite = generator.GetLayer("TEXT").CreateSprite(texture.Path, OsbOrigin.Centre, letterPosition);
                sprite.StartLoopGroup(startTime + delay, duration/10000);
                sprite.MoveX(OsbEasing.InOutSine, 0, 5000, position.X + SentenceOptions.Width, position.X - SentenceOptions.Width);
                sprite.MoveX(OsbEasing.InOutSine, 5000, 10000, position.X + SentenceOptions.Width, position.X - SentenceOptions.Width);
                sprite.Fade(OsbEasing.InOutSine, 0, 1000, 0, 1);
                sprite.Fade(OsbEasing.InOutSine, 1000, 4000, 1, 1);
                sprite.Fade(OsbEasing.InOutSine, 4000, 5000, 1, 0);
                sprite.ScaleVec(OsbEasing.InOutSine, 0, 2500, 0, scale, scale, scale);
                sprite.ScaleVec(OsbEasing.InOutSine, 2500, 5000, scale, scale, 0, scale);
                sprite.EndGroup();
            }        
            letterX += texture.BaseWidth * scale;
            delay += 300;
        }
    }
    private class SentenceOptions
    {
        public float Width;
        public float Height;

        public SentenceOptions(FontGenerator font, string text, float scale)
        {
            float width = 0;
            float height = 0;
            foreach(var letter in text)
            {
                var texture = font.GetTexture(letter.ToString());
                width += texture.BaseWidth * scale;
                height = Math.Max(height, texture.BaseHeight) * scale;
            }
            this.Width = width;
            this.Height = height;
        }
    }
}
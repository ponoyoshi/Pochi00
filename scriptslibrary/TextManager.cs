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

            if(!texture.IsEmpty)
            {
                var sprite = generator.GetLayer("TEXT").CreateSprite(texture.Path, OsbOrigin.Centre, letterPosition);
                sprite.Fade(startTime + delay, startTime + delay + 1000, 0, 1);
                sprite.Fade(endTime + delay, endTime + delay + 300, 1, 0);
                sprite.Scale(startTime, scale);
                sprite.MoveY(OsbEasing.OutExpo, startTime + delay, startTime + delay + 1000, letterPosition.Y + generator.Random(-30, 30), letterPosition.Y);

            }
            delay += 100;
            letterX += texture.BaseWidth * scale;
        }
    }
    public void GenerateRotatingText(OsbEasing easing, string text, int startTime, int endTime, Vector2 position, float scale, float fade, int speed, string fontStyle = "Regular")
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
                + texture.OffsetFor(OsbOrigin.Centre) * scale;
           
                var sprite = generator.GetLayer("TEXT").CreateSprite(texture.Path, OsbOrigin.Centre, letterPosition);
                sprite.StartLoopGroup(startTime + delay, duration/speed);
                sprite.MoveX(easing, 0, speed, position.X + SentenceOptions.Width, position.X - SentenceOptions.Width);
                sprite.Fade(OsbEasing.InOutSine, 0, 1000, 0, fade);
                sprite.Fade(OsbEasing.InOutSine, 1000, speed - 1000, fade, fade);
                sprite.Fade(OsbEasing.InOutSine, speed - 1000, speed, fade, 0);
                sprite.ScaleVec(OsbEasing.InOutSine, 0, speed/2, 0, scale, scale, scale);
                sprite.ScaleVec(OsbEasing.InOutSine, speed/2, speed, scale, scale, 0, scale);
                sprite.EndGroup();
            }        
            letterX += texture.BaseWidth * scale;
            delay += speed/(text.Length*2);
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
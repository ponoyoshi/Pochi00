using System;
using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;

public class TextManager
{
    private StoryboardObjectGenerator generator;
    public TextManager(StoryboardObjectGenerator generator) 
    {
        this.generator = generator;
    } 

    public void GenerateTextVerticlalLetter(string text, int startTime, int endTime, Vector2 position, float scale, string fontStyle = "Regular")
    {
        var font = new FontLibrary(generator).GetFont(fontStyle);
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
using System;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

public class FlatBackground
{
    private StoryboardObjectGenerator generator;
    public FlatBackground(StoryboardObjectGenerator generator)
    {
        this.generator = generator;
    }
    public void GenerateColorBackground(int startTime, int endTime, Color4 color)
    {
        var sprite = generator.GetLayer("BACKGROUND").CreateSprite("sb/p.png");
        sprite.ScaleVec(startTime, 854, 480);
        sprite.Fade(startTime, startTime + 1000, 0, 1);
        sprite.Fade(endTime, endTime + 1000, 1, 0);
        sprite.Color(startTime, color);
    }
    public void GenerateFlash(int startTime, int duration)
    {
        var sprite = generator.GetLayer("FOREGROUND").CreateSprite("sb/p.png");
        sprite.ScaleVec(startTime, 854, 480);
        sprite.Fade(startTime, startTime + duration, 1, 0);
        sprite.Additive(startTime, startTime + duration);
    }
    public void GenerateGradientBackground(int startTime, int endTime, Color4 color1, Color4 color2)
    {
        var background = generator.GetLayer("BACKGROUND").CreateSprite("sb/p.png");
        background.ScaleVec(startTime, 854, 480);
        background.Fade(startTime, startTime + 1000, 0, 1);
        background.Fade(endTime, endTime + 1000, 1, 0);
        background.Color(startTime, color1);

        var foreground = generator.GetLayer("BACKGROUND").CreateSprite("sb/hl.png");
        foreground.Scale(startTime, 1);
        foreground.Fade(startTime, startTime + 1000, 0, 1);
        foreground.Fade(endTime, endTime + 1000, 1, 0);
        foreground.Color(startTime, color2);


    }
    public void RoundFade(int startTime, int endTime)
    {
        var sprite = generator.GetLayer("BACKGROUND").CreateSprite("sb/hl.png");
        sprite.Scale(startTime, endTime, 0, 10);
        sprite.Color(startTime, Color4.Black);
    }
    public void GenerateLinearGradient(int startFade, int startTime, int endTime, int endFade, Color4 colorFront, Color4 colorBack)
    {
        var background = generator.GetLayer("BACKGROUND").CreateSprite("sb/p.png");
        background.ScaleVec(startTime, 854, 480);
        background.Fade(startFade, startTime, 0, 1);
        background.Fade(endTime, endFade, 1, 0);
        background.Color(startTime, colorBack);

        var foreground = generator.GetLayer("BACKGROUND").CreateSprite("sb/grad.png", OsbOrigin.CentreLeft, new OpenTK.Vector2(320, 480));
        foreground.ScaleVec(startTime, 0.7, 10);
        foreground.Fade(startFade, startTime, 0, 1);
        foreground.Fade(endTime, endFade, 1, 0);
        foreground.Color(startFade, colorFront);
        foreground.Rotate(startTime, -Math.PI/2);
    
    }
}
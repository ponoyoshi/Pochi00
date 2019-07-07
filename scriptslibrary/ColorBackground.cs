using OpenTK.Graphics;
using StorybrewCommon.Scripting;

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
}
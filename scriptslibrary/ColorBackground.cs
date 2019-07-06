using OpenTK.Graphics;
using StorybrewCommon.Scripting;

public class ColorBackground
{
    public ColorBackground(int startTime, int endTime, Color4 color, StoryboardObjectGenerator Generator)
    {
        var sprite = Generator.GetLayer("BACKGROUND").CreateSprite("sb/p.png");
        sprite.ScaleVec(startTime, 854, 480);
        sprite.Fade(startTime, startTime + 1000, 0, 1);
        sprite.Fade(endTime, endTime + 1000, 1, 0);
        sprite.Color(startTime, color);
    }
}
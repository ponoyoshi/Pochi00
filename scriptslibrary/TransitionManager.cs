using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

public class TransitionManager
{
    private Color4 transitionColor = new Color4(0.05f, 0.05f, 0.05f, 1);
    private StoryboardObjectGenerator generator;
    public TransitionManager(StoryboardObjectGenerator generator)
    {
        this.generator = generator;
    }
    public void TransitionLines(int startTransition, int endTransition, int endTime)
    {
        int transitionDuration = endTransition - startTransition;
        int delay = 0;
        int posX = -120;
        for(int i = 0; i < 60; i++)
        {
            var sprite = generator.GetLayer("TRANSITION").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(posX, 240));
            sprite.ScaleVec(startTransition + delay, startTransition + delay + 300, 0, 500, 900/60f, 500);
            sprite.Fade(endTime, endTime + 1000, 1, 0);
            sprite.Rotate(startTransition, 0.1);
            sprite.Color(startTransition, transitionColor);
            
            delay += transitionDuration/60;
            posX += 900/60;
        }
    }
}
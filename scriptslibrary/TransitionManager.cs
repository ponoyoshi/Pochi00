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
    public void SquareTransition(int startTime, int endTime, bool In, int squareScale, Color4 color, OsbEasing easing)
    {
        int posX = -107;
        int posY = 0;

        while(posX < 747 + squareScale)
        {
            while(posY < 480 + squareScale)
            {
                var sprite = generator.GetLayer("TRANSITION").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(posX, posY));
                sprite.Fade(startTime, endTime, 1, 1);
                
                if(In)
                {
                    sprite.Scale(easing, startTime, endTime, 0, squareScale);
                    sprite.Rotate(easing, startTime, endTime, Math.PI, 0);
                }
                else
                {
                    sprite.Scale(easing, startTime, endTime, squareScale, 0);
                    sprite.Rotate(easing, startTime, endTime, 0, -Math.PI);        
                }
                sprite.Color(startTime, color);
                posY += squareScale;
            }
            posY = 0;
            posX += squareScale;
        }
    }
    public void SquareTransitionScaled(string spritePath, int startTime, int endTime, bool In, double scale, Color4 color, OsbEasing easing)
    {
        double posX = -107;
        double posY = 0;
        Bitmap spriteBitmap = new Bitmap(generator.MapsetPath + "/" + spritePath);

        while(posX < 747 + spriteBitmap.Height*scale)
        {
            while(posY < 480 + spriteBitmap.Height*scale)
            {
                var sprite = generator.GetLayer("TRANSITION").CreateSprite(spritePath, OsbOrigin.Centre, new Vector2((float)posX, (float)posY));
                sprite.Fade(startTime, endTime, 1, 1);
                
                if(In)
                {
                    sprite.Scale(easing, startTime, endTime, 0, scale);
                    sprite.Rotate(easing, startTime, endTime, Math.PI, 0);
                }
                else
                {
                    sprite.Scale(easing, startTime, endTime, scale, 0);
                    sprite.Rotate(easing, startTime, endTime, 0, -Math.PI);        
                }
                sprite.Color(startTime, color);
                posY += spriteBitmap.Height*scale;
            }
            posY = 0;
            posX += spriteBitmap.Height*scale;
        }
    }
}
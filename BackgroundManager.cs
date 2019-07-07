using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;
using System.Collections.Generic;
using System.IO;

namespace StorybrewScripts
{
    public class BackgroundManager : StoryboardObjectGenerator
    {
        private Background[] backgrounds;
        private string dataPath;
        private int backgroundFormat = 1040;
        public override void Generate()
        {   
            RemoveBaseBackground();
            dataPath = ProjectPath + "/backgroundData.csv";
            AddDependency(dataPath);
		    SetLibrary();
            GenerateBackgrounds();
            GenerateBlackBorders(6678, 629877);
        }
        private void GenerateBackgrounds()
        {
            string[] fileLines = File.ReadAllLines(dataPath);
            foreach(var line in fileLines)
            {
                string[] lineValues = line.Split(',');
                if(lineValues[0] == "StartTime")
                    continue;

                int startTime = int.Parse(lineValues[0]);
                int endTime = int.Parse(lineValues[1]);
                int backgroundID = int.Parse(lineValues[2]);
                string backgroundType = lineValues[3];
                string effect = lineValues[4];

                GenerateBackgroundEffect(backgrounds[backgroundID], startTime, endTime, backgroundType, effect);
            }
        }
        private void GenerateBackgroundEffect(Background background, int startTime, int endTime, string backgroundType, string effect)
        {
            OsbSprite sprite = GetLayer("DEBUG").CreateSprite("sb/DEBUG/nf.png");
            List<OsbSprite> layerSprites = null;
            bool isLayered = false;
            switch(backgroundType)
            {
                case "BASE":
                sprite = background.baseSprite;
                break;

                case "BLUR":
                sprite = background.spriteBlur;
                break;

                case "BW":
                sprite = background.spriteBlackWhite;
                break;

                case "LAYERED":
                layerSprites = background.layeredSprites;
                sprite = background.baseSprite;
                isLayered = true;
                break;
            }
            if(sprite == null)
                sprite = GetLayer("DEBUG").CreateSprite("sb/DEBUG/nf.png");

            switch(effect)
            {
                case "RIGHT":
                MoveBackground(sprite, startTime, endTime, Direction.RIGHT, isLayered, layerSprites);
                break;

                case "DOWN":
                MoveBackground(sprite, startTime, endTime, Direction.DOWN, isLayered, layerSprites);
                break;

                case "LEFT":
                MoveBackground(sprite, startTime, endTime, Direction.LEFT, isLayered, layerSprites);
                break;

                case "TOP":
                MoveBackground(sprite, startTime, endTime, Direction.TOP, isLayered, layerSprites);
                break;

                case "SCALE_UP":
                ScaleBackground(sprite, startTime, endTime, ScaleType.UP, isLayered, layerSprites);
                break;

                case "SCALE_DOWN":
                ScaleBackground(sprite, startTime, endTime, ScaleType.DOWN, isLayered, layerSprites);
                break;

                case "FADE_IN":
                FadeBackground(sprite, startTime, endTime, FadeType.IN, isLayered, layerSprites);
                break;

                case "FADE_OUT":
                FadeBackground(sprite, startTime, endTime, FadeType.OUT, isLayered, layerSprites);
                break;
            }
        }
        private void MoveBackground(OsbSprite sprite, int startTime, int endTime, Direction direction, bool isLayered, List<OsbSprite> sprites)
        {
            if(isLayered)
            {
                double moveParameter = 0;
                switch(direction)
                {
                    case Direction.RIGHT:
                    
                    break;

                    case Direction.DOWN:
                    break;

                    case Direction.LEFT:
                    break;

                    case Direction.TOP:
                    foreach(var layer in sprites)
                    {
                        layer.Move(OsbEasing.OutExpo, startTime, endTime, 320, 240 - moveParameter, 320, 240 + moveParameter);
                        moveParameter += 10;
                    }
                    break;
                }
            }
            else
            {
                switch(direction)
                {
                    case Direction.RIGHT:
                    sprite.Move(startTime, endTime, 310, 240, 330, 240);
                    break;

                    case Direction.DOWN:
                    sprite.Move(startTime, endTime, 320, 230, 320, 250);
                    break;

                    case Direction.LEFT:
                    sprite.Move(startTime, endTime, 330, 240, 310, 240);
                    break;

                    case Direction.TOP:
                    sprite.Move(startTime, endTime, 320, 250, 320, 230);
                    break;
                }
            }
        }
        private void ScaleBackground(OsbSprite sprite, int startTime, int endTime, ScaleType scaleType, bool isLayered, List<OsbSprite> sprites)
        {
            if(isLayered)
            {
                switch(scaleType)
                {
                    case ScaleType.UP:
                    break;

                    case ScaleType.DOWN:
                    break;
                }
            }
            else
            {
                switch(scaleType)
                {
                    case ScaleType.UP:
                    sprite.Scale(OsbEasing.OutSine, startTime, endTime, 480.0/backgroundFormat, 480.0/1000);
                    break;

                    case ScaleType.DOWN:
                    sprite.Scale(OsbEasing.InSine, startTime, endTime, 480.0/1000, 480.0/backgroundFormat);
                    break;
                }
            }
        }
        private void FadeBackground(OsbSprite sprite, int startTime, int endTime, FadeType fadeType, bool isLayered, List<OsbSprite> sprites)
        {
            if(isLayered)
            {
                int i = 0;
                switch(fadeType)
                {          
                    case FadeType.IN:
                    foreach(var layer in sprites)
                    {
                        layer.Fade(OsbEasing.InSine, startTime + (i * 300), endTime + (i * 300), 0, 1);
                        i++;
                    }
                        
                    break;

                    case FadeType.OUT:
                    foreach(var layer in sprites)
                    {
                        layer.Fade(OsbEasing.OutSine, startTime, startTime, 1, 0);
                        i++;
                    }  
                    break;
                }
            }
            else
            {
                switch(fadeType)
                {
                    case FadeType.IN:
                    sprite.Fade(OsbEasing.InSine, startTime, endTime, 0, 1);
                    break;

                    case FadeType.OUT:
                    sprite.Fade(OsbEasing.OutSine, startTime, endTime, 1, 0);
                    break;
                }
            }            
        }
        private void SetLibrary()
        {          
            string path = MapsetPath + "/sb/b";
            Background[] backgrounds = new Background[Directory.GetDirectories(path).Length];
            int i = 0;     
            int scale = 1050;
            foreach(var directoryFile in Directory.GetDirectories(path))
            {
                Background background = new Background();
                Log($"| DIRECTORY {i}");
                int spriteNumber = 0;
                foreach(string fileName in Directory.GetFiles(directoryFile))
                {
                    string filePath = fileName.Split(new string[] {"mia/"}, StringSplitOptions.None)[1];
                    string[] spritePath = filePath.Split('\\');
                    string spriteName = spritePath[spritePath.Length-1];
                    
                    switch(spriteName)
                    {
                        case "b.jpg":
                        background.baseSprite = GetLayer("").CreateSprite(filePath);
                        background.baseSprite.Scale(0, 480.0/backgroundFormat);
                        background.baseSprite.Fade(0, 0);
                        background.baseSprite.Additive(0, 635210);
                        Log($"> Base background set!");
                        break;

                        case "f.jpg":
                        background.spriteBlur = GetLayer("BLUR").CreateSprite(filePath);
                        background.spriteBlur.Scale(0, 480.0/backgroundFormat);
                        background.spriteBlur.Fade(0, 0);
                        background.spriteBlur.Additive(0, 635210);
                        Log($"> Blur background set!");
                        break;

                        case "w.jpg":
                        background.spriteBlackWhite = GetLayer("").CreateSprite(filePath);
                        background.spriteBlackWhite.Scale(0, 480.0/backgroundFormat);
                        background.spriteBlackWhite.Fade(0, 0);
                        background.spriteBlackWhite.Additive(0, 635210);
                        Log($"> Black&White background set!");
                        break;
                    }   
                    if(spriteName[0] == 'l')
                    {
                        background.layeredSprites.Add(GetLayer(spriteNumber.ToString()).CreateSprite(filePath));
                        spriteNumber++;
                        Log($"+ Added layer {spriteName}");   
                    }    
                }
                foreach(var sprite in background.layeredSprites)
                {
                    sprite.Scale(0, 480.0/1050);
                    sprite.Fade(0, 0);
                    scale -= 50;
                }        
                Log("");
                backgrounds[i] = background;
                i++;
            }
            this.backgrounds = backgrounds;
        }
        private class Background : OsbSprite
        {
            public OsbSprite baseSprite {get; set;}
            public List<OsbSprite> layeredSprites = new List<OsbSprite>();
            public OsbSprite spriteBlur {get; set;}
            public OsbSprite spriteBlackWhite {get; set;}
            public Background(){}
        }
        private enum Direction
        {
            RIGHT,
            DOWN,
            LEFT,
            TOP
        }
        private enum ScaleType
        {
            UP,
            DOWN
        }
        private enum FadeType
        {
            IN,
            OUT,
        }
        private void RemoveBaseBackground()
            => GetLayer("").CreateSprite("bg.jpg").Fade(0,0);

        private void GenerateBlackBorders(int startTime, int endTime)
        {
            var spriteTop = GetLayer("FOREGROUND").CreateSprite("sb/p.png", OsbOrigin.TopCentre, new Vector2(320, 0));
            var spriteBot = GetLayer("FOREGROUND").CreateSprite("sb/p.png", OsbOrigin.BottomCentre, new Vector2(320, 480));

            spriteTop.Color(startTime, Color4.Black);
            spriteBot.Color(startTime, Color4.Black);

            spriteTop.Fade(startTime, endTime + 1000, 1, 1);
            spriteBot.Fade(startTime, endTime + 1000, 1, 1);

            spriteTop.ScaleVec(OsbEasing.OutSine, startTime, startTime + 1000, 854, 0, 854, 30);
            spriteBot.ScaleVec(OsbEasing.OutSine, startTime, startTime + 1000, 854, 0, 854, 30);

            spriteTop.ScaleVec(OsbEasing.OutSine, endTime, endTime + 1000, 854, 50, 854, 0);
            spriteBot.ScaleVec(OsbEasing.OutSine, endTime, endTime + 1000, 854, 50, 854, 0);

        }
    }
}

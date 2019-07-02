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
        public override void Generate()
        {   
            dataPath = ProjectPath + "/SBDATA - BGDATA.csv";
            AddDependency(dataPath);
		    SetLibrary();
            GenerateBackgrounds();
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
                isLayered = true;
                break;
            }
            switch(effect)
            {
                case "RIGHT":
                MoveBackground(sprite, startTime, endTime, Direction.RIGHT, isLayered);
                break;

                case "DOWN":
                MoveBackground(sprite, startTime, endTime, Direction.DOWN, isLayered);
                break;

                case "LEFT":
                MoveBackground(sprite, startTime, endTime, Direction.LEFT, isLayered);
                break;

                case "TOP":
                MoveBackground(sprite, startTime, endTime, Direction.TOP, isLayered);
                break;

                case "SCALE_UP":
                ScaleBackground(sprite, startTime, endTime, ScaleType.UP, isLayered);
                break;

                case "SCALE_DOWN":
                ScaleBackground(sprite, startTime, endTime, ScaleType.DOWN, isLayered);
                break;
            }
        }
        private void MoveBackground(OsbSprite sprite, int startTime, int endTime, Direction direction, bool isLayered)
        {
            if(isLayered)
            {
                switch(direction)
                {
                    case Direction.RIGHT:
                    
                    break;

                    case Direction.DOWN:
                    break;

                    case Direction.LEFT:
                    break;

                    case Direction.TOP:
                    break;
                }
            }
            else
            {
                switch(direction)
                {
                    case Direction.RIGHT:
                    sprite.Move(OsbEasing.InOutSine, startTime, endTime, 310, 240, 330, 240);
                    sprite.Fade(OsbEasing.InSine, startTime, startTime + 1000, 0, 1);
                    sprite.Fade(OsbEasing.OutSine, endTime, endTime + 1000, 1, 0);
                    break;

                    case Direction.DOWN:
                    sprite.Move(OsbEasing.InOutSine, startTime, endTime, 320, 230, 320, 250);
                    sprite.Fade(OsbEasing.InSine, startTime, startTime + 1000, 0, 1);
                    sprite.Fade(OsbEasing.OutSine, endTime, endTime + 1000, 1, 0);
                    break;

                    case Direction.LEFT:
                    sprite.Move(OsbEasing.InOutSine, startTime, endTime, 330, 240, 310, 240);
                    sprite.Fade(OsbEasing.InSine, startTime, startTime + 1000, 0, 1);
                    sprite.Fade(OsbEasing.OutSine, endTime, endTime + 1000, 1, 0);
                    break;

                    case Direction.TOP:
                    sprite.Move(OsbEasing.InOutSine, startTime, endTime, 320, 250, 320, 230);
                    sprite.Fade(OsbEasing.InSine, startTime, startTime + 1000, 0, 1);
                    sprite.Fade(OsbEasing.OutSine, endTime, endTime + 1000, 1, 0);
                    break;
                }
            }
        }
        private void ScaleBackground(OsbSprite sprite, int startTime, int endTime, ScaleType scaleType, bool isLayered)
        {

        }
        private void SetLibrary()
        {          
            string path = MapsetPath + "/sb/b";
            Background[] backgrounds = new Background[Directory.GetDirectories(path).Length];
            int i = 0;     
            foreach(var directoryFile in Directory.GetDirectories(path))
            {
                Background background = new Background();
                Log($"| DIRECTORY {i}");
                foreach(string fileName in Directory.GetFiles(directoryFile))
                {
                    string filePath = fileName.Split(new string[] {"mia/"}, StringSplitOptions.None)[1];
                    string[] spritePath = filePath.Split('\\');
                    string spriteName = spritePath[spritePath.Length-1];
                    
                    switch(spriteName)
                    {
                        case "b.jpg":
                        background.baseSprite = GetLayer("BASE").CreateSprite(filePath);
                        background.baseSprite.Scale(0, 480.0/1050);
                        Log($"> Base background set!");
                        break;

                        case "f.jpg":
                        background.spriteBlur = GetLayer("BLUR").CreateSprite(filePath);
                        background.spriteBlur.Scale(0, 480.0/1050);
                        Log($"> Blur background set!");
                        break;

                        case "w.jpg":
                        background.spriteBlackWhite = GetLayer("BLACKWHITE").CreateSprite(filePath);
                        background.spriteBlackWhite.Scale(0, 480.0/1050);
                        Log($"> Black&White background set!");
                        break;
                    }   
                    if(spriteName[0] == 'l')
                    {
                        background.layeredSprites.Add(GetLayer("LAYERS").CreateSprite(filePath));
                        Log($"+ Added layer {spriteName}");   
                    }    
                }
                foreach(var sprite in background.layeredSprites)
                    sprite.Scale(0, 480.0/1080);

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
    }
}

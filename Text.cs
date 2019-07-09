using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace StorybrewScripts
{
    public class Text : StoryboardObjectGenerator
    {
        [Configurable]
        public OsbOrigin LyricsOrigin = OsbOrigin.Centre;

        [Configurable]
        public string FontName = "Verdana";

        [Configurable]
        public string OutputPath = "sb/lyrics/main";

        [Configurable]
        public float Fade = 0.8f;

        [Configurable]
        public int FontSize = 26;

        [Configurable]
        public float FontScale = 0.5f;

        [Configurable]
        public Color4 FontColor = Color4.White;

        [Configurable]
        public FontStyle FontStyle = FontStyle.Regular;

        [Configurable]
        public int GlowRadius = 0;

        [Configurable]
        public Color4 GlowColor = new Color4(255, 255, 255, 255);

        [Configurable]
        public bool AdditiveGlow = true;

        [Configurable]
        public int OutlineThickness = 0;

        [Configurable]
        public Color4 OutlineColor = new Color4(50, 50, 50, 200);

        [Configurable]
        public int ShadowThickness = 4;

        [Configurable]
        public Color4 ShadowColor = new Color4(0, 0, 0, 200);

        [Configurable]
        public Vector2 Padding = Vector2.Zero;

        [Configurable]
        public bool TrimTransparency = true;

        [Configurable]
        public bool EffectsOnly = false;

        [Configurable]
        public bool Debug = false;

        [Configurable]
        public bool Additive = false;

        [Configurable]
        public bool RandomLyricsColor = false;

        [Configurable]
        public Color4 MinLyricsColor = new Color4(255, 255, 255, 255);

        [Configurable]
        public Color4 MaxLyricsColor = new Color4(100, 100, 100, 255);

        public override void Generate()
        {
            var font = LoadFont(OutputPath, new FontDescription()
            {
                FontPath = FontName,
                FontSize = FontSize,
                Color = FontColor,
                Padding = Padding,
                FontStyle = FontStyle,
                TrimTransparency = TrimTransparency,
                EffectsOnly = EffectsOnly,
                Debug = Debug,
            },
            new FontGlow()
            {
                Radius = AdditiveGlow ? 0 : GlowRadius,
                Color = GlowColor,
            },
            new FontOutline()
            {
                Thickness = OutlineThickness,
                Color = OutlineColor,
            },
            new FontShadow()
            {
                Thickness = ShadowThickness,
                Color = ShadowColor,
            });

            string[] Part = {

                            "Mitose Noriko - 炎路夜行", // 0
                            "Taishi feat. Himawari - AIRI-愛離-", // 1
                            "SHIHO - One Summer", // 2
                            "Shimotsuki Haruka - stardrops", // 3
                            "SHIHO - Fallin Snow", // 4
                            "Himawari - Slow Step -First Love Comes Again-", // 5
                            "Electro.muster - Think The Future -Club Extend-", // 6
                            "TRäkker - Future & Aquarium (TRakker Ver)", // 7
                            "Taishi - Downloader", // 8
                            "Mai Fuchigami - Flying Saucer", // 9
                            "Mitose Noriko - Personalizer", // 10
                            "Himawari - Engage", // 11

                            // part 2
                            "      SecondPart", // 12
                            "      (Higher BPM)", // 13

                            "Electro.muster - Amazing Sweet", // 14
                            "Noriko Mitose - The Party of SevenWitches", // 15
                            "Taishi - Bryn (Club Track)", // 16
                            "Rita - Dream Walker", // 17
                            "Mitose Noriko - Duel Alternatives", // 18
                            "Rita - Into the cosmos", // 19
                            "Eri Kumagai(cv.Asami Seto) - Winteright", // 20
                            "Electro.muster - A Sugar Business", // 21
                            "TRakker - Countdown to the Blue", // 22
                            "mintea - Defect of Memoria", // 23
                            "Rita - Jet Loser", // 24
                            "TRakker - Rootus", // 25
                            "Rita - Blue Liner", // 26
                            "TRakker - Trail of Dust" // 27
            };

            CreateLyrics(font, MinLyricsColor, Part[0], FontName, FontSize, new Vector2(322, 295), 29556, 74556, 4);
            CreateLyrics(font, MinLyricsColor, Part[1], FontName, FontSize, new Vector2(322, 295), 69413, 121787, 3);
            CreateLyrics(font, MinLyricsColor, Part[2], FontName, FontSize, new Vector2(322, 295), 116453, 177592, 6);
            CreateLyrics(font, MinLyricsColor, Part[3], FontName, FontSize, new Vector2(322, 295), 170571, 253854, 6);
            CreateLyrics(font, MinLyricsColor, Part[4], FontName, FontSize, new Vector2(322, 295), 253854, 288545, 3);
            CreateLyrics(font, MinLyricsColor, Part[5], FontName, FontSize, new Vector2(322, 295), 281982, 327833, 2);
            CreateLyrics(font, MinLyricsColor, Part[6], FontName, FontSize, new Vector2(322, 295), 322442, 393045, 3);
            CreateLyrics(font, MinLyricsColor, Part[7], FontName, FontSize, new Vector2(322, 295), 387654, 446382, 3);
            CreateLyrics(font, MinLyricsColor, Part[8], FontName, FontSize, new Vector2(322, 295), 444536, 497693, 5);
            CreateLyrics(font, MinLyricsColor, Part[9], FontName, FontSize, new Vector2(322, 295), 492197, 542905, 3);
            CreateLyrics(font, MinLyricsColor, Part[10], FontName, FontSize, new Vector2(322, 295), 533814, 634773, 7);
            CreateLyrics(font, MinLyricsColor, Part[11], FontName, FontSize, new Vector2(322, 295), 629556, 692415, 7);

            // part 2 (higher bpm)
            CreateLyrics(font, MaxLyricsColor, Part[12], FontName, FontSize, new Vector2(322, 285), 692415, 703505, 1);
            CreateLyrics(font, MaxLyricsColor, Part[13], FontName, FontSize, new Vector2(322, 305), 692415, 703505, 1);

            CreateLyrics(font, MinLyricsColor, Part[14], FontName, FontSize, new Vector2(322, 295), 700634, 752818, 3);
            CreateLyrics(font, MinLyricsColor, Part[15], FontName, FontSize, new Vector2(322, 295), 744755, 823365, 4);
            CreateLyrics(font, MinLyricsColor, Part[16], FontName, FontSize, new Vector2(322, 295), 821953, 878432, 4);
            CreateLyrics(font, MinLyricsColor, Part[17], FontName, FontSize, new Vector2(322, 295), 872946, 929442, 5);
            CreateLyrics(font, MinLyricsColor, Part[18], FontName, FontSize, new Vector2(322, 295), 921213, 958065, 2);
            CreateLyrics(font, MinLyricsColor, Part[19], FontName, FontSize, new Vector2(322, 295), 952580, 996478, 3);
            CreateLyrics(font, MinLyricsColor, Part[20], FontName, FontSize, new Vector2(322, 295), 985507, 1028081, 2);
            CreateLyrics(font, MinLyricsColor, Part[21], FontName, FontSize, new Vector2(322, 295), 1022595, 1084416, 4);
            CreateLyrics(font, MinLyricsColor, Part[22], FontName, FontSize, new Vector2(322, 295), 1084416, 1146507, 4);
            CreateLyrics(font, MinLyricsColor, Part[23], FontName, FontSize, new Vector2(322, 295), 1144485, 1180037, 3);
            CreateLyrics(font, MinLyricsColor, Part[24], FontName, FontSize, new Vector2(322, 295), 1180037, 1234245, 7);
            CreateLyrics(font, MinLyricsColor, Part[25], FontName, FontSize, new Vector2(322, 295), 1236941, 1303429, 8);
            CreateLyrics(font, MinLyricsColor, Part[26], FontName, FontSize, new Vector2(322, 295), 1302080, 1348609, 5);
            CreateLyrics(font, MinLyricsColor, Part[27], FontName, FontSize, new Vector2(322, 295), 1344609, 1407888, 5);
        }

        private void CreateLyrics(FontGenerator font, Color4 ColorType, string Sentence, string FontName, int FontSize, Vector2 position, int StartTime, int EndTime, int loopAmount)
        {
            // string[] Sentence = sentence.Split(' ');
            // string[] Sentence = sentence.Split(new char[0]);
            var LyricsLayer = GetLayer("");

            var letterY = position.Y;
            var lineWidth = 0f;
            var lineHeight = 0f;
            foreach (var letter in Sentence)
            {
                var texture = font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * FontScale;
                lineHeight = Math.Max(lineHeight, texture.BaseHeight * FontScale);
            }

            var letterX = position.X - lineWidth * 0.5f;

            int letterAmount = Sentence.Length;
            // Log(Sentence.ToString());

            var i = 0;
            foreach (var letter in Sentence)
            {
                var texture = font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    var letterPos = new Vector2(letterX, letterY)
                        + texture.OffsetFor(LyricsOrigin) * FontScale;

                    var sprite = LyricsLayer.CreateSprite(texture.Path, LyricsOrigin);

                    var HoverInterval = 5000;
                    var letterInterval = (400 - letterAmount) + letterAmount;
                    var letterDuration = (8000 - letterAmount) + letterAmount;

                    var duration = 500 * letterAmount; // 23500 if letterAmount = 16
                    var letterSpaceInterval = 25 * letterAmount;
                    float lineSpace = ((letterDuration / letterInterval) * letterAmount) - 200; // returns 940
                    var RealDuration = ((letterDuration / letterInterval) * letterSpaceInterval) - 8000; // returns 15500
                    var Duration = (duration - RealDuration); // returns 8000

                    Log(lineSpace.ToString()); // log

                    var FadeTime = 1500;
                    var SphereStartTime = 26984;
                    sprite.StartLoopGroup(SphereStartTime - FadeTime, EndTime - SphereStartTime);
                    sprite.MoveY(OsbEasing.InOutSine, FadeTime, HoverInterval, letterPos.Y - 10, letterPos.Y + 10);
                    sprite.MoveY(OsbEasing.InOutSine, HoverInterval, HoverInterval * 2, letterPos.Y + 10, letterPos.Y - 10);
                    sprite.EndGroup();

                    var startX = position.X + 50;
                    var endX = position.X - 50;
                    var fadeTime = 2.5;
                    
                    sprite.StartLoopGroup(StartTime - FadeTime, loopAmount);
                    sprite.MoveX(OsbEasing.InOutSine, (lineSpace) + letterInterval * i, Duration + letterInterval * i, startX, endX);
                    sprite.MoveX(OsbEasing.InOutSine, Duration + letterInterval * i, RealDuration + Duration + letterInterval * i, endX, endX);
                    sprite.EndGroup();

                    var ScaleStart = new Vector2(FontScale, FontScale);
                    var ScaleEnd = new Vector2(0, FontScale);
                    
                    sprite.StartLoopGroup(StartTime - FadeTime, loopAmount);
                    sprite.Fade(lineSpace + letterInterval * i, lineSpace + (letterDuration / 2.8) + letterInterval * i, 0, Fade);
                    sprite.Fade((Duration - (letterDuration / 2.8)) + letterInterval * i, Duration + letterInterval * i, Fade, 0);
                    sprite.ScaleVec(OsbEasing.InOutQuad, lineSpace + letterInterval * i, lineSpace + (letterDuration / fadeTime) + letterInterval * i, ScaleEnd, ScaleStart);
                    sprite.ScaleVec(OsbEasing.InOutQuad, (Duration - (letterDuration / fadeTime)) + letterInterval * i, Duration + letterInterval * i, ScaleStart, ScaleEnd);
                    sprite.Fade(Duration + letterInterval * i, RealDuration + Duration + letterInterval * i, 0, 0);
                    sprite.EndGroup();

                    sprite.Color(StartTime, ColorType);
                    sprite.Fade(SphereStartTime, 0);

                    if (Additive)
                    {
                        sprite.Additive(StartTime, EndTime);
                    }
                    i++;
                }
            }
            letterY += lineHeight;
        }
    }
}

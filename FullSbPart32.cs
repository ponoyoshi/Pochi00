using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class FullSbPart32 : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var PianoHits = new int[]{
                // FullSbPart32
                575210, 575710, 575793, 575877, 576210, 576377, 576543, 576793, 577043, 577210, 577543, 577877, 578210,
                578293, 578377, 578460, 578543, 578877, 579210, 579460, 579710, 579877, 580127, 580377, 580543,
                580877, 580960, 581043, 581127, 581210, 581460, 581710, 581877, 582210, 582377, 582460, 582543,
                582793, 583043, 583210, 583543, 583710, 583793, 583877, 584127, 584377, 584543, 584793, 585043, 585210,
                585543, 585877, 586210, 586543, 586877
            };

            GenerateVerticalBar(PianoHits);
        }

        private void GenerateVerticalBar(int[] pianoHits)
        {
            for (int i = 0; i < Random(1, 3); i++)
            {
                foreach (var hit in pianoHits)
                {
                    var position = new Vector2(Random(0, 640), 240);
                    var sprite = GetLayer("PianoHighlights").CreateSprite("sb/p.png", OsbOrigin.Centre, position);

                    sprite.ScaleVec(hit, 60, 480);
                    sprite.Fade(hit, hit + 500, 0.1, 0);
                    sprite.Additive(hit, hit + 500);

                    foreach (var hitobject in Beatmap.HitObjects)
                    {
                        if ((hit != 0 || hit + 500 != 0) &&
                        (hitobject.StartTime < hit - 5 || hit + 500 - 5 <= hitobject.StartTime))
                            continue;

                        sprite.Color(hit, hitobject.Color);
                    }
                }
            }
        }
    }
}

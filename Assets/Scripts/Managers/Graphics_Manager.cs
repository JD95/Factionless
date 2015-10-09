using UnityEngine;
using System;
using System.Collections;

public enum graphic { Postion, Rotation, Scale, Head, Body, Weapon};

namespace Effect_Management
{
    public class Graphics_Manager
    {

        public static TimedEffect_table<Graphical> timedEffects = new TimedEffect_table<Graphical>();
        public static LastingEffect_table<Graphical> lastingEffects = new LastingEffect_table<Graphical>();

        const int numGraphics = 6;

        Effect_Container<Graphical>[] graphics = new Effect_Container<Graphical>[numGraphics];

        public Graphics_Manager()
        {
            for (int i = 0; i < numGraphics; i++)
            {
                graphics[i] = new Effect_Container<Graphical>();
            }
        }

        public void stepTime()
        {
            foreach(var graphic in graphics)
            {
                graphic.stepTime();
                graphic.compileEffects().value();
            }
            
        }

        public void addTimedEffectFor(graphic g, string effect)
        {
            graphics[(int)g].add_timedEffect(timedEffects[effect]());
        }

        public void addLastingEffectFor(graphic g, string effect)
        {
            graphics[(int)g].add_lastingEffect(lastingEffects[effect]());
        }

        public void removeLastingEffectFor(graphic g, string id)
        {
            graphics[(int)g].remove_lastingEffect(id);
        }

        public void removeHarmfulEffects()
        {
            foreach (var grahpic in graphics)
            {
                grahpic.removeHarmful();
            }
        }

    }


    public class Graphics_Effects
    {
        public static EffectApply<Graphical> 
            phased_Transform(GameObject target, Vector3 initState, Vector3 targetState,
                             float transitionTime, float usageLength)
        {
            DateTime startTime = DateTime.Now;
            DateTime transitionEnd = startTime.AddSeconds((double) transitionTime);
            DateTime usageEnd = transitionEnd.AddSeconds((double) usageLength);
            DateTime effectEnd = usageEnd.AddSeconds((double) transitionTime);

            return (time, stacks) => new Graphical(
                () => {
                    var current = target.transform.localScale;

                    if (time <= transitionEnd)
                    {
                        target.transform.localScale = Vector3.Lerp(current, targetState, Time.deltaTime * 1/transitionTime);
                    }
                    else if (time <= usageEnd) { /* Do nothing while effect is in use */ }
                    else if (time <= effectEnd)
                    {
                        target.transform.localScale = Vector3.Lerp(current, initState, Time.deltaTime * 1/transitionTime);
                    }
                }    
            );
        }
    }
}
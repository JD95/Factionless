using UnityEngine;
using System;
using System.Collections;

using Effect_Management;
using Attribute = Effect_Management.Attribute;

using AbilityHelp = Utility.AbilityHelp;

public class Translate : Ability {

    public const string abilityName = "Translate";

    public override Tuple<bool, Ability_Overlay> trigger()
    {
        GameObject selected = AbilityHelp.getSelectable_UnderMouse();
        Navigation nav = caster.GetComponent<Navigation>();
        Character character = caster.GetComponent<Character>();

        if (selected == null) { return new Tuple<bool, Ability_Overlay>(false, null); } // Ability needs a target
        else
        {
            // Turn off navigation
            nav.toggle_navigation_lock(Nav_Lock.channeling);

            nav.disableMeshAgent(); // Allows caster to pass through objects

            character.graphics.addTimedEffectFor(graphic.Postion, abilityName, caster);

            return new Tuple<bool, Ability_Overlay>(true, null);
        }
    }

    public static Timed_Effect<Graphical> make_translate(GameObject caster)
    {
        return translateTo(caster, AbilityHelp.getSelectable_UnderMouse().transform.position, 1.0F);
    }

    public static Timed_Effect<Graphical> translateTo(GameObject target, Vector3 destination, float duration)
    {
        return new Timed_Effect<Graphical>(
            new effectInfo(abilityName, EffectType.Buff, 1, duration, DateTime.Now),
            Graphics_Effects.continuous_Translate(target, destination , duration),
            () => {
                var nav = target.GetComponent<Navigation>();
                nav.enableMeshAgent();
                nav.toggle_navigation_lock(Nav_Lock.channeling);
            });
    }

    public override void passiveEffect()
    {    }

    public override void registerEffects()
    {
        Effect_Management.Graphics_Manager.timedEffects.Add(abilityName, make_translate);
        Effect_Management.Graphics_Manager.timedEffects.Add("TranslateTo", make_translate);
    }
}

using UnityEngine;
using System;
using System.Collections;

using Effect_Management;
using Attribute = Effect_Management.Attribute;

using AbilityHelp = Utility.AbilityHelp;

public class Translate : Ability {

    public const string abilityName = "Translate";

    public override bool trigger()
    {
        GameObject selected = AbilityHelp.getSelectable_UnderMouse();
        Navigation nav = caster.GetComponent<Navigation>();
        Character character = caster.GetComponent<Character>();

        if (selected == null) { return false; } // Ability needs a target
        else
        {
            // Turn off navigation
            nav.turnOn_Channeling();

            nav.disableMeshAgent(); // Allows caster to pass through objects

            character.graphics.addTimedEffectFor(graphic.Postion, abilityName);

            return true;
        }
    }

    public Func<Timed_Effect<Graphical>> make_translate(GameObject caster)
    {
        return () => new Timed_Effect<Graphical>(
            new effectInfo(abilityName, EffectType.Buff, 1, 1.0, DateTime.Now),
            Graphics_Effects.continuous_Translate(caster,AbilityHelp.getSelectable_UnderMouse().transform.position, 1.0F),
            () =>
            {
                var nav = caster.GetComponent<Navigation>();
                nav.enableMeshAgent();
                nav.turnOff_Channeling();
            });
    }

    public override void passiveEffect()
    {    }

    public override void registerEffects()
    {
        Effect_Management.Graphics_Manager.timedEffects.Add(abilityName, make_translate(caster));
    }
}

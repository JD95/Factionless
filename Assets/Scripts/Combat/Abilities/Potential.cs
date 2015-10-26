using UnityEngine;
using System;
using System.Collections;

using Utility;
using Effect_Management;
using Attribute = Effect_Management.Attribute;

public class Potential : Ability {

    public const string thisAbility = "Potential";

    public float healAmount = 10;

    public override bool trigger()
    {
        // Grab target under mouse
        var selected = AbilityHelp.getSelectable_UnderMouse();

        if (TeamLogic.areEnemies(caster, selected))
        {
            selected.GetComponent<Combat>().stats.effects.addTimedEffectFor(attribute.AS, thisAbility, selected);
            return true;
        }
        else if (TeamLogic.areAllies(caster, selected))
        {
            selected.GetComponent<Combat>().recieve_Healing(healAmount);
            return true;
        }
        else return false;
    }

    public override void passiveEffect()
    {

    }

    public Timed_Effect<Attribute> make_Potential(GameObject caster)
    {
        return new Timed_Effect<Attribute>(
            new effectInfo(thisAbility, EffectType.Slow, 1, 3.0, DateTime.Now),
            (time,stacks) => new Attribute(0,0,0,10*stacks),
            () => { }
        );
    }


    public override void registerEffects()
    {
        Attribute_Manager.timedEffects.Add(thisAbility, make_Potential);
    }
}

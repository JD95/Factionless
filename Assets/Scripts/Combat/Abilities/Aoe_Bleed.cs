﻿using UnityEngine;
using System;
using System.Collections;

using Utility;
using Effect_Management;

public class Aoe_Bleed : Ability {

    public float range;

    public override Tuple<bool, Ability_Overlay> trigger()
    {
        //Debug.Log("AOE_BLEED has been triggerd");
        var toSlice = TeamLogic.enemyCombatsInRange(caster, range);

        foreach(var enemy in toSlice)
        {
            enemy.recieve_Damage_Physical(5.0f);
            enemy.stats.effects.addTimedEffectFor(attribute.HP, "Shadow Slash", null);
        }

        return new Tuple<bool, Ability_Overlay>(true, null);
    }

    public override Tuple<bool, Ability_Overlay> trigger_ai()
    {
        throw new NotImplementedException();
    }

    public override void passiveEffect()
    {
        // None
    }

    public override void registerEffects()
    {
        Effect_Management.Attribute_Manager.timedEffects.Add("Shadow Slash", shadowSlash);
    }

    public static Timed_Effect<Effect_Management.Attribute> shadowSlash(GameObject target)
    {
        return new Timed_Effect<Effect_Management.Attribute>(
                    new effectInfo("Shadow Slash", EffectType.Bleed, 1, 10.0, DateTime.Now),
                    Attribute_Effects.periodic_changeBy(1.0, -5.0),
                    Utility_Effects.doNothing_Stop()
                );
    }

}

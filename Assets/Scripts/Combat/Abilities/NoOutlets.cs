using UnityEngine;
using System;
using System.Collections;

using Utility;
using Effect_Management;
using Attribute = Effect_Management.Attribute;

public class NoOutlets : Ability
{
    public const string thisAbility = "No Outlets";
    public const string noOutlet_heal = "No Outlets Heal";
    public const string noOutlet_damage = "No Outlets Blocked Healing";

    public const float areaOfEffect = 10f;

    public const double healDelay = 3;
    public const double healBlockTime = 5; 

    public float timedHeal;


    public override bool trigger()
    {
        // Grab All enemies in area
        var enemies = TeamLogic.enemyCombatsInRange(caster, areaOfEffect);
        var blockHeals = new Attribute_Filter(noOutlet_damage, (healing) => 0);

        // Apply heal block to each
        foreach (var enemy in enemies)
        {
            enemy.stats.effects.recieveHealing.Add(blockHeals);
        }

        // Grab all allies in area
        var allies = TeamLogic.allyCombatsIntRange(caster, areaOfEffect);

        // Apply timed heal to each
        foreach (var ally in allies)
        {
            ally.stats.effects.addTimedEffectFor(attribute.HP, noOutlet_heal, caster);
        }

        return true;
    }

    public override void passiveEffect()
    {

    }

    public static Timed_Effect<Attribute> make_NoOutlets_Heal(GameObject target)
    {
        var late = target.GetComponent<Abilities>().r.GetComponent<NoOutlets>();

        return new Timed_Effect<Attribute>(
            new effectInfo(noOutlet_heal, EffectType.Posion, 1, healDelay, DateTime.Now),
            (t, s) => Attribute.zero(),
            () => { target.GetComponent<Combat>().recieve_Healing(late.timedHeal); }
        );
    }

    public static Timed_Effect<Attribute> make_Block_Healing(GameObject target)
    {
        return new Timed_Effect<Attribute>(
            new effectInfo(noOutlet_damage, EffectType.Posion, 1, healBlockTime, DateTime.Now),
            (t, s) => Attribute.zero(),
            () => { target.GetComponent<Combat>().stats.effects.recieveHealing.RemoveAll(x => x.name == noOutlet_damage); }
        );
    }

    public override void registerEffects()
    {
        Attribute_Manager.timedEffects.Add(noOutlet_damage, make_Block_Healing);
        Attribute_Manager.timedEffects.Add(noOutlet_heal, make_NoOutlets_Heal);
    }
}

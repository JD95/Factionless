using UnityEngine;
using System;
using System.Collections.Generic;

using Effect_Management;
using Attribute = Effect_Management.Attribute;

public class AlwaysLate : Ability
{
    public const string thisAbility = "Always Late";
    public const string delay_Damage = "Delayed Damage";
    public const string delay_Healing = "Delayed Healing";

    // Two queues are used to store the delayed damage and healing
    // Timed effects pull from the queue to apply
    public Queue<float> delayedDamage = new Queue<float>();
    public Queue<float> delayedHealing = new Queue<float>();

    public override Tuple<bool, Ability_Overlay> trigger()
    {
        return new Tuple<bool, Ability_Overlay>(true, null);
    }

    public override void passiveEffect()
    {
        var casterCombat = caster.GetComponent<Combat>();

        // Take damage and convert into a timed status effect
        var damageDelayer = new Attribute_Filter(thisAbility, (damage) =>
        {
            delayedDamage.Enqueue(damage);
            casterCombat.stats.effects.addTimedEffectFor(attribute.HP, delay_Damage, caster);
            return 0;
        });

        // Take Healing and convert into a timed status effect
        var healingDelayer = new Attribute_Filter(thisAbility, (healing) =>
        {
            delayedHealing.Enqueue(healing);
            casterCombat.stats.effects.addTimedEffectFor(attribute.HP, delay_Healing, caster);
            return 0;
        });

        // Add the filters
        casterCombat.stats.effects.recieveDamage.Add(damageDelayer);
        casterCombat.stats.effects.recieveHealing.Add(healingDelayer);
    }

    public static Timed_Effect<Effect_Management.Attribute> make_delayedDamage(GameObject target)
    {
        var late = target.GetComponent<Abilities>().e.GetComponent<AlwaysLate>().delayedDamage;

        return new Timed_Effect<Attribute>(
            new effectInfo(delay_Damage, EffectType.Posion ,1, 5.0, DateTime.Now),
            (t,s) => { return Attribute.zero(); },
            () => {
                float futureDamage = late.Dequeue();
                target.GetComponent<Combat>().recieve_Damage_Direct(futureDamage);
            });
    }

    public static Timed_Effect<Effect_Management.Attribute> make_delayedHealing(GameObject target)
    {
        var late = target.GetComponent<Abilities>().e.GetComponent<AlwaysLate>().delayedHealing;

        return new Timed_Effect<Attribute>(
            new effectInfo(delay_Healing, EffectType.Buff, 1, 5.0, DateTime.Now),
            (t, s) => { return Attribute.zero(); },
            () => {
                float futureHealing = late.Dequeue();
                target.GetComponent<Combat>().recieve_Damage_Direct(futureHealing);
            });
    }

    public override void registerEffects()
    {
        // register delayed damage
        Attribute_Manager.timedEffects.Add(delay_Damage, make_delayedDamage);

        // register delayed healing
        Attribute_Manager.timedEffects.Add(delay_Healing, make_delayedHealing);
    }
}

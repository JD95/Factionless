using UnityEngine;
using System.Collections;
using System;

using Utility;
using Effect_Management;
using Attribute = Effect_Management.Attribute;

public class TakeItBack : Ability {

    public const string thisAbility = "Take it Back";
    public float shieldAmount = 20;

    public float damage = 20;

    public override Tuple<bool, Ability_Overlay> trigger()
    {
        // Grab Target under mouse
        var selected = AbilityHelp.getSelectable_UnderMouse();

        if (TeamLogic.areAllies(caster, selected)) return new Tuple<bool, Ability_Overlay> (false, null);

        var enemy = selected.GetComponent<Combat>();
        var backSheild = caster.GetComponent<Abilities>().e.GetComponent<TakeItBack>();

        var sheild = new Attribute_Filter(thisAbility, (damage) =>
            {
                backSheild.shieldAmount -= damage;

                if (backSheild.shieldAmount < 0)
                {
                    enemy.stats.effects.recieveDamage.RemoveAll(filter => filter.name == thisAbility);
                    backSheild.shieldAmount = backSheild.damage;
                    return backSheild.damage * 2;
                }

                else return 0;
            }
        );

        // Apply sheild
        enemy.stats.effects.recieveDamage.Add(sheild);
        enemy.stats.effects.addTimedEffectFor(attribute.HP, thisAbility, selected);

        return new Tuple<bool, Ability_Overlay>(true, null);
    }

    public override Tuple<bool, Ability_Overlay> trigger_ai()
    {
        throw new NotImplementedException();
    }

    public Timed_Effect<Attribute> make_TakeItBack(GameObject target)
    {
        var backSheild = caster.GetComponent<Abilities>().e.GetComponent<TakeItBack>();

        return new Timed_Effect<Attribute>(
            new effectInfo(thisAbility, EffectType.Posion, 1, 5.0, DateTime.Now),
            (t,s) => Attribute.zero(),
            () => 
            {
                var targetCombat = target.GetComponent<Combat>();

                // Remove filter
                targetCombat.stats.effects.recieveDamage.RemoveAll(filter => filter.name == thisAbility);

                // If sheild hasn't been drained yet, only deal normal damage
                if (backSheild.shieldAmount > 0) targetCombat.recieve_Damage_Magic(backSheild.damage);

                // Reset sheild amount
                backSheild.shieldAmount = backSheild.damage;
            }    
        );
    }

    public override void passiveEffect()
    {

    }

    public override void registerEffects()
    {
        Attribute_Manager.timedEffects.Add(thisAbility, make_TakeItBack);
    }
}

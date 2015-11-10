using UnityEngine;
using System;
using System.Collections;

using Effect_Management;
using Attribute = Effect_Management.Attribute;



public class Dodge_EveryN : Ability{

    const string thisAbility = "Dodge_EveryN";

    public  int dodgeRate;
    private int attackCount = 0;

    public override Tuple<bool, Ability_Overlay> trigger()
    {
        // No triggered effect
        return new Tuple<bool, Ability_Overlay>(true, null);
    }

    public override void passiveEffect()
    {
        var casterCombat = caster.GetComponent<Combat>();

        var dodgeFilter = new Attribute_Filter(thisAbility, (a) => { attackCount++; return a; });

        casterCombat.stats.effects.recieveDamage.Add(dodgeFilter);
        casterCombat.stats.effects.addLastingEffectFor(attribute.DO, thisAbility, caster);
    }

    // Because Dodge_EveryN needs references to dodgeRate and attackCount, we can't make it a static function
    // Since we can only pass static functions into our table, we just have to make this function return a
    // factory that creates new lasting effects
    public static Lasting_Effect<Attribute> makeDodge_EveryN (GameObject _target)
    {
        var target = _target.GetComponent<Abilities>().q.GetComponent<Dodge_EveryN>();
        return new Lasting_Effect<Attribute>(
                    thisAbility,
                    (x, y) => {
                        if (target.dodgeRate == target.attackCount)
                        {
                            Debug.Log("Incite has taken effect!");
                            target.attackCount = 0;
                            return new Attribute(1.0,0.0,0.0,0.0); // Give 100% chance to dodge
                        }
                        else
                        {
                            return Attribute.zero();
                        }
                    }
                );
    }

    public override void registerEffects()
    {
        // Add the ability to the list
        Debug.Log("Dodge_EveryN added to dictionary");
        Effect_Management.Attribute_Manager.lastingEffects.Add(thisAbility, makeDodge_EveryN);
    }
}

using UnityEngine;
using System.Collections;

using AbilityHelp = Utility.AbilityHelp;
using TeamLogic = Utility.TeamLogic;
using System;
using Effect_Management;

public class Valgrind : Ability {

    const string abilityName = "Valgrind";

	public override Tuple<bool, Ability_Overlay> trigger()
    {
        GameObject target = AbilityHelp.getSelectable_UnderMouse();

        if (TeamLogic.areAllies(caster, target))
        {
            target.GetComponent<Combat>().stats.effects.removeHarmfulEffects();
            target.GetComponent<Combat>().stats.effects.addTimedEffectFor(attribute.AD, abilityName, target);
            var graphic = PhotonNetwork.Instantiate("Valgrind_Graphic", target.transform.position, Quaternion.identity, 0);
            graphic.GetComponent<Stick_On_Target>().target = target.transform;

            return new Tuple<bool, Ability_Overlay>(true, null);
        }
        else
        {
            return new Tuple<bool, Ability_Overlay>(false, null);
        }
        
    }

    public override Tuple<bool, Ability_Overlay> trigger_ai()
    {
        throw new NotImplementedException();
    }

    public override void passiveEffect()
    {
 	//throw new System.NotImplementedException();
    }

    public static Timed_Effect<Effect_Management.Attribute> Valgrind_attribute(GameObject target)
    {
        return new Timed_Effect<Effect_Management.Attribute>(
            new effectInfo(abilityName, EffectType.Buff, 1, 5.0, DateTime.Now),
            Attribute_Effects.changeBy(2.0),
            () => { }
        );
    }

    public override void registerEffects()
    {
        Effect_Management.Attribute_Manager.timedEffects.Add(abilityName, Valgrind_attribute);
    }
}

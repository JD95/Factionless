using UnityEngine;
using System;
using System.Collections;

using AbilityHelp = Utility.AbilityHelp;
using TeamLogic = Utility.TeamLogic;

using Effect_Management;

public class MemoryLeak : Ability {

    public override Tuple<bool, Ability_Overlay> trigger()
    {
        
        // Select target
        GameObject target = AbilityHelp.getSelectable_UnderMouse();

        // Check that they are enemy
        if (target == null) return new Tuple<bool, Ability_Overlay>(false, null);

        if (!TeamLogic.areEnemies(target, caster)) return new Tuple<bool, Ability_Overlay>(false, null);

        // Apply debuff
        target.GetComponent<Combat>().stats.effects.addTimedEffectFor(attribute.MaxMP, "Memory Leak", target);

        // Damage Target
        target.GetComponent<Combat>().recieve_Damage_Magic(5);

        var graphic = PhotonNetwork.Instantiate("MemoryLeak_Graphic", target.transform.position, Quaternion.identity,0);
        graphic.GetComponent<Stick_On_Target>().target = target.transform;

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

    public static Timed_Effect<Effect_Management.Attribute> memoryLeak(GameObject target)
    {
        return new Timed_Effect<Effect_Management.Attribute>(
            new effectInfo("Memory Leak", EffectType.Posion, 1, 10.0, DateTime.Now),
            Attribute_Effects.changeBy_percent(-0.50),
            () => {});
    }
    


    public override void registerEffects()
    {
        Effect_Management.Attribute_Manager.timedEffects.Add("Memory Leak", memoryLeak);
    }

}

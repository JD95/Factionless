using UnityEngine;
using System.Collections;
using System;

public class DNS : Ability {

	public override Tuple<bool, Ability_Overlay> trigger()
    {
        var targetLocation = Utility.AbilityHelp.getTerrain_UnderMouse();

        // if area is visible

        caster.transform.position = targetLocation;
        caster.GetComponent<Navigation>().moveTo(targetLocation);

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
        //throw new System.NotImplementedException();
    }
}

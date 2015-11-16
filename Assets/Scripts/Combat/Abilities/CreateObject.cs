using UnityEngine;
using System.Collections;

using Utility;
using System;

public class CreateObject : Ability {

    public override Tuple<bool, Ability_Overlay> trigger()
    {
        //Create the object in the scene
        var obj = PhotonNetwork.Instantiate(spawnName, caster.transform.position, Quaternion.identity, 0);
        obj.tag = caster.tag;

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

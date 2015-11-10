using UnityEngine;
using System.Collections;


using AbilityHelp = Utility.AbilityHelp;

public class Cache_Hits : Ability {

    public override Tuple<bool, Ability_Overlay> trigger()
    {
        Vector3 target = AbilityHelp.getTerrain_UnderMouse();

        //var launcher = GetComponentInChildren<Projectile_Launcher>().transform;

        var wave = PhotonNetwork.Instantiate("Projectile_CacheHit", caster.transform.position, caster.transform.rotation, 0).GetComponent<CacheHit_Projectile>();

        wave.target = target;

        wave.caster = caster;

        return new Tuple<bool, Ability_Overlay>(true, null);
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


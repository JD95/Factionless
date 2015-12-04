using UnityEngine;
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

		var shadow_slash_graphic = PhotonNetwork.Instantiate ("ShadowSlash_Graphic", Vector3.zero, Quaternion.identity,0);
		shadow_slash_graphic.GetComponent<Stick_On_Target> ().target = caster.transform;
		caster.GetComponent<Character> ().graphics.addTimedEffectFor (graphic.Body, "Shadow Slash", shadow_slash_graphic);

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
		Effect_Management.Graphics_Manager.timedEffects.Add("Shadow Slash", make_shadowSlash_Graphic);
    }

    public static Timed_Effect<Effect_Management.Attribute> shadowSlash(GameObject target)
    {
        return new Timed_Effect<Effect_Management.Attribute>(
                    new effectInfo("Shadow Slash", EffectType.Bleed, 1, 10.0, DateTime.Now),
                    Attribute_Effects.periodic_changeBy(1.0, -5.0),
                    Utility_Effects.doNothing_Stop()
                );
    }

	public Timed_Effect<Graphical> make_shadowSlash_Graphic(GameObject target)
	{
		return new Timed_Effect<Graphical>(
			new effectInfo("Shadow Slash", EffectType.Bleed, 1, 5.0, DateTime.Now),
			(t,s) => Effect_Management.Graphical.zero(),
			() => {
			foreach (var sys in target.GetComponentsInChildren<ParticleSystem>())sys.Stop();
		
			});
	}
}

using UnityEngine;
using System;
using System.Collections;

using Utility;
using Effect_Management;

public class Rotate_Matrix : Ability {

	public const string thisAbility = "Rotate Matrix";
	public override Tuple<bool, Ability_Overlay> trigger ()
	{
		//throw new System.NotImplementedException ();
		var character = caster.GetComponent<Character> ();

		var targets = Utility.TeamLogic.enemyCombatsInRange (caster, 5.0f);

		character.graphics.addTimedEffectFor (graphic.Rotation, thisAbility, caster);

		foreach (var target in targets) 
		{
			target.recieve_Damage_Physical(5.0f);
		}

		return new Tuple<bool, Ability_Overlay>(true, null);
	}

    public override Tuple<bool, Ability_Overlay> trigger_ai()
    {
        throw new NotImplementedException();
    }

    public override void passiveEffect ()
	{
		//throw new System.NotImplementedException ();
	}

	public static Effect_Management.Timed_Effect<Graphical> rotate_Matrix(GameObject caster)
	{
		return new Timed_Effect<Graphical> (
			new effectInfo(thisAbility, EffectType.Buff, 1, 1.0, DateTime.Now),
			Graphics_Effects.rotate_nTimes(caster,10),
			()=>{}
		);
	}

	public override void registerEffects ()
	{
		Effect_Management.Graphics_Manager.timedEffects.Add(thisAbility, rotate_Matrix);
	}
}

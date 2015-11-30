using UnityEngine;
using System.Collections;
using System;

using Utility;

public class Vertex_Shader : Ability {

    public override Tuple<bool, Ability_Overlay> trigger()
    {
        // Find enemies within radius
        var enemies = TeamLogic.enemyObjsInRange(caster, 15f);

		if (enemies != null) {
			// Apply translate effect towards cliburn's position
			foreach (var enemy in enemies) {
				var e_char = enemy.GetComponent<Character> ();
				var e_char_graphics = e_char.graphics;
				e_char_graphics.addTimedEffectFor (graphic.Postion, "TranslateTo", enemy);
			}
		}
        // Scale Cliburn
        // Rotate Cliburn
        return new Tuple<bool, Ability_Overlay>(true, null);
    }

    public override Tuple<bool, Ability_Overlay> trigger_ai()
    {
        throw new NotImplementedException();
    }

    public override void passiveEffect()
    {
        //throw new NotImplementedException();
    }

    public override void registerEffects()
    {
       // throw new NotImplementedException();
    }
}

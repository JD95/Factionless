using UnityEngine;
using System.Collections;
using System;
using System.Linq;

using Utility;

public class Vertex_Shader : Ability {

    public override Tuple<bool, Ability_Overlay> trigger()
    {
        // Find enemies within radius
        var enemies = TeamLogic.enemyObjsInRange(caster, 15f).Where(x => !x.GetComponent<Character>().isBase);

		if (enemies != null) {
			// Apply translate effect towards cliburn's position
			foreach (var enemy in enemies) {
				var e_char = enemy.GetComponent<Character> ();
                Debug.Log("Vertex Shader caught: " + enemy.name);
				var e_char_graphics = e_char.graphics;
				e_char_graphics.addTimedEffectFor (graphic.Postion, "TranslateTo", enemy);
			}
		}

        // Add scaling effect
        caster.GetComponent<Character>().graphics.addTimedEffectFor(graphic.Scale, "Scale Matrix", caster);

        // Increase AD
        caster.GetComponent<Combat>().stats.effects.addTimedEffectFor(attribute.AD, "Scale Matrix", caster);

        // Rotate Cliburn
        var targets = Utility.TeamLogic.enemyCombatsInRange(caster, 5.0f);

        caster.GetComponent<Character>().graphics.addTimedEffectFor(graphic.Rotation, "Rotate Matrix", caster);

        if (targets != null)
        {
            foreach (var target in targets)
            {
                target.recieve_Damage_Physical(5.0f);
            }
        }

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

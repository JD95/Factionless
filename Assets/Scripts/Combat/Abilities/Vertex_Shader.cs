using UnityEngine;
using System.Collections;
using System;

using Utility;

public class Vertex_Shader : Ability {

    public override bool trigger()
    {
        // Find enemies within radius
        var enemies = TeamLogic.enemyObjsInRange(caster, 10);

        // Apply translate effect towards cliburn's position
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Character>().graphics.addTimedEffectFor(graphic.Postion, Translate.abilityName, enemy);
        }
        // Scale Cliburn
        // Rotate Cliburn
        return true;
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

﻿using UnityEngine;
using System.Collections;
using System;

using Utility;

public class Vertex_Shader : Ability {

    public override Tuple<bool, Ability_Overlay> trigger()
    {
        // Find enemies within radius
        var enemies = TeamLogic.enemyObjsInRange(caster, 10);

        // Apply translate effect towards cliburn's position
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Character>().graphics.addTimedEffectFor(graphic.Postion, "TranslateTo", enemy);
        }
        // Scale Cliburn
        // Rotate Cliburn
        return new Tuple<bool, Ability_Overlay>(true, null);
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

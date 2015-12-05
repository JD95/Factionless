using UnityEngine;
using System.Collections;
using System;

public class Return_To_Camp : AI_Objective {

    Vector3 inital_position;
    Navigation nav;
    Combat combatData;

    public override bool begin()
    {
        return combatData.inRangeEnemies.Count == 0;
    }

    public override bool end()
    {
        return transform.position.AlmostEquals(inital_position, 5);
    }

    public override void init()
    {
        inital_position = transform.position;
        combatData = GetComponent<Combat>();
        nav = GetComponent<Navigation>();
    }

    public override void progress()
    {
        nav.moveTo(inital_position);
    }
}

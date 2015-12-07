using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Retreat : AI_Objective {

    private Combat ai;
    private Navigation nav;
    public  Transform retreat_point;



    public override bool begin()
    {
        var begin = ai.health / ai.maxHealth < 0.30;
        if (begin)
        {
            nav.set_navigation_lock(Nav_Lock.inCombat, false);
            nav.set_navigation_lock(Nav_Lock.withinRange, false);
            ai.hostile = false;
            ai.target = null;
        }
        return begin;
    }

    public override bool end()
    {
        var end = ai.health - ai.maxHealth < 0.5;
        if (end) ai.hostile = true;

        return end;
    }

    public override void init()
    {
        ai = GetComponent<Combat>();
        nav = GetComponent<Navigation>();
        retreat_point = GameObject.Find("Red_Retreat_Point").transform;
    }

    public override void progress()
    {
        nav.moveTo(retreat_point.position);
    }
}

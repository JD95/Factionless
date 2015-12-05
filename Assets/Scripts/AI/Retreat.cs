using UnityEngine;
using System.Collections;
using System;

public class Retreat : AI_Objective {

    private Combat ai;
    private Navigation nav;
    private Vector3 retreat_point;

    public override bool begin()
    {
        return ai.health / ai.maxHealth < 0.30;
    }

    public override bool end()
    {
        return ai.health == ai.maxHealth;
    }

    public override void init()
    {
        ai = GetComponent<Combat>();
        nav = GetComponent<Navigation>();
        retreat_point = GameObject.Find("Red_Retreat_Point").transform.position;
    }

    public override void progress()
    {
        nav.moveTo(retreat_point);
    }
}

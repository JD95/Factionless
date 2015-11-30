using UnityEngine;
using System.Collections;
using System;

public class Chase_Enemy : AI_Objective {

    protected Combat combatData;
    protected Navigation nav;

    public override void init()
    {
        combatData = GetComponent<Combat>();
        nav = GetComponent<Navigation>();
    }

    public override bool begin()
    {
        return combatData.target != null && !combatData.targetWithin_AttackRange();
    }

    public override void progress()
    {
        nav.moveTo(combatData.target.transform.position, combatData.attackRange());
    }

    public override bool end()
    {
        return combatData.target == null || combatData.targetWithin_AttackRange();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

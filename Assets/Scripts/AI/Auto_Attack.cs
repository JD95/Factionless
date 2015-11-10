using UnityEngine;
using System.Collections;
using System;

public class Auto_Attack : AI_Objective {

    protected Combat combatData;
    protected Navigation nav;

    public override void init()
    {
        combatData = GetComponent<Combat>();
        nav = GetComponent<Navigation>();
    }

    public override bool begin()
    {
        bool test = combatData.target != null && combatData.targetWithin_AttackRange();

        if (test && nav != null)
        {
            nav.toggle_navigation_lock(Nav_Lock.withinRange);
            nav.toggle_navigation_lock(Nav_Lock.inCombat);
        }
        return test;
    }

    public override void progress()
    {
        transform.LookAt(combatData.target.transform);

        if (combatData.basicAttackCoolDown <= 0)
        {
            if (combatData.isRanged)
            {
                GetComponentInChildren<Projectile_Launcher>().fire(combatData.target);
                combatData.basicAttackCoolDown = combatData.attackSpeed();
            }
            else
            {
                combatData.cause_Damage_Physical(combatData.target.GetComponent<Combat>());
            }

            combatData.basicAttackCoolDown = combatData.attackSpeed();
        }
    }

    public override bool end()
    {
        bool test = !combatData.targetWithin_AttackRange();

        if (test)
        {   nav.toggle_navigation_lock(Nav_Lock.withinRange);
            nav.toggle_navigation_lock(Nav_Lock.inCombat);
        }
        return test;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

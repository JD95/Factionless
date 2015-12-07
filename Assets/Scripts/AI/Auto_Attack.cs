using UnityEngine;
using System.Collections;
using System;

public class Auto_Attack : AI_Objective {

    public Combat combatData;
    public Character character;
    public Navigation nav;

    public override void init()
    {
        combatData = GetComponent<Combat>();
        nav = GetComponent<Navigation>();
        character = GetComponent<Character>();
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
        if (combatData == null) return;

        transform.LookAt(combatData.target.transform);

        var hit_time = (float)(character.getAttack_Animation_Length() / (float)2.0);
        //Debug.Log("Hit time: " + hit_time);


        // Attack animations usually "hit" at half length, so we trigger the attack at the half way point
        if (combatData.basicAttackCoolDown <= hit_time)
        {
            //Debug.Log("Triggering Attack Animation!");
            character.triggerAnimation_state(character.attacking_State);
        }


        if (combatData.basicAttackCoolDown > 0) return;

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

    public override bool end()
    {
        bool test = combatData == null || combatData.target == null || !combatData.targetWithin_AttackRange() || combatData.target.GetComponent<Combat>().dead;

        if (test && nav != null)
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

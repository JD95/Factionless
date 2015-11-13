using UnityEngine;
using System;
using System.Collections;

using Utility;

public class Hero : Photon.MonoBehaviour
{
	public string heroTeam;

	private Character character;
	private Combat combatData;
    private Abilities abilities;
	

	// Navigation
	private Navigation navigation;
	public Waypoint targetLocation;


	void Start ()
	{
		character = GetComponent<Character> ();

		navigation = GetComponent<Navigation>();

		combatData = GetComponent<Combat>();

        abilities = GetComponent<Abilities>();
	}

	// Called every frame
	void Update ()
	{
        capture_input();

        if(combatData.targetWithin_AttackRange())
        {
            character.setAnimation_State(character.running_State, false);
            character.setAnimation_State(character.attacking_State, true);
            combatData.autoAttack();
        }
        else character.setAnimation_State(character.attacking_State, false);

    }

    void capture_input()
    {
        if (Input.GetButtonDown("Fire1")) select_target();
        if (Input.GetButtonDown("Fire2")) adjustDestination();
        else if (Input.GetKeyDown("q")) abilities.trigger_Ability(Slot.q);
        else if (Input.GetKeyDown("w")) abilities.trigger_Ability(Slot.w);
        else if (Input.GetKeyDown("e")) abilities.trigger_Ability(Slot.e);
        else if (Input.GetKeyDown("r")) abilities.trigger_Ability(Slot.r);

    }
	// Adjusts location based on new clicks
	void adjustDestination ()
	{
        var location = AbilityHelp.getTerrain_UnderMouse();
        navigation.moveTo(location, 0);
        combatData.target = null;
	}

    void select_target()
    {
        GameObject hit = AbilityHelp.getSelectable_UnderMouse();

        if (hit != null && hit != gameObject && Utility.TeamLogic.areEnemies(hit, gameObject))
        {
            combatData.target = hit;
        }
        else
        {
            combatData.target = null;
        }
    }

}

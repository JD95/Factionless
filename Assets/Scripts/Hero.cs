using UnityEngine;
using System;
using System.Collections;

using Utility;

public class Hero : Photon.MonoBehaviour
{
	public string heroTeam;

	private Character character;
	private Combat combatData;
	

	// Navigation
	private Navigation navigation;
	public Waypoint targetLocation;


	void Start ()
	{
		character = GetComponent<Character> ();

		navigation = GetComponent<Navigation>();

		combatData = GetComponent<Combat>();
	}

	// Called every frame
	void Update ()
	{
		adjustDestination();

        if(combatData.targetWithin_AttackRange())
        {
            character.setAnimation_State(character.running_State, false);
            character.setAnimation_State(character.attacking_State, true);
            combatData.autoAttack();
        }
        else character.setAnimation_State(character.attacking_State, false);

    }

	// Adjusts location based on new clicks
	void adjustDestination ()
	{
		if (Input.GetButtonDown ("Fire1")) { //Debug.Log ("Click!");	

			Tuple<Vector3, double> clicked =  filterClick(Input.mousePosition);
			navigation.moveTo(clicked.First, clicked.Second);

		}
	
	}

	 // Checks for obstacles in the current path to clicked location 
    Tuple<Vector3, double> filterClick(Vector2 point)
	{
        GameObject hit;

        hit = AbilityHelp.getSelectable_UnderMouse();

        if (hit != null && hit != gameObject && Utility.TeamLogic.areEnemies(hit, gameObject))
        {
             combatData.target = hit;
             return new Tuple<Vector3, double>(hit.transform.position, combatData.attackRange());
        }
        else 
        {
            return new Tuple<Vector3, double>(AbilityHelp.getTerrain_UnderMouse(), 0);
        }

	}

}

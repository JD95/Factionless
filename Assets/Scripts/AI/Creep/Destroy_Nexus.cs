using UnityEngine;
using System.Collections;

using TeamLogic = Utility.TeamLogic;

public class Destroy_Nexus : AI_Objective {

	Combat combatData;
	Transform nexus;
	NavMeshAgent movement;
    Soldier ai;

    int travelPoint = 0;

	public override void init()
	{
		combatData = gameObject.GetComponent<Combat>();
		movement = gameObject.GetComponent<NavMeshAgent>();

        ai = GetComponent<Soldier>();
	}

	public override bool begin()
	{
		// This is a default behaviour, don't really need to define this
		return true;
	}

	public override void progress()
	{
        Transform destination = ai.objectivePath[travelPoint];

        if(Vector3.Distance(transform.position, destination.position) <= 20.0 && travelPoint != 0)
        {
            destination = ai.objectivePath[++travelPoint];
        }

        movement.SetDestination(destination.position);

    }

	public override bool end()
	{
		// Nexus is destroyed
		return false;
	}


}

using UnityEngine;
using System.Collections;

using TeamLogic = Utility.TeamLogic;

public class Destroy_Nexus : AI_Objective {

	Combat combatData;
	Transform nexus;
	NavMeshAgent movement;

	Vector3 nexusPosition;

	public override void init()
	{
		combatData = gameObject.GetComponent<Combat>();
		movement = gameObject.GetComponent<NavMeshAgent>();
		
		nexus = GameObject.Find(targetNexus()).GetComponent<Transform>();

		nexusPosition = nexus.transform.position;

	}

	public override bool begin()
	{
		// This is a default behaviour, don't really need to define this
		return true;
	}

	private string targetNexus()
	{
		if(gameObject.CompareTag(TeamLogic.TeamA))
		{
			return "NexusB";
		}
		else{
			return "NexusA";
		}
	}

	public override void progress()
	{
		if(!movement.destination.AlmostEquals(nexusPosition, 1.0F))
		{
			//Debug.Log("Target is now set to nexus");
			movement.SetDestination(nexus.position);
			movement.Resume();
		}

		// When in range of Nexus auto attack it
	}

	public override bool end()
	{
		// Nexus is destroyed
		return false;
	}


}

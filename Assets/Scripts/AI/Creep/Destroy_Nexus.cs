﻿using UnityEngine;
using System.Collections.Generic;

using TeamLogic = Utility.TeamLogic;

public interface HasObjectivePath
{
    List<Transform> getObjectivePath();
}

public class Destroy_Nexus : AI_Objective {

	Combat combatData;
	Transform nexus;
	NavMeshAgent movement;
    HasObjectivePath ai;

    int travelPoint = 0;

	public override void init()
	{
		combatData = gameObject.GetComponent<Combat>();
		movement = gameObject.GetComponent<NavMeshAgent>();

        ai = GetComponent<HasObjectivePath>();
	}

	public override bool begin()
	{
		// This is a default behaviour, don't really need to define this
		return true;
	}

	public override void progress()
	{
        Transform destination = ai.getObjectivePath()[travelPoint];

        if(Vector3.Distance(transform.position, destination.position) <= 20.0 && travelPoint != ai.getObjectivePath().Count - 1)
        {
			//Debug.Log("Chaning destination for creep!");
            destination = ai.getObjectivePath()[++travelPoint];
        }

        movement.SetDestination(destination.position);

    }

	public override bool end()
	{
		// Nexus is destroyed
		return false;
	}


}

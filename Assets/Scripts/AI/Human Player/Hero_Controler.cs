using UnityEngine;
using System.Collections.Generic;
using System;

public class Hero_Controler : AI {

	// Use this for initialization
	void Start () {
        active_Objectives = new Stack<AI_Objective>();
        secondary_Objectives = new List<AI_Objective>();

        // The main objective for creeps
        //main_Objective = createObjective<Destroy_Nexus>();
        main_Objective = createObjective<Idle>();
        active_Objectives.Push(main_Objective);

        // All other objectives
        fillSecondaryObjectives();
    }
	
	// Update is called once per frame
	void Update () {
        runObjectives();
    }

    protected override void fillSecondaryObjectives()
    {
        secondary_Objectives.Add(createObjective<Auto_Attack>());
    }
}

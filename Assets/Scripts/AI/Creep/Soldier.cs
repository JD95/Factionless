using UnityEngine;
using System.Linq;
using System.Collections.Generic;

using Utility;

public class Soldier : AI {

    public List<string> secondObjectivesNames = new List<string>();
    public List<string> activeObjectiveNames = new List<string>();

    public int laneAssignment;


    public List<Transform> objectivePath;

    void Start ()
	{
		active_Objectives = new Stack<AI_Objective>();
		secondary_Objectives = new List<AI_Objective>();

        // The main objective for creeps
        main_Objective = createObjective<Destroy_Nexus>();
		main_Objective.turnOn();
		active_Objectives.Push(main_Objective);
		
		// All other objectives
		fillSecondaryObjectives();
		
	}

	private List<string> convertStack()
	{	
		List<string> newList = new List<string>();
		
		foreach (var item in active_Objectives)
		{
			newList.Add(item.ToString());
		}
		
		return newList;
	}

	private List<string> convertList()
	{	
		List<string> newList = new List<string>();
		
		secondary_Objectives.ForEach(x => newList.Add(x.ToString()));
		
		return newList;
	}

	void Update()
	{
		runObjectives();
		secondObjectivesNames = convertList();
		activeObjectiveNames = convertStack();
	}

	void OnDestroy()
	{
		var objectives = gameObject.GetComponents<AI_Objective>();

		foreach(var objective in objectives)
		{
			GameObject.Destroy(objective);
		}
	}

	// This is where the creep's secondary objectives are added
	protected override void  fillSecondaryObjectives()
	{
		//secondary_Objectives.Add(createObjective<Destroy_Nexus>());
		secondary_Objectives.Add(createObjective<Auto_Attack>());
        secondary_Objectives.Add(createObjective<Chase_Enemy>());
    }
}

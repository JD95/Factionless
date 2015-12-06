using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class Hubert : AI, HasObjectivePath {

    Combat combatData;

    public bool useTranslate;

    public List<Transform> objectivePath;
    public List<Transform> getObjectivePath() { return objectivePath; }


    public List<string> activeObjectiveNames = new List<string>();

    private List<string> convertStack()
    {
        List<string> newList = new List<string>();

        foreach (var item in active_Objectives)
        {
            newList.Add(item.ToString());
        }

        return newList;
    }

    protected override void fillSecondaryObjectives()
    {
		secondary_Objectives.Add(createObjective<Auto_Attack>());
		secondary_Objectives.Add(createObjective<Chase_Enemy>());
        secondary_Objectives.Add(createObjective<Retreat>());
    }
	
	// Use this for initialization
    void Start () {
        combatData = GetComponent<Combat>();
        active_Objectives = new Stack<AI_Objective>();
        secondary_Objectives = new List<AI_Objective>();

        // The main objective for creeps
        main_Objective = createObjective<Destroy_Nexus>();
        main_Objective.turnOn();
        active_Objectives.Push(main_Objective);

        // All other objectives
        fillSecondaryObjectives();
    }
	
	// Update is called once per frame
	void Update () {

        runObjectives();
        activeObjectiveNames = convertStack();

    }
}

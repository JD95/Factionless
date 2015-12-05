using UnityEngine;
using System.Collections.Generic;

public class NeutralMob : AI {

    void Start()
    {
        active_Objectives = new Stack<AI_Objective>();
        secondary_Objectives = new List<AI_Objective>();

        // The main objective for creeps
        main_Objective = createObjective<Idle>();
        main_Objective.turnOn();
        active_Objectives.Push(main_Objective);

        // All other objectives
        fillSecondaryObjectives();

    }

    void Update()
    {
        runObjectives();
    }

    void OnDestroy()
    {
        var objectives = gameObject.GetComponents<AI_Objective>();

        foreach (var objective in objectives)
        {
            GameObject.Destroy(objective);
        }
    }

    // This is where the creep's secondary objectives are added
    protected override void fillSecondaryObjectives()
    {
        //secondary_Objectives.Add(createObjective<Destroy_Nexus>());
        secondary_Objectives.Add(createObjective<Auto_Attack>());
        secondary_Objectives.Add(createObjective<Chase_Enemy>());
        secondary_Objectives.Add(createObjective<Return_To_Camp>());
    }
}


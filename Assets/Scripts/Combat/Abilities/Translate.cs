using UnityEngine;
using System.Collections;

using AbilityHelp = Utility.AbilityHelp;

public class Translate : Ability {

    public override bool trigger()
    {
        GameObject selected = AbilityHelp.getSelectable_UnderMouse();
        Navigation nav = caster.GetComponent<Navigation>();

        if (selected == null) { return false; } // Ability needs a target
        else
        {
            // Turn off navigation
            nav.turnOn_Channeling();

            // Move the model through other models within range of target

            nav.disableMeshAgent(); // Allows caster to pass through objects

            /* Add a timed world effect which moves the model
             * towards the location of the target (when cast)
             * until it reaches that location. Then it renables navigation.
             */ 

            nav.enableMeshAgent();

            // Reactivate navigation
            nav.turnOff_Channeling();

            return true;
        }
    }

    public override void passiveEffect()
    {
        //throw new System.NotImplementedException();
    }

    public override void registerEffects()
    {
        //throw new System.NotImplementedException();
    }
}

using UnityEngine;
using System.Collections;

/*
 *  We need the game objects to make constructing champs and
 *  their abilities simpler.
 * 
 */
 
public enum Slot { q, w, e, r };

public class Ability_Overlay
{


}
     
public class Abilities : MonoBehaviour {

    private Combat combatData;

    // Q ABILITY
    public Ability q;
    public int      q_Level;
    public bool     q_UsePassive;
    public float[]  q_ResourceCost;

    // W ABILITY
    public Ability w;
    public int      w_Level;
    public bool     w_UsePassive;
    public float[]  w_ResourceCost;

    // E ABILITY
    public Ability e;
    public int      e_Level;
    public bool     e_UsePassive;
    public float[]  e_ResourceCost;

    // R ABILITY
    public Ability r;
    public int      r_Level;
    public bool     r_UsePassive;
    public float[]  r_ResourceCost;

    void Start()
    {
        combatData = GetComponent<Combat>();

        initAbility(ref q);
        initAbility(ref w);
        initAbility(ref e);
        initAbility(ref r);

        applyPassives();


    }

    void initAbility(ref Ability ability)
    {
        ability.setCaster(gameObject);
        ability.registerEffects();
    }

    void Update()
    {


        
    }


    void applyPassives()
    {
        if (q_UsePassive) q.passiveEffect();
        if (w_UsePassive) w.passiveEffect();
        if (e_UsePassive) e.passiveEffect();
        if (r_UsePassive) r.passiveEffect();
    }



    // Returns a bool if the ability was actually triggered or not
    // eg. if you have a target spell, but did not target anything
    bool useAbility(Ability ability, int level, float[] resourceCost)
    {
        bool haveEnoughMana = combatData.mana - resourceCost[level] >= 0;

        if (haveEnoughMana && ability.trigger())
        {
            consumeResource(resourceCost[level]);
            return true;
        }
        else
        {
            return false; // Ability was not used
        }
    }

    void consumeResource(float cost)
    {
        combatData.mana -= cost;
    }

}

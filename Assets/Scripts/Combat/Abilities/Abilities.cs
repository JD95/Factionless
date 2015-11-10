using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/*
 *  We need the game objects to make constructing champs and
 *  their abilities simpler.
 * 
 */

public enum Slot { q, w, e, r };
public delegate Tuple<bool, Ability_Overlay> ability_override();

public class Ability_Overlay
{
    ability_override[] overrides = new ability_override[4];

    public Ability_Overlay(ability_override q_override = null, ability_override w_override = null, 
                           ability_override e_override = null, ability_override r_override = null)
    {
        overrides[(int)Slot.q] = q_override;
        overrides[(int)Slot.w] = w_override;
        overrides[(int)Slot.e] = e_override;
        overrides[(int)Slot.r] = r_override;
    }

    public ability_override get_override(Slot button)
    {
        var call = overrides[(int)button];

        if (call != null) return call;

        else return null;
    }

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

    Stack<Ability_Overlay> ability_overlay = new Stack<Ability_Overlay>();

    void Start()
    {
        combatData = GetComponent<Combat>();

        initAbility(q);
        initAbility(w);
        initAbility(e);
        initAbility(r);

        applyPassives();
    }

    void initAbility(Ability ability)
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

    // Returns true if the ability actually triggered
    public bool trigger_Ability(Slot button)
    {
        ability_override current_override = get_override(button);

        if(current_override != null) return get_ability_result(current_override);

        switch(button)
        {
            case Slot.q: return useAbility(q, q_Level, q_ResourceCost);
            case Slot.w: return useAbility(w, w_Level, w_ResourceCost);
            case Slot.e: return useAbility(e, e_Level, e_ResourceCost);
            case Slot.r: return useAbility(r, r_Level, r_ResourceCost);
        }

        return false;
    }

    ability_override get_override(Slot button)
    {
        foreach (var overlay in ability_overlay)
        {
            ability_override new_override = overlay.get_override(button);
            if (new_override != null) return new_override;
        }

        return null;
    }

    // Returns a bool if the ability was actually triggered or not
    // eg. if you have a target spell, but did not target anything
    bool useAbility(Ability ability, int level, float[] resourceCost)
    {
        bool haveEnoughMana = combatData.mana - resourceCost[level] >= 0;

        if (haveEnoughMana)
        {
            bool triggered = get_ability_result(ability.trigger);
            if (triggered) consumeResource(resourceCost[level]);
            return triggered;
        }

        else return false; // Ability was not used

    }

    void consumeResource(float cost)
    {
        combatData.mana -= cost;
    }

    bool get_ability_result(ability_override call)
    {
        Tuple<bool, Ability_Overlay> result = call();

        bool triggerd = result.First;
        Ability_Overlay new_overlay = result.Second;

        if (triggerd)
        {
            if (new_overlay != null) ability_overlay.Push(new_overlay);
            else ability_overlay.Pop();
        }

        return triggerd;
    }

}

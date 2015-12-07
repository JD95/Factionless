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
    public int     q_Level;
    public bool    q_UsePassive;
    public float[] q_ResourceCost;
    public float[] q_Cooldowns;
    public float q_current_cooldown;
    public string q_description;

    // W ABILITY
    public Ability w;
    public int     w_Level;
    public bool    w_UsePassive;
    public float[] w_ResourceCost;
    public float[] w_Cooldowns;
    public float w_current_cooldown;
    public string w_description;

    // E ABILITY
    public Ability e;
    public int     e_Level;
    public bool    e_UsePassive;
    public float[] e_ResourceCost;
    public float[] e_Cooldowns;
    public float e_current_cooldown;
    public string e_description;

    // R ABILITY
    public Ability r;
    public int     r_Level;
    public bool    r_UsePassive;
    public float[] r_ResourceCost;
    public float[] r_Cooldowns;
    public float r_current_cooldown;
    public string r_description;

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
        if (ability == null) return;

        Debug.Log("init abilities for" + gameObject.name);
        ability.setCaster(gameObject);
        ability.registerEffects();
    }

    float tick_cooldown(float cd)
    {
        return cd > 0 ? cd - Time.deltaTime : 0;
    }

    void tick_cooldowns()
    {
        q_current_cooldown = tick_cooldown(q_current_cooldown);
        w_current_cooldown = tick_cooldown(w_current_cooldown);
        e_current_cooldown = tick_cooldown(e_current_cooldown);
        r_current_cooldown = tick_cooldown(r_current_cooldown);
    }

    void Update()
    {
        tick_cooldowns();
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
            case Slot.q: return q_current_cooldown <= 0 ? useAbility(button, q, q_Level, q_ResourceCost) : false;
            case Slot.w: return w_current_cooldown <= 0 ? useAbility(button, w, w_Level, w_ResourceCost) : false;
            case Slot.e: return e_current_cooldown <= 0 ? useAbility(button, e, e_Level, e_ResourceCost) : false;
            case Slot.r: return r_current_cooldown <= 0 ? useAbility(button, r, r_Level, r_ResourceCost) : false;
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
    bool useAbility(Slot button, Ability ability, int level, float[] resourceCost)
    {
        bool haveEnoughMana = combatData.mana - resourceCost[level] >= 0;

        if (haveEnoughMana)
        {
            bool triggered;
            if (!combatData.is_ai)
            {
                triggered = get_ability_result(ability.trigger);
            }
            else
            {
                triggered = get_ability_result(ability.trigger_ai);
            }

            if (triggered)
            {
                Debug.Log("Ability Triggered!");
                consumeResource(resourceCost[level]);
                start_cooldown(button);
            }
            return triggered;
        }

        else return false; // Ability was not used

    }

    void start_cooldown(Slot button)
    {
        switch (button)
        {
            case Slot.q: q_current_cooldown = q_Cooldowns[q_Level]; break;
            case Slot.w: w_current_cooldown = w_Cooldowns[w_Level]; break;
            case Slot.e: e_current_cooldown = e_Cooldowns[e_Level]; break;
            case Slot.r: r_current_cooldown = r_Cooldowns[r_Level]; break;
        }
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
            if (new_overlay != null)
            {
                //Debug.Log("Pushing ability overlay");
                ability_overlay.Push(new_overlay);
            }
            else
            {
                //Debug.Log("Popping ability overlay");
                if (ability_overlay.Count > 0)
                    ability_overlay.Pop();
            }
        }

        return triggerd;
    }

    public string get_ability_description(Slot slot)
    {
        switch(slot)
        {
            case Slot.q: return q_description;
            case Slot.w: return w_description;
            case Slot.e: return e_description;
            case Slot.r: return r_description;
        }

        return "";
    }

}

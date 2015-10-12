using UnityEngine;
using System.Collections;
using System;

using Effect_Management;
using Atrtribute = Effect_Management.Attribute;

public class Scale_Matrix : Ability {

    const string abilityName = "Scale Matrix";
    public override bool trigger()
    {
        Character character = caster.GetComponent<Character>();
        Combat combat = caster.GetComponent<Combat>();

        // Add scaling effect
        character.graphics.addTimedEffectFor(graphic.Scale, abilityName, caster);

        // Increase AD
        combat.stats.effects.addTimedEffectFor(attribute.AD, abilityName, caster);

        return true;
    }

    public static Timed_Effect<Graphical> scaleMatrix_Graphic(GameObject caster)
    {
        Vector3 currentSize = caster.transform.localScale;
        Vector3 targetSize = caster.transform.localScale * 2;

        return new Timed_Effect<Graphical>(
            new effectInfo(abilityName, EffectType.Buff, 1, 15.0, DateTime.Now),
            Graphics_Effects.phased_Scale(caster, currentSize, targetSize, 2.5F, 10F),
            () => { caster.transform.localScale = currentSize; }
        );
    }

    public static Timed_Effect<Effect_Management.Attribute> scaleMatrix_attribute(GameObject target)
    {
        return new Timed_Effect<Effect_Management.Attribute>(
            new effectInfo(abilityName, EffectType.Buff, 1, 10.0, DateTime.Now),
            Attribute_Effects.changeBy(5.0),
            () => { }
        );
    }

    public override void passiveEffect()
    {
        //throw new System.NotImplementedException();
    }

    public override void registerEffects()
    {
        Effect_Management.Attribute_Manager.timedEffects.Add(abilityName, scaleMatrix_attribute);
        Effect_Management.Graphics_Manager.timedEffects.Add(abilityName, scaleMatrix_Graphic);
    }
}

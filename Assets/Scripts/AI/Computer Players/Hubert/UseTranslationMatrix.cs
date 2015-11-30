using UnityEngine;
using System.Collections;
using System;

public class UseTranslationMatrix : AI_Objective {
    Hubert controller;
    Abilities abilities;

    public override void init()
    {
        controller = GetComponent<Hubert>();
        abilities = GetComponent<Abilities>();
    }

    public override bool end()
    {
        return !(controller.useTranslate);
    }

    public override void progress()
    {
        // select target
        abilities.trigger_Ability(Slot.w);
        controller.useTranslate = false;
    }

    public override bool begin()
    {
        return (controller.useTranslate);
    }

}

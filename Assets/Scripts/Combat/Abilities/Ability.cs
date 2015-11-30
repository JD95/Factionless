using UnityEngine;
using System.Collections;

public abstract class Ability  : Photon.MonoBehaviour
{
    protected GameObject caster;

    public string   spawnName;
    public float    cooldownTime;

    public abstract void registerEffects();
    public abstract Tuple<bool, Ability_Overlay> trigger();
    public abstract Tuple<bool, Ability_Overlay> trigger_ai();
    public abstract void passiveEffect();

    public void setCaster(GameObject _caster)
    {
        caster = _caster;
    }

}

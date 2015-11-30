using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

using TeamLogic = Utility.TeamLogic;
using transformData = Utility.transformData;
using VectorHelp = Utility.VectorHelp;
using Effect_Management;


public class ShallowCopies : Ability {

    const int   abilityLevel = 4;
    const float cloneSpawnDistance = 1.0F;
    const float runDistance = 10F;

    List<GameObject> clones = new List<GameObject>();

    public override Tuple<bool, Ability_Overlay> trigger()
    {
        Vector3 center     = caster.transform.position;
        Vector3 realTarget = Utility.AbilityHelp.getTerrain_UnderMouse();
        Vector3 firstPoint = Vector3.MoveTowards(center, realTarget, cloneSpawnDistance);

        caster.transform.position = firstPoint;
        caster.transform.LookAt(realTarget);

        clones = makeClonesAt(spawnPositions(abilityLevel, center, firstPoint));

        setDestinations(center, realTarget);
        
        return new Tuple<bool, Ability_Overlay>(true, abilityOverride());
    }

    public override Tuple<bool, Ability_Overlay> trigger_ai()
    {
        throw new NotImplementedException();
    }

    private List<Utility.transformData> spawnPositions(int level, Vector3 center, Vector3 firstPoint)
    {
        List<transformData> transforms = new List<transformData>();

        for (int i = 1; i < level; i++)
        {
            Vector3 angle = new Vector3(0,i * (360/level),0);
            Vector3 nextPosition = VectorHelp.RotatePointAroundPivot(firstPoint, center, angle);
            transforms.Add(new transformData(nextPosition, angle));
        }

        return transforms;
    }

    private List<GameObject> makeClonesAt(List<Utility.transformData> tDatas)
    {
        List<GameObject> clones = new List<GameObject>();

        foreach (var tData in tDatas)
        {
            var clone = PhotonNetwork.Instantiate("Gao Clone", tData.position, Quaternion.Euler(tData.rotation), 0);
            clone.transform.eulerAngles += caster.transform.eulerAngles;

            //clone.GetComponent<CharacterController>().enabled = true;
            clone.GetComponent<Navigation>().Init();

            clones.Add(clone);
        }

        return clones;
    }

    private void setDestinations(Vector3 center, Vector3 realTarget)
    {
        var destinations = createDestinations(center, realTarget);

        caster.GetComponent<Navigation>().moveTo(destinations[0]);

        int destinationIndex = 1;
        foreach(var clone in clones)
        {     
            clone.GetComponent<Navigation>().moveTo(destinations[destinationIndex++]);
        }
    }

    private Vector3[] createDestinations(Vector3 center, Vector3 realTarget)
    {
        Vector3[] destinations = Enumerable.Repeat(realTarget, abilityLevel).ToArray();

        for (int i = 0; i < destinations.Length; i++)
        {
            destinations[i] = VectorHelp.RotatePointAroundPivot(realTarget, center, new Vector3(0, i * (360 / abilityLevel)));
        }

        return destinations;
    }

    public Ability_Overlay abilityOverride()
    {
        var result = new Tuple<bool, Ability_Overlay>(true, null);

        return new Ability_Overlay(

            () => { this.trigger_Default(); return result; },

            () => { this.trigger_WaywardNightmare(); return result; },

            () => { this.trigger_ShadowSlash(); return result; },
          
            () => { this.trigger_Default(); return result; }
        );
    }

    private void trigger_WaywardNightmare()
    {
        foreach (var clone in clones)
        {
            var explosionRange = Utility.TeamLogic.enemyCombatsInRange(clone, 5.0f);

            foreach (var enemy in explosionRange)
            { enemy.recieve_Damage_Physical(5.0f); }

            PhotonNetwork.Instantiate("Explosion", clone.transform.position, clone.transform.rotation, 0);

            GameObject.Destroy(clone);
        }

        clones = null;
    }

    private void trigger_ShadowSlash()
    {
        foreach (var clone in clones)
        {
            var toSlice = TeamLogic.enemyCombatsInRange(clone, 5.0f);

            foreach (var enemy in toSlice)
            {
                enemy.recieve_Damage_Physical(1.0f);
                enemy.stats.effects.addTimedEffectFor(attribute.HP, "Shadow Slash", null);
            }

            PhotonNetwork.Instantiate("Explosion", clone.transform.position, clone.transform.rotation, 0);

            GameObject.Destroy(clone);
        }

    }

    private void trigger_Default()
    { 
        foreach(var clone in clones)
        {
            PhotonNetwork.Instantiate("Explosion", clone.transform.position, clone.transform.rotation, 0);

            GameObject.Destroy(clone);
        }
    }

    public override void passiveEffect()
    {
        // Do Nothing
    }

    public override void registerEffects()
    {
        //throw new NotImplementedException();
    }
}

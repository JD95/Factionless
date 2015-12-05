using UnityEngine;
using System.Collections;

using Utility;

public class Fountain_Heal : MonoBehaviour {

    float time_until_heal = 0;
    const float heal_interval = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        time_until_heal -= Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Colliding with " + other.name);

        if (other.name == "AI_Collider") return;

        if(TeamLogic.areAllies(other.gameObject, gameObject) && time_until_heal <= 0)
        {
            other.gameObject.GetComponent<Combat>().recieve_Healing_Direct(10);
        }
    }
}

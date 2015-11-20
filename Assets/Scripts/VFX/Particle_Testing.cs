using UnityEngine;
using System.Collections;

public class Particle_Testing : MonoBehaviour {

	public void heal()
	{
		GameObject.Find ("player").GetComponent<Combat> ().recieve_Healing(1);
	}

	public void damage()
	{
		GameObject.Find ("player").GetComponent<Combat> ().recieve_Damage_Physical (1);
	}
}

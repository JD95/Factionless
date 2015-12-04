using UnityEngine;
using System.Collections;

public enum Particle {Heal=0, Stun, Bleed, Slow, Sheild};

public class ParticlePre : MonoBehaviour {

	private const int sizeof_effects = 5;
	public ParticleEffect[] effects;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playEffect(Particle id, int time = 0)
	{
		if(time == 0)
		//Debug.Log ("playing particle at " + id.ToString () + "for " + gameObject.transform.parent.name);
		effects [(int)id].playParticle ();

		else effects [(int)id].playParticle (time);
	}

	public void stopEffect(Particle id)
	{
		effects [(int)id].stopParticle ();
	}
    
}

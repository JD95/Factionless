using UnityEngine;
using System.Linq;
using System.Collections.Generic;


using TeamLogic = Utility.TeamLogic;

public class CacheHit_Projectile : MonoBehaviour {

	public Vector3 target;
    public GameObject caster;
	public float heal;
	public float speed;

    public List<GameObject> allies = new List<GameObject>();

    public CacheHit_Projectile(GameObject caster, Vector3 _target)
    {
		target = _target;
	}

	void Update(){

		if(Vector3.Distance(transform.position,target) > 1)
		{
			transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); 
		}
        else
		{
            foreach (var ally in allies.Where(x => x != null))
            {
				//Debug.Log ("Cache hit healing " + ally.name);
                ally.GetComponent<Combat>().recieve_Healing(heal * allies.Count);
            }

			GameObject.Destroy(gameObject);
		}
	}

    void OnTriggerEnter(Collider hit){

		//Debug.Log ("Cache hit collided with " + hit.gameObject.name);

        if (TeamLogic.areAllies(caster, hit.gameObject) && hit.gameObject.name != "AI_Collider")
        {
			//Debug.Log ("Adding " + hit.gameObject.name + " to Chache hit list");
            allies.Add(hit.gameObject);
        }

	}
}

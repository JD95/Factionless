using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sync_PlayerHealth : MonoBehaviour {

    public Combat combat;
    public Image image; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(combat != null)
            image.fillAmount = combat.health / combat.maxHealth;
	}
}

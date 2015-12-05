using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sync_PlayerMana : MonoBehaviour {

    public Image image;
    public Combat combat;
    public Text text;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (combat != null)
        {
            image.fillAmount = combat.mana / combat.maxMana();
            text.text = combat.mana + " / " + combat.maxMana();
        }
	}
}

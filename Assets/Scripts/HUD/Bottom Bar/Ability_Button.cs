using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ability_Button : MonoBehaviour {

    public Bottom_Bar bar;
    public Text text;
    public Slot slot;

    public GameObject panel;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClicked()
    {
        Debug.Log("I am clicked!");
        bar.abilites.trigger_Ability(slot);
    }

    public void OnMouseEnter()
    {
        panel.SetActive(true);
        text.text = bar.abilites.get_ability_description(slot);
    }

    public void OnMouseExit()
    {
        panel.SetActive(false);
        text.text = "";
    }
}

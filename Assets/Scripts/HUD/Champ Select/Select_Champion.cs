using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Select_Champion : MonoBehaviour {

    public NetworkManager network;
    public GameManager game;
    public Champions champ = Champions.Chadi;

    public Selection_Navigation selection_nav;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoLeft()
    {
        var i = (int)champ;
        champ = i != 0 ? (Champions) i - 1 : champ;
        Debug.Log("Looking at " + champ.ToString());
    }

    public void GoRight()
    {
        var i = (int)champ;
        champ = i != 3 ? (Champions) i + 1 : champ;
        Debug.Log("Looking at " + champ.ToString());
    }

    public void OnClicked()
    {
        Debug.Log("Player has selected " + champ.ToString());
        game.selectChampion(champ, network.playerID);

        game.playerIsReady();

        selection_nav.turnOff();

        GetComponent<Button>().interactable = false;
    }
}

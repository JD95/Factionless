using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

    public GameObject gameManager;

	// Use this for initialization
	void Start () {
         
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        gameManager.GetComponent<GameManager>().end_game(Utility.TeamLogic.oppositeTeam(gameObject.tag));
    }
}

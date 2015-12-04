using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class checkPoint : MonoBehaviour {

    public bool activated = false;
    public GameObject[] ChecksPointList;
    public string[] input;
    public Text text;
    public GameObject panel;
    private int traverse = 0;

    // Use this for initialization
    void Start () {
        foreach (GameObject cp in ChecksPointList)
        {
            cp.SetActive(false);
        }

        ChecksPointList[0].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")
        {
            if (traverse == (ChecksPointList.Length)-1)
            {
                panel.SetActive(false);
            }
            else
            {
                Debug.Log("hit checkpoint");
                text.text = input[traverse];
                ChecksPointList[traverse].SetActive(false);
                traverse++;
                ChecksPointList[traverse].SetActive(true);
            }
        }
    }


}

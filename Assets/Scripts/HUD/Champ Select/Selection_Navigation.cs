using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Selection_Navigation : MonoBehaviour {

    public Text display_name;
    public Text display_role;
    public Text display_description;

    public Camera[] cameras;
    public string[] names;
    public string[] roles;
    public string[] descriptions;

    public int current_selection = 0;

	// Use this for initialization
	void Start () {
        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;

        foreach(var camera in cameras)
        {
            camera.enabled = false;
        }

        cameras[current_selection].enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void switch_champ()
    {
        cameras[current_selection].enabled = true;

        display_name.text = names[current_selection];
        display_role.text = "Role: " + roles[current_selection];
        display_description.text = descriptions[current_selection];
    }

    public void GoLeft()
    {
        if(current_selection != 0)
        {
            int old_selection = current_selection;
            current_selection--;
            cameras[old_selection].enabled = false;
            switch_champ();
        }
    }

    public void GoRight()
    {
        if (current_selection != 3)
        {
            int old_selection = current_selection;
            current_selection++;
            cameras[old_selection].enabled = false;
            switch_champ();
        }
    }

    public void turnOff()
    {
        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
        cameras[current_selection].enabled = false;
        enabled = false;
    }
}

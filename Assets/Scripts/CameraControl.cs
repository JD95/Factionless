using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	public Transform target;
	public Vector3 offset;
	private float time;
	private Vector3 desired;
    public bool locked = false;
    public float CamSpeed = 3.00f;
    public float GUIsize = 20.00f;

    void Update () {
        if (Input.GetKeyDown("y"))
            locked = !locked;
        if (locked && target != null && PhotonNetwork.playerList.Length > 0)
        {
            Vector3 newDesired = target.position;
            if (desired != newDesired)
            {
                time = 0;
                desired = newDesired;
            }
            time += Time.deltaTime * 5;
            Vector3 lerpTarget = Vector3.Lerp(transform.position - offset, desired, time);
            transform.position = lerpTarget + offset;
            transform.LookAt(lerpTarget);
        }
        else
        {
    
            var recright = new Rect(0, 0, Screen.width, GUIsize);
            var recleft = new Rect(0, Screen.height - GUIsize, Screen.width, GUIsize);
            var recdown = new Rect(0, 0, GUIsize, Screen.height);
            var recup = new Rect(Screen.width - GUIsize, 0, GUIsize, Screen.height);

            if (Input.GetKey("space"))
                transform.position = target.position + offset;
            if (recdown.Contains(Input.mousePosition))
                transform.Translate(0, 0, -CamSpeed, Space.World);

            if (recup.Contains(Input.mousePosition))
                transform.Translate(0, 0, CamSpeed, Space.World);

            if (recleft.Contains(Input.mousePosition))
                transform.Translate(-CamSpeed, 0, 0, Space.World);

            if (recright.Contains(Input.mousePosition))
                transform.Translate(CamSpeed, 0, 0, Space.World);
           
        }
    }
	
	public void SetTarget(Transform t) {
		enabled = true;
		target = t;
	}
}

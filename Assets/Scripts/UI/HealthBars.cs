using UnityEngine;
using System.Collections;

public class HealthBars : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public RectTransform canvasRectT;
	public RectTransform healthBar;
	public Transform objectToFollow;
	void Update()
	{
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, objectToFollow.position);
		healthBar.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
	}
}

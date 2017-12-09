using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuMouse : MonoBehaviour {

	//RectTransform menu = GetComponent<RectTransform>();

	float screenCenterX = Screen.width / 2;
	float screenCenterY = Screen.height / 2;

	RectTransform m_RectTransform;
	public float XAxis, YAxis;
	void Awake(){
		m_RectTransform = GetComponent<RectTransform> ();
	}
	void Update () {

		XAxis = (screenCenterX - Input.mousePosition.x) / 10;
		YAxis = (screenCenterY - Input.mousePosition.y) / 12;

		float mouseX = screenCenterX;

	}

	void OnGUI() {
		m_RectTransform.anchoredPosition = new Vector2 (XAxis, YAxis);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textBehavior : MonoBehaviour {

	RectTransform m_RectTransform;

	void Awake() {
		m_RectTransform = GetComponent<RectTransform> ();
	}

	void OnGUI() {
		
	}
}
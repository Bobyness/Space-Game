using UnityEngine;
using System.Collections;

public class UISectionToggleButton : MonoBehaviour {

	public GameObject UISection;

	// Use this for initialization
	void Start () {
		UISection.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Clicked () {
		if (UISection.activeSelf == true) {
			UISection.SetActive(false);
		}
		else {
			UISection.SetActive(true);
		}
	}
}

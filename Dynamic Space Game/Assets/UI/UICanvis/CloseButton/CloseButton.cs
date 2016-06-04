using UnityEngine;
using System.Collections;

public class CloseButton : MonoBehaviour {

	public GameObject AttachedTo;
	
	void Start () {
		if (AttachedTo == null) {
			AttachedTo = transform.parent.gameObject;
		}
	}
	
	public void Clicked () {
		AttachedTo.SetActive(false);
	}
}

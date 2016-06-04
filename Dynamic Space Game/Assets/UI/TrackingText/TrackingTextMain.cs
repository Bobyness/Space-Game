using UnityEngine;
using System.Collections;

public class TrackingTextMain : MonoBehaviour {

	public GameObject Target;
	
	Vector3 GetTargetPositionOnScreen () {
		Vector3 Position = CameraMain.GetWorldPositionOnScreen(Target.transform.position);
		return Position;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = GetTargetPositionOnScreen();
	}
}

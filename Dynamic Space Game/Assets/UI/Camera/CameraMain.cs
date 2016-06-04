using UnityEngine;
using System.Collections;

public class CameraMain : MonoBehaviour {

	private float ZoomMomentum;
	public GameObject FollowTarget; //make this private when done testing.
	private Camera MyCamera;
	
	public void SetFollowTarget (GameObject NewTarget) {
		GameObject FollowTarget = NewTarget;
		
	}
	
	private void LookAtAndFollowObject (GameObject TargetObject) {
		Vector3 TargetPosition = TargetObject.transform.position;
		
		Vector3 NewPosition = TargetPosition;
		NewPosition.z = transform.position.z;
		transform.position = NewPosition;
		
		transform.LookAt(TargetPosition);
	}
	
	public static Vector3 GetMouseInWorld () {
		Vector3 MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z*-1);
		Vector3 NewVector = Camera.main.ScreenToWorldPoint(MousePosition);
		NewVector.z = 0;
		return NewVector;
	}
	
	public static Vector2 GetMouseOnScreen () {
		Vector2 MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		return MousePosition;
	}
	
	public static Vector2 GetWorldPositionOnScreen (Vector3 Position) {
		Vector2 ScreenPosition = Camera.main.WorldToScreenPoint(Position);
		return ScreenPosition;
	}
	
	void HandleZoom () {
		float PreviousZ = transform.position.z;
		if (Mathf.Abs(Input.mouseScrollDelta.y) > 0) {
			ZoomMomentum += (Input.mouseScrollDelta.y) * 0.03f;
		}
		
		if (Mathf.Abs(ZoomMomentum) < 0.001f) {
			ZoomMomentum = 0;
		}
		else {
			Vector3 MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
			Vector3 MouseWorldPosition = Camera.main.ScreenToWorldPoint(MousePosition);
			Vector3 CamChange = (transform.position - MouseWorldPosition) * ZoomMomentum;
			transform.position += CamChange;
		}
		if (transform.position.z < -50000) {
			if (ZoomMomentum < 0)
				ZoomMomentum *= 0.1f;
				
			ZoomMomentum += 0.005f;
		}
		ZoomMomentum *= 0.8f;
		if (PreviousZ < -5000 && transform.position.z >= -5000) {
			GameObject RootPlanet = GameWorld.Instance.RootPlanet;
			IList Children = GameWorld.GetChildObjects(RootPlanet);
			GameObject Nearest = GameWorld.InstanceNearest(transform.position, Children);
			Vector3 RelativePosition = transform.position - Nearest.transform.position;
			print(Nearest);
			GameWorld.Instance.SetViewedPlanet(Nearest.GetComponent<PlanetMain>());
			transform.position = new Vector3(RelativePosition.x, RelativePosition.y, transform.position.z);
		}
	}
	
	void Start () {
		MyCamera = GetComponent<Camera>();
		ZoomMomentum = 0;
	}
	
	// Update is called once per frame
	void Update () {
		HandleZoom();
		if (PlayerControl.GetControlHeld(KeyCode.F)) {
			LookAtAndFollowObject (FollowTarget);
		}
	}
}

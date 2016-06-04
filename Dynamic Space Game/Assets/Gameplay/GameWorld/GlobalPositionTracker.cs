using UnityEngine;
using System.Collections;

public class GlobalPositionTracker : MonoBehaviour {

	public Vector2 AbsolutePosition;

	// Use this for initialization
	void Start () {
		ResetAbsolutePosition (GameWorld.Instance.transform.position);
	}
	
	public void ResetAbsolutePosition (Vector2 WorldPosition) {
		float PosX = transform.position.x - WorldPosition.x;
		float PosY = transform.position.y - WorldPosition.y;
		Vector2 NewPosition = new Vector2(PosX, PosY);
		AbsolutePosition = NewPosition;
	}
	
	public void SetUnityPosition (Vector2 WorldPosition) {
		float PosX = WorldPosition.x + AbsolutePosition.x;
		float PosY = WorldPosition.y + AbsolutePosition.y;
		transform.position = new Vector2 (PosX, PosY);
	}
}
/*
public class DVector2 {
	public double x;
	public double y;
	public DVector2 (double NewX, double NewY) {
		x = NewX;
		y = NewY;
	}
}
*/
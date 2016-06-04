using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameWorld : MonoBehaviour {

	public static GameWorld Instance;
	
	public GameObject RootTitle;
	public GameObject RootPlanet;
	
	public GameObject StockShip;
	public GameObject StockStation;
	
	public List<string> CargoTypes;
	
	void Awake () {
		Instance = this;
	}
	
	void Start () {
		CargoTypes.Add("Power");
		CargoTypes.Add("Food");
		CargoTypes.Add("Metal");
		Application.targetFrameRate = 60;
	}
	
	public void SetViewedPlanet (PlanetMain ViewedPlanet) {
		transform.position = ViewedPlanet.GetComponent<GlobalPositionTracker>().AbsolutePosition *-1;
		foreach (Transform Planet in RootPlanet.transform) {
			GlobalPositionTracker PlanetGPT = Planet.GetComponent<GlobalPositionTracker>();
			PlanetGPT.SetUnityPosition(transform.position);
			
		}
	}
	
	public GameObject GetRootTitle () {
		return RootTitle;
	}

	public static float GetDirectionDifference (float Direction1, float Direction2) {
		float Difference = Direction1 - Direction2;
		if (Mathf.Abs(Difference) >= 180) {
			if (Direction1 > Direction2) {
				Difference = 360 - Direction1 + Direction2;
				Difference*=-1;
			}
			else {
				Difference = 360 - Direction2 + Direction1;
			}
		}
		return Difference;
	}
	
	public static float PointDirection (Vector2 FirstPosition, Vector2 SecondPosition) {
		Vector2 RelativePosition = SecondPosition - FirstPosition;
		float Direction = Mathf.Atan2(RelativePosition.y, RelativePosition.x) * Mathf.Rad2Deg;;
		Direction += 270;
		return Direction;
	}
	
	public static GameObject InstanceNearest (Vector3 Position, IList Objects) { //returns the nearest GameObject in the list
		float LowestDistance = Mathf.Infinity;
		GameObject Nearest = null;
		foreach (GameObject Instance in Objects) {
			float Distance = (Position - Instance.transform.position).sqrMagnitude;
			if (Distance < LowestDistance) {
				Nearest = Instance;
				LowestDistance = Distance;
			}
		}
		return Nearest;
	}
	
	public static IList GetChildObjects (GameObject Parent) {
		List<Transform> Transforms = new List<Transform>(Parent.GetComponentsInChildren<Transform>());
		Transforms.RemoveAt(0);
		IList Objects = new ArrayList();
		foreach (Transform Trans in Transforms) {
			if (Trans.parent == Parent.transform) {
				Objects.Add(Trans.gameObject);
			}
		}
		return Objects;
	}
	
	public static void SpawnShip (GameObject Planet, Vector2 RelativePosition) {
		PlanetMain PlanetScript = Planet.GetComponent<PlanetMain>();
		GameObject NewShip = Instantiate(GameWorld.Instance.StockShip);
		ShipMain NewShipMain = NewShip.GetComponent<ShipMain>();
		NewShip.transform.SetParent(PlanetScript.Ships.transform);
		NewShip.transform.localPosition = RelativePosition;
	}
}

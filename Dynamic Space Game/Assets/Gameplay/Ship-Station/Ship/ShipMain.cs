using UnityEngine;
using System.Collections;

public class ShipMain : MonoBehaviour {
	//Requires a RigidBody2D componant.
	//To use, attach a controling class as a child of the ship
	//This could be an AI or a player handler. 
	
	AutoPilotTypes AutoPilot = AutoPilotTypes.None;
	Vector3 AutoPilotDestination = new Vector3();
	
	
	public float MaxThrust;
	public float Dampening;
	public float MaxRotationSpeed;
	public float Mass; //May still be used later for dealing with collisions
	public float WarpSpeed;
	public Inventory MyInventory;
	
	private Vector3 Velocity;
	private float RotationVelocity;
	private bool Warp;
	private Vector3 WarpVector;
	private Vector3 WarpDestination;
	private int WarpTimer;
	
	private float CaulculateEffectiveMaxThrust () { //obsolete because I'm using the regular MaxThrust value now.
		return MaxThrust / Mass;
	}
	
	public void AttemptAccelerate (float Direction, float Thrust, bool Absolute = false) {
		if (Thrust > MaxThrust)
			Thrust = MaxThrust;
		
		MoveAccelerate(Direction, MaxThrust, Absolute);
	}
	
	public void AttemptTurn (Vector2 TurnTo, float Speed) {
		float DirectionToTarget = GameWorld.PointDirection(transform.position, TurnTo);
		float RelativeDirection = GameWorld.GetDirectionDifference(transform.rotation.eulerAngles.z, DirectionToTarget);
		if (Mathf.Abs(RelativeDirection) <= Speed) {
			Speed = RelativeDirection;
		}
		if (RelativeDirection < -1) {
			Turn(Speed);
		}
		else if (RelativeDirection > 1) {
			Turn(Speed*-1);
		}
	}
	
	private void MoveAccelerate (float Direction, float Thrust, bool Absolute = false) {
		//A direction of 0 is forward.
		if (Absolute == false)
			Direction += transform.rotation.eulerAngles.z;
			
		Direction *= Mathf.Deg2Rad;
		float AccelX = Mathf.Sin(Direction);
		AccelX*=-1;
		float AccelY = Mathf.Cos(Direction);
		Vector2 NewAccel = new Vector2(AccelX, AccelY);
		Velocity += (Vector3)NewAccel * Thrust;
		//MyRigidBody.AddRelativeForce(NewAccel * Thrust);
	}
	
	public void StartWarp (Vector3 Destination) {
		float MissFactor = 0.2f;
		Vector3 Offset = new Vector3((Random.value - 0.5f) * (MissFactor * 2f), (Random.value - 0.5f) * (MissFactor * 2f));
		Warp = true;
		WarpDestination = Destination + Offset;
		WarpVector = (WarpDestination - transform.position).normalized;
		WarpVector *= WarpSpeed;
		transform.localEulerAngles = new Vector3(0, 0, GameWorld.PointDirection(transform.position, WarpDestination));
		Velocity = new Vector3(0, 0, 0);
		RotationVelocity = 0;
		WarpTimer = Mathf.RoundToInt(Vector3.Distance(transform.position, WarpDestination) / WarpSpeed);
		WarpDestination -= GameWorld.Instance.transform.position;
	}
	
	private void HandleWarp () {
		transform.position += WarpVector;
		WarpTimer -= 1;
		if (WarpTimer <= 0) {
			transform.position = WarpDestination + GameWorld.Instance.transform.position; //This is called seperately because EndWarp can be called externally
			EndWarp();
		}
	}
	
	public void EndWarp () {
		IList Children = GameWorld.GetChildObjects(GameWorld.Instance.RootPlanet);
		GameObject Nearest = GameWorld.InstanceNearest(transform.position, Children);
		GameObject NearestShipList = Nearest.GetComponent<PlanetMain>().Ships;
		transform.SetParent(NearestShipList.transform);
		Warp = false;
	}
	
	private void Turn (float Speed) {
		Vector3 CurrentRotation = transform.rotation.eulerAngles;
		if (Mathf.Abs(Speed) >= MaxRotationSpeed) {
			if (Speed < 0) {
				Speed = (MaxRotationSpeed*-1);
			}
			else {
				Speed = MaxRotationSpeed;
			}
		}
		transform.Rotate(0, 0, Speed);
	}
	
	public void TurnAccelerate (float Torque) {
		RotationVelocity += Torque;
		//MyRigidBody.AddTorque(Torque);
	}
	
	public void StartAutoMove (Vector3 Destination) { // Autopilots the ship to a destination. For use by AI and player.
		AutoPilotDestination = Destination - GameWorld.Instance.transform.position;
		AutoPilot = AutoPilotTypes.Move;
		//EndWarp();
	}
	
	void AutoMove (Vector3 Destination) {
		if (Warp == false) {
			Destination += GameWorld.Instance.transform.position;
			float Distance = Vector2.Distance(transform.position, Destination);
			AttemptTurn(Destination, 10);
			float Direction = Mathf.Atan2(Velocity.y, Velocity.x) * Mathf.Rad2Deg;
			Direction += 270;
			float DirectionDifference = GameWorld.GetDirectionDifference(Direction, GameWorld.PointDirection(transform.position, Destination));
			
			if (Distance > (Velocity.magnitude / MaxThrust) / 10) {
				AttemptAccelerate(0, MaxThrust);
			}
			else {
				if (Mathf.Abs(DirectionDifference) <= 90) {
					AttemptAccelerate(Direction += 180, Velocity.magnitude / 10, true);
				}
				else {
					AttemptAccelerate(0, MaxThrust);
				}
				if (Distance <= 0.01f && Velocity.magnitude <= 0.001f) {
					AutoPilot = AutoPilotTypes.None;
					Velocity = Vector2.zero;
				}
			}
			if (Distance >= WarpSpeed * 5) {
				StartWarp(Destination);
			}
		}
	}
	
	void Awake () {
		MyInventory = GetComponent<Inventory>();
	}
	
	// Use this for initialization
	void Start () {
		//MyRigidBody = GetComponent<Rigidbody2D>();
		//EffectiveMaxThrust = CaulculateEffectiveMaxThrust();
	}
	
	// Update is called once per frame
	void Update () {
		switch (AutoPilot) {
			case AutoPilotTypes.Move:
				AutoMove(AutoPilotDestination);
				break;
				
			default:
				
				break;
		}
		if (Warp == false) {
			transform.localPosition += Velocity;
			Velocity *= Dampening;
			transform.Rotate(0, 0, RotationVelocity);
		}
		else {
			HandleWarp();
		}
	}
}
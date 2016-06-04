using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour {
	
	// Attach this to a character to allow the player to control them.
	// There should only ever be zero or one instances of this class at a time.
	
	public static bool GetControlHeld (KeyCode Key) { //Returns true if the key is being held
		if (Input.GetKey(Key)) {
			return true;
		}
		else {
			return false;
		}
	}
	
	public static bool GetControlPressed (KeyCode Key) { //Returns true if the key is was just pressed
		if (Input.GetKeyDown(Key)) {
			return true;
		}
		else {
			return false;
		}
	}
	
	private CharacterMain GetCurrentCharacterMain () {
		// This assumes that this classes object is attached to a character.
		GameObject MyCharObject = this.transform.parent.gameObject; 
		if (MyCharObject == null) {
			return null;
		}
		else {
			return MyCharObject.GetComponent<CharacterMain>();
		}
	}
	
	private ShipMain GetCurrentShipMain () { // Returns the ShipMain of the ship this
		CharacterMain MyCharacter = GetCurrentCharacterMain();
		ShipMain MyShip = MyCharacter.GetCurrentShipMain();
		if (MyShip == null) {
			return null;
		}
		else {
			return MyShip;
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey && !EventSystem.current.IsPointerOverGameObject()) {
			ShipMain ShipControl = GetCurrentShipMain(); // I can get away with this because there is only one player.
			if (GetControlHeld(KeyCode.W)) {
				ShipControl.AttemptAccelerate(0, 10);
			}
			
			if (GetControlHeld(KeyCode.Mouse1)) {
				ShipControl.AttemptTurn(CameraMain.GetMouseInWorld(), 10);
			}
			
			if (GetControlHeld(KeyCode.Mouse0)) {
				ShipControl.StartAutoMove(CameraMain.GetMouseInWorld());
			}
			if (GetControlPressed(KeyCode.A)) {
				PlanetMain ParentPlanetMain = ShipControl.transform.GetComponentInParent<PlanetMain>();
				IList Stations = GameWorld.GetChildObjects(ParentPlanetMain.Stations);
				GameObject NearestStation = GameWorld.InstanceNearest(transform.position, Stations);
				Inventory StationInventory = NearestStation.GetComponent<Inventory>();
				ShipControl.MyInventory.TransferCargo(StationInventory, "Hunks", 1);
			}
		}
	}
}

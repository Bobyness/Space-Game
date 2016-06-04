using UnityEngine;
using System.Collections;

public class CharacterMain : MonoBehaviour {
	
	public int Money;
	
	public ShipMain GetCurrentShipMain () {
		return GetComponentInParent<ShipMain>();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

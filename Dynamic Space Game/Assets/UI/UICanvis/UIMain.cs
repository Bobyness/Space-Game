using UnityEngine;
using System.Collections;

public class UIMain : MonoBehaviour {

	public static UIMain Instance;
	
	public GameObject TrackingText;
	public InventoryButtons CargoButtons;
	public InventoryButtons TradeButtons;

	void Awake () {
		Instance = this;
	}
}

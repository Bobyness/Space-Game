using UnityEngine;
using System.Collections;

public class WheelMenuButton : MonoBehaviour {

	public WMBTypes Type;
	public WheelMenu ParentScript;

	void Awake () {
		Type = WMBTypes.None;
	}
	
	public void Clicked () {
		switch (Type) {
			case WMBTypes.Trade:
				TradeButtonClicked();
				break;
				
			default:
				
				break;
		}
	}
	
	private void TradeButtonClicked () {
		InventoryButtons CargoButtons = UIMain.Instance.CargoButtons;
		InventoryButtons TradeButtons = UIMain.Instance.TradeButtons;
		TradeButtons.ReferencedInventory = ParentScript.Target.GetComponent<Inventory>();
		TradeButtons.IsPlayerInventory = false;
		GameObject TradePanel = TradeButtons.transform.parent.parent.gameObject;
		GameObject CargoPanel = CargoButtons.transform.parent.parent.gameObject;
		TradePanel.SetActive(true);
		CargoPanel.SetActive(true);
	}
	
	// Use this for initialization
	void Start () {
		print (Type);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

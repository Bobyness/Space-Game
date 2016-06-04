using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CargoButton : MonoBehaviour {

	public string MyItemType;

	public int MyItemQuantity;

	private Text MyText;
	private Button MyButton;
	private InventoryButtons ParentScript;
	
	Inventory GetOtherInventory () { //Returns the inventory of the other inventory panel
		Inventory Destination = null;
		if (ParentScript.IsPlayerInventory == true) {
			Destination = UIMain.Instance.TradeButtons.ReferencedInventory;
		}
		else {
			Destination = UIMain.Instance.CargoButtons.ReferencedInventory;
		}
		return Destination;
	}
	
	void TransferItems (int Quantity) {
		Inventory Provider = ParentScript.ReferencedInventory;
		Inventory Destination = GetOtherInventory();
		if (UIMain.Instance.TradeButtons.InRange == true) {
			ParentScript.ReferencedInventory.TransferCargo(Destination, MyItemType, Quantity);
		}
	}
	
	void TradeItems (int Quantity) { //This transfers money as well as the items.
		Inventory MyInventory = ParentScript.ReferencedInventory;
		Inventory OtherInventory = GetOtherInventory();
		if (ParentScript.IsPlayerInventory == true) {
			MyInventory.SellItem(OtherInventory, MyItemType);
		}
		else {
			OtherInventory.BuyItem(MyInventory, MyItemType);
		}
	}
	
	public void Clicked () {
		TradeItems(1);
	}

	// Use this for initialization
	void Start () {
		MyText = GetComponentInChildren<Text>();
		MyButton = GetComponent<Button>();
		ParentScript = GetComponentInParent<InventoryButtons>();
		
	}
	
	// Update is called once per frame
	void Update () {
		MyItemQuantity = ParentScript.ReferencedInventory.GetQuantityOfItem(MyItemType);
		MyText.text = MyItemType + "\n" + MyItemQuantity.ToString();
		if (MyItemQuantity <= 0) {
			Destroy(gameObject);
		}
		if (ParentScript.InRange == true) {
			MyButton.interactable = true;
		}
		else {
			MyButton.interactable = false;
		}
	}
}

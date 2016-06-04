using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryButtons : MonoBehaviour {

	public Inventory ReferencedInventory;
	public GameObject CashDisplay;
	public bool IsPlayerInventory;
	public GameObject CargoButtonPrefab;
	public bool InRange;

	void CreateNewCargoButton (string Type) {
		GameObject NewButton = Instantiate(CargoButtonPrefab);
		NewButton.transform.SetParent(this.transform);
		CargoButton NewButtonScript = NewButton.GetComponent<CargoButton>();
		NewButtonScript.MyItemType = Type;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ReferencedInventory != null) {
			InRange = true;
			if (!IsPlayerInventory) {
				Vector3 MyPosition = ReferencedInventory.transform.position;
				Vector3 PlayerPosition = UIMain.Instance.CargoButtons.ReferencedInventory.transform.position;
				if (Vector2.Distance(MyPosition, PlayerPosition) > GameConfig.Instance.TradeDistance) {
					InRange = false;
				}
			}
			
			CashDisplay.GetComponent<Text>().text = ReferencedInventory.Money.ToString();
			
			IList ChildCargoButtons = GetComponentsInChildren<CargoButton>();
			IList AllCargo = ReferencedInventory.GetAllCargo();
			foreach(Cargo CurrentItem in AllCargo) {
				bool FoundCorrectButton = false;
				foreach(CargoButton ChildButton in ChildCargoButtons) {
					if (ChildButton.MyItemType == CurrentItem.Type) {
						if (FoundCorrectButton == true) { //This ensures no repeat buttons
							Destroy(ChildButton.gameObject);
						}
						else {
							FoundCorrectButton = true;
						}
					}
				}
				if (FoundCorrectButton == false) { //If no button exists for the item type then it is created here
					CreateNewCargoButton(CurrentItem.Type);
				}
			}
		}
	}
}

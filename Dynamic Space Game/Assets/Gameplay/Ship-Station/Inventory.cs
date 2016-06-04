using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public List<Cargo> MyCargo;
	public float Money;
	
	public void TransferCargo (Inventory Destination, string Type, int Quantity) { // May cause bugs if Quantity is < 0
		if (GetQuantityOfItem(Type) >= Quantity) {
			ChangeQuantityOfItem(Type, Quantity*-1);
			Destination.ChangeQuantityOfItem(Type, Quantity);
		}
	}
	
	public void ChangeQuantityOfItem (string Type, int Amount) {
		Cargo Item = FindCargoOfType(Type);
		if (Item == null) {
			Cargo NewCargo = new Cargo();
			NewCargo.Type = Type;
			MyCargo.Add(NewCargo);
			Item = NewCargo;
		}
		Item.Quantity += Amount;
		if (Item.Quantity <= 0) {
			MyCargo.Remove(Item);
		}
	}
	
	public int GetQuantityOfItem (string Type) {
		int Quantity = 0;
		Cargo Item = FindCargoOfType(Type);
		if (Item != null)
			Quantity = Item.Quantity;
			
		return Quantity;
	}
	
	public IList GetAllCargo () {
		IList AllCargo = new ArrayList();
		foreach(Cargo Item in MyCargo) {
			AllCargo.Add(Item);
		}
		return AllCargo;
	}
	
	public void BuyItem (Inventory Vendor, string Type) {
		if (TransferMoney(this, Vendor, Vendor.GetSellPrice(Type))) {
			Vendor.TransferCargo(this, Type, 1);
		}
	}
	
	public void SellItem (Inventory Buyer, string Type) {
		if (TransferMoney(Buyer, this, Buyer.GetBuyPrice(Type))) {
			this.TransferCargo(Buyer, Type, 1);
		}
	}
	
	public bool TransferMoney (Inventory Provider, Inventory Receiver, float Quantity) {
		if (Provider.Money >= Quantity) {
			Provider.Money -= Quantity;
			Receiver.Money += Quantity;
			return true;
		}
		else {
			return false;
		}
	}
	
	public float GetSellPrice (string Type) {
		float Price = Mathf.Infinity;
		Cargo CurrentItem = FindCargoOfType(Type);
		if (GetQuantityOfItem(Type) > CurrentItem.SellDownTo) {
			Price = CurrentItem.SellPrice;
		}
		return Price;
	}
	
	public float GetBuyPrice (string Type) {
		float Price = Mathf.Infinity;
		Cargo CurrentItem = FindCargoOfType(Type);
		if (GetQuantityOfItem(Type) < CurrentItem.BuyUpTo) {
			Price = CurrentItem.BuyPrice;
		}
		return Price;
	}
	
	private Cargo FindCargoOfType (string Type) {
		Cargo CargoInstance = null;
		foreach(Cargo Item in MyCargo) {
			if (Item.Type == Type) {
				CargoInstance = Item;
			}
		}
		return CargoInstance;
	}
	
	void Start () {
		MyCargo = new List<Cargo>();
		ChangeQuantityOfItem ("Hunks", 6);
		ChangeQuantityOfItem ("Foods", 6);
	}
	
	void Update () {
		
	}
}

public class Cargo {
	public string Type;
	public int Quantity;
	
	//Extra values for the AI to use and change
	public float BuyUpTo = 20;
	public float BuyPrice = 0.8f;
	public float SellDownTo = -2;
	public float SellPrice = 1;
}
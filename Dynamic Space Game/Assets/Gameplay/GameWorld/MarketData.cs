using UnityEngine;
using System.Collections;

public class MarketData : MonoBehaviour {

	IList TradeRoutes;
	
	void RefreshTradeRoutes () {
		
	}
	
	void Start () {
		RefreshTradeRoutes();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public class TradeRoute {
	public GameObject Origin;
	public GameObject Destination;
	public string CargoType;
	public int Quantity;
}
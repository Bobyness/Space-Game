using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WheelMenu : MonoBehaviour {

	public GameObject WheelMenuButtonPrefab;
	public IList ButtonsList;
	public GameObject Target;
	
	public void CreateButtons (bool Trade = false, bool Diplomacy = false, bool None3 = false, bool None4 = false, bool None5 = false, bool None6 = false, bool None7 = false, bool None8 = false) {
		if (Trade == true) {
			CreateTradeButton();
		}
	} //This function sucks, I know
	
	private void CreateTradeButton () {
		WheelMenuButton NewButtonScript = Instantiate(WheelMenuButtonPrefab).GetComponent<WheelMenuButton>();
		Text NewButtonText = NewButtonScript.GetComponentInChildren<Text>();
		
		NewButtonScript.transform.SetParent(this.transform);
		NewButtonScript.transform.localPosition = new Vector2(-80, 0);
		
		NewButtonScript.Type = WMBTypes.Trade;
		NewButtonText.text = "Trade";
		NewButtonScript.ParentScript = this;
		
		ButtonsList.Add(NewButtonScript.gameObject); //This may not be needed
	}
	
	void Awake () {
		ButtonsList = new ArrayList(8); //Limited to 8 buttons, lists the game objects
	}
	
	// Use this for initialization
	void Start () {
		CreateButtons(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Target != null)
			transform.position = CameraMain.GetWorldPositionOnScreen(Target.transform.position);
	}
}
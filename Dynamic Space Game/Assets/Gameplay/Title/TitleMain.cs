using UnityEngine;
using System.Collections;

public class TitleMain : MonoBehaviour {
	
	public string Name; //This is the name shown
	public float Legitimacy; //This is the strength of the titles claim to existence.
	public GameObject Property; //This is what physical property, if any, the title claims to own
	public GameObject Holder; //This is what character holds this title
	public Color MyColor; //The color of the title
	private Color VisibleColor; //The color of the title as actually shown.
	public IList Subjects; // These are what titles the title owns

	// Use this for initialization
	void Start () {
		Subjects = new ArrayList();
	 	RefreashSubjectsList();
	 	SetVisibleColor();
	}
	
	public TitleMain GetDirectOwner () {
		GameObject Owner = transform.parent.gameObject;
		if (Owner == GameWorld.Instance.RootTitle) {
			 Owner = this.gameObject;
		}
		TitleMain OwnerTitle = Owner.GetComponent<TitleMain>();
		return OwnerTitle;
	}
	
	public TitleMain GetHighestOwner () {
		GameObject Owner = this.gameObject;
		while (Owner.transform.parent.gameObject != GameWorld.Instance.RootTitle) {
			Owner = Owner.transform.parent.gameObject;
		}
		TitleMain OwnerTitle = Owner.GetComponent<TitleMain>();
		return OwnerTitle;
	}
	
	private void SetVisibleColor () { //Sets the visible color value then updates the property icon color.
		TitleMain HighestOwner = GetHighestOwner();
		VisibleColor = Color.Lerp(HighestOwner.MyColor, MyColor, 0.1f);
		Property.GetComponentInChildren<ObjectGraphicsHandler>().IconColor = VisibleColor;
	}
	
	private void RefreashSubjectsList () {
		Subjects.Clear();
		foreach (Transform Child in transform) {
			TitleMain CurrentTitleMain = Child.GetComponent<TitleMain>();
			if (Child.GetComponent<TitleMain>() != null) {
				Subjects.Add(CurrentTitleMain);
			}
		}
		//The following lines are just for debuging
		foreach (TitleMain Subject in Subjects) {
			 print(Subject.Name);
		}
		//End of debugging lines
	}
	
}
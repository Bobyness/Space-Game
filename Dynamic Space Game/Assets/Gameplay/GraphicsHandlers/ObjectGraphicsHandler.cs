using UnityEngine;
using System.Collections;

public class ObjectGraphicsHandler : MonoBehaviour {

	public Sprite NormalSprite;
	public Sprite IconSprite;
	public float IconMaxScale;
	public float DistanceToUseIcon;
	public Color IconColor;
	private SpriteRenderer MySpriteRenderer;
	private bool UsingIcon;

	public void SetIcon (bool UseIcon) {
		if (UseIcon == true) {
			MySpriteRenderer.sprite = IconSprite;
			UsingIcon = true;
		}
		else {
			MySpriteRenderer.sprite = NormalSprite;
			UsingIcon = false;
		}
	}
	
	private void SetColor (Color NewColor) {
		MySpriteRenderer.color = NewColor;
	}

	void HandleSettingIcon () {
		if (Camera.main.transform.position.z <= DistanceToUseIcon*-1) {
			SetIcon(true);
		}
		else {
			SetIcon(false);
		}
	}
	
	void Awake () {
		
	}

	// Use this for initialization
	void Start () {
		MySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
		HandleSettingIcon();
		if (UsingIcon == true) {
			float NewScale = Camera.main.transform.position.z;
			NewScale*=-1; //Invert the value because the camera decreases in z as it moves farther away
			if (NewScale > IconMaxScale)
				NewScale = IconMaxScale;
				
			transform.localScale = new Vector3(1, 1) * NewScale;
			SetColor(IconColor);
		}
		else {
			transform.localScale = new Vector3(1, 1);
			SetColor(Color.white);
		}
	}
}

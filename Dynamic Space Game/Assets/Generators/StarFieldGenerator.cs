using UnityEngine;
using System.Collections;

public class StarFieldGenerator : MonoBehaviour {

	public Sprite StarSprite;

	private void GenerateStarfield (float Size, int NumberOfStars) {
		float MinY = transform.position.y - (Size / 2f);
		float MaxY = transform.position.y + (Size / 2f);
		float MinX = transform.position.x - (Size / 2f);
		float MaxX = transform.position.x + (Size / 2f);
		float MinZ = 20000;
		float MaxZ = 50000;
		
		for (int i = 0; i < NumberOfStars; i += 1) {
			//Sets the values for position and size
			float NewX = Random.Range(MinX, MaxX);
			float NewY = Random.Range(MinY, MaxY);
			float NewZ = Random.Range(MinZ, MaxZ);
			Vector3 NewPosition = new Vector3(NewX, NewY, NewZ);
			float NewSize = NewZ * Random.Range(0.05f, 0.1f);
			Vector3 NewScale = new Vector3(NewSize, NewSize, 1);
			
			//Creates the object and sets the position as generated above
			GameObject NewStar = new GameObject();
			Transform NewStarTransform = NewStar.transform;
			NewStarTransform.position = NewPosition;
			NewStarTransform.localScale = NewScale;
			
			//Adds the sprite component and sets it to the StarSprite variable
			SpriteRenderer NewStarSpriteRenderer = NewStar.AddComponent<SpriteRenderer>();
			NewStarSpriteRenderer.sprite = StarSprite;
			
			//Gives the star a random color
			float NewRed = Random.Range(0.9f, 1);
			float NewGreen = Random.Range(0.9f, 1);
			float NewBlue = Random.Range(0.9f, 1);
			NewStarSpriteRenderer.color = new Color(NewRed, NewGreen, NewBlue);
		}
	}
	
	void Start () {
		GenerateStarfield(200000, 3000);
	}
}

using UnityEngine;
using System.Collections;

public class GameConfig : MonoBehaviour {

	public static GameConfig Instance;
	
	public float TradeDistance;
	
	void Awake () {
		Instance = this;
	}
}

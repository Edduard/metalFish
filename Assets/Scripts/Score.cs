using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text score; 

	void Update(){
		float sc = Mathf.Round(PlayerPrefs.GetFloat ("score"));
		score.text = "Score: " + sc;
	}
}
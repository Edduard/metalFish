using UnityEngine;
using System.Collections;

public class scroll : MonoBehaviour {

	public float speed = 0.5f;
	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		speed = 0.5f + ((player.GetComponent<Player> ().energy - 50f > 0) ? ((player.GetComponent<Player> ().energy - 50f) / 50f * 0.5f) : 0);


		Vector2 offset = new Vector2 (Time.time * speed, 0);
		GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}

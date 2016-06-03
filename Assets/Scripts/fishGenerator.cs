using UnityEngine;
using System.Collections;

public class fishGenerator : MonoBehaviour {
	public GameObject player;
	public GameObject fish1;
	public GameObject fish2;
	public GameObject fish3;
	public GameObject fish4;
	public GameObject fish5;
	public GameObject shark;
	public float spawnTime = 3f;
	float currentTime;
	float spawnInterval;
	Vector3 min_fish_size;
	float max_fish_size;

	// Use this for initialization
	void Start () {
		spawnInterval = Random.value * 4f;
		currentTime = 0f;
		min_fish_size = new Vector3 (0.15f, 0.15f, 0.15f);
		max_fish_size = 0.25f;
	}

	void createFish() {
		int rand = Random.Range(1,7);
		float seed = Random.value;
		if (rand == 6)
			Debug.Log ("SHARK!");
		switch (rand) {
		case 1:
			fish1.GetComponent<Transform> ().localScale = new Vector3 (1, 1, 1) * seed * max_fish_size + min_fish_size;
			fish1.GetComponent<fishBehaviour> ().fishSpeed = (1 - seed) / 8f + 0.05f;
			Instantiate (fish1);
			break;
		case 2:
			fish2.GetComponent<Transform>().localScale = new Vector3(1, 1, 1) * seed * max_fish_size + min_fish_size;
			fish2.GetComponent<fishBehaviour> ().fishSpeed = (1 - seed) / 8f + 0.05f;
			Instantiate (fish2);
			break;
		case 3:
			fish3.GetComponent<Transform>().localScale = new Vector3(1, 1, 1) * seed * max_fish_size + min_fish_size;
			fish3.GetComponent<fishBehaviour> ().fishSpeed = (1 - seed) / 8f + 0.05f;
			Instantiate (fish3);
			break;
		case 4:
			fish4.GetComponent<Transform>().localScale = new Vector3(1, 1, 1)*  seed * max_fish_size + min_fish_size;
			fish4.GetComponent<fishBehaviour> ().fishSpeed = (1 - seed) / 8f + 0.05f;
			Instantiate (fish4);
			break;
		case 5:
			fish5.GetComponent<Transform>().localScale = new Vector3(1, 1, 1) * seed * max_fish_size + min_fish_size;
			fish5.GetComponent<fishBehaviour> ().fishSpeed = (1 - seed) / 8f + 0.05f;
			Instantiate (fish5);
			break;
		case 6:
			shark.GetComponent<Transform> ().localScale = new Vector3 (1, 1, 1) * seed * 0.16f + min_fish_size;
			shark.GetComponent<sharkBehaviour> ().fishSpeed = (1 - seed/2) / 8f + 0.05f;
			shark.GetComponent<sharkBehaviour> ().lightSpeed = (1 - seed/2) / 8f + 0.05f;
			Instantiate (shark);
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime >= spawnInterval) {
			spawnTime = 5f - player.GetComponent<Player> ().energy * 3.4f / 100f;
			if (spawnTime < 1.0f)
				spawnTime = 1.0f;
			createFish ();
			currentTime = 0;
			spawnInterval = Random.value * spawnTime;
		}
	}
}
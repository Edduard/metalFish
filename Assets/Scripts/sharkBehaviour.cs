using UnityEngine;
using System.Collections;

public class sharkBehaviour : MonoBehaviour {
	public float fishSpeed;
	public float lightSpeed;
	public float direction;
	GameObject player;
	private float timerCurrent = 0f;
	private float limit = 0.5f;
	private int dir;
	private float angle;
	bool rotated = false;
	public bool following = false;
	Vector3 last_known_pos;
	float speed_aux;
	float light_aux;

	public float rotationAngle = 0f;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (50f, (Random.value - 0.5f) * 8f, -1f);
		dir = 0;
		timerCurrent = 0f;
		angle = 60f;
		rotationAngle = 0;
		transform.Rotate (new Vector3 (0, 0, 180));
		player = GameObject.Find ("player");
		fishSpeed = 0.05f;
		speed_aux = fishSpeed;
		light_aux = lightSpeed;
	}

	void Rotate() {
		if (rotationAngle <= 0) {
			float deltaX = transform.position.x - 0;
			float deltaY = transform.position.y - 0;
			rotationAngle = Mathf.Atan2 (deltaY, deltaX) * 180.0f / Mathf.PI;
		} else {
			Vector3 ang = new Vector3 (0, 0, 2f);
			transform.Rotate (ang);
			rotationAngle -= 2f;
		}
	}

	void move() {
		fishSpeed = speed_aux * ((player.GetComponent<Player> ().energy - 50f > 0) ? ((player.GetComponent<Player> ().energy - 50f) / 50f * 2f) : 1f);
		lightSpeed = light_aux * ((player.GetComponent<Player> ().energy - 50f > 0) ? ((player.GetComponent<Player> ().energy - 50f) / 50f * 2f) : 1f);
		if(following)
			transform.Translate (new Vector3(lightSpeed, (dir == 1) ? (Random.value / angle) : (-Random.value / angle)));
		else
			transform.Translate (new Vector3 (fishSpeed, (dir == 1) ? (Random.value / angle) : (-Random.value / angle)));
	}

	// Update is called once per frame
	void Update () {
		timerCurrent += Time.deltaTime;
		if (timerCurrent >= limit) {
			timerCurrent = 0;
			angle = Random.value * 80f + 40f;
			limit = Random.value * 1f;
			dir = (dir == 1) ? 0 : 1;
			rotated = false;
		}
		if (Input.GetKey ("x")) {

			if (!rotated) {
				Vector3 new_pos = player.GetComponent<Transform> ().position - new Vector3(-1.52f,-.23f,0);
				float deltaX = transform.position.x - new_pos.x;
				float deltaY = transform.position.y - new_pos.y;
				rotationAngle = Mathf.Atan2 (deltaY, deltaX) * 180.0f / Mathf.PI;
				rotated = true;
				if (Mathf.Abs (rotationAngle) > 75f || Vector3.Distance (transform.position, new_pos) > 25) {
					following = false;
				} else {
					last_known_pos = new_pos;
					following = true;
				}
			}

			if(following)
				transform.Rotate (new Vector3(0, 0, (rotationAngle - transform.rotation.eulerAngles.z + 180f) / 5f));

			move ();
		} else {
			if (rotated) {
				rotated = false;
			}
			if (following) {
				transform.Rotate (new Vector3 (0, 0, (rotationAngle - transform.rotation.eulerAngles.z + 180f) / 5f));
			} else {
				transform.Rotate (new Vector3 (0, 0, (180f - transform.rotation.eulerAngles.z) / 5f));
			}
			move ();
			if (Vector3.Distance (transform.position, last_known_pos) < 2f ||
				Vector3.Distance (transform.position, last_known_pos) > 25) {
				following = false;
			}
		}
		if (transform.position.x < -14) {
			Destroy (gameObject);
		}
	}
}
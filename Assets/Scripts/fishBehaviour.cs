using UnityEngine;
using System.Collections;

public class fishBehaviour : MonoBehaviour {
	public float fishSpeed;
	public float direction;
	GameObject player;
	private float timerCurrent = 0f;
	private float limit = 0.5f;
	private int dir;
	private float angle;
	bool rotated = false;
	Vector3 rot_speed;
	float speedAux;

	public float rotationAngle = 0f;


	public fishBehaviour(int direction, float speed) {
		this.direction = direction;
		this.fishSpeed = speed;
		dir = 0;
		timerCurrent = 0f;
		angle = 40f;
		Start ();
	}

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (50f, (Random.value - 0.5f) * 8f, -1f);
		dir = 0;
		timerCurrent = 0f;
		angle = 0f;
		rotationAngle = 0;
		transform.Rotate (new Vector3 (0, 0, 180));
		rot_speed = new Vector3 (0, 0, 7f);
		player = GameObject.Find ("player");
		speedAux = fishSpeed;
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
		fishSpeed = speedAux * (1f + ((player.GetComponent<Player> ().energy - 50f > 0) ? ((player.GetComponent<Player> ().energy - 50f) / 50f) : 0f));
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
				Vector3 zero = player.GetComponent<Transform> ().position - new Vector3(-1.52f,-.23f,0);
				float deltaX = transform.position.x - zero.x;
				float deltaY = transform.position.y - zero.y;
				rotationAngle = Mathf.Atan2 (deltaY, deltaX) * 180.0f / Mathf.PI;
				rotated = true;
			}

			transform.Rotate (new Vector3(0, 0, (rotationAngle - transform.rotation.eulerAngles.z + 180f) / 5f));

			move ();
		} else {
			if (rotated) {
				rotated = false;
			}
			transform.Rotate (new Vector3(0, 0, (180f - transform.rotation.eulerAngles.z) / 5f));
			move ();
		}
		if (transform.position.x < -14) {
			Destroy (gameObject);
		}
	}
}
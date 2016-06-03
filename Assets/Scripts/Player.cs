using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	// The force which is added when the player jumps
	// This can be changed in the Inspector window
	public float speed = 50;
	public GameObject Light;
	public GameObject passiveLight;
	public float energy;
    public float score;
	public float lightIntensity;
    bool attackMode;                   
    bool finishAttack;               
    Vector3 playerRealPos;                 
    Vector3 attackPos;

	float last_eat = 0f;
	float animCancel = 0.1f;

    public Camera mainCam;

    Animator anim;


	//HealthBar

	public RectTransform healthTransformGreen;
	public RectTransform healthTransform;

	public float cachedY;
	private float minXValue;
	private float maxXValue;
	private float currentHealth;
	public float coolDown;



	public float maxHealth = 100f;
	public Image visualHealth;

    //Sounds
    // The source where the sound comes from
    private AudioSource source;

    //	The sound when the player is jumping
    public AudioClip bgMusic;

    // The sound when player crashes
    public AudioClip crashSound;


    void Start () 
	{
		Light.GetComponent<Light> ().intensity = 0;
		energy = 50f;
        update_light();
        attackMode = false;                     
        finishAttack = false;
        source = GetComponent<AudioSource>();
        source.PlayOneShot(bgMusic, 1F);
        score = 0;
		PlayerPrefs.SetFloat ("score", score);
        anim = GetComponent<Animator>();
        mainCam.GetComponent<Camera>().orthographicSize = 7.3f;

		//HealthBar
		cachedY = healthTransform.position.y;
		maxXValue = healthTransform.position.x;
		minXValue = healthTransform.position.x - healthTransform.rect.width;
		currentHealth = maxHealth;
	}

    void update_light()
	{
		if (energy < 100) {	
			lightIntensity = 1.5f; //energy / 100f * 2.5f;

			Light.GetComponent<Light> ().spotAngle = 30f + energy / 100f * 150f;
			Light.GetComponent<Light> ().range = 12f + energy / 100f * 13f;
			currentHealth = energy;
			HandleHealth ();
		}
	}
	void update_size(){
		if (energy < 100) {	
			transform.localScale = new Vector3(1f, 1f, 0f) * energy / 50f;
			Debug.Log (energy / 50f);

			Light.GetComponent<Transform> ().localScale = new Vector3 (1, 1, 1);
			passiveLight.GetComponent<Transform> ().localScale = new Vector3 (1, 1, 1);

		}
	}

	// Update is called once per frame
	void Update ()
	{
		last_eat += Time.deltaTime;
		if (last_eat < animCancel) {
			anim.SetBool ("eat", false);
		}
        energy -= 0.003f;
		update_light (); // for health bar
	//	update_size();
		//Widen the camera

		if (energy < 50) {
			if (mainCam.GetComponent<Camera> ().orthographicSize > 7.3f)
				mainCam.GetComponent<Camera> ().orthographicSize -= 0.01f;
		}
		if ((energy > 50) && (energy < 60)) {
			if (mainCam.GetComponent<Camera> ().orthographicSize > 8f)
				mainCam.GetComponent<Camera> ().orthographicSize -= 0.01f;
			if (mainCam.GetComponent<Camera> ().orthographicSize < 8f)
				mainCam.GetComponent<Camera> ().orthographicSize += 0.01f;
		}
		if ((energy > 60) && (energy < 70)){
			if (mainCam.GetComponent<Camera> ().orthographicSize > 9f)
				mainCam.GetComponent<Camera> ().orthographicSize -= 0.01f;
			if (mainCam.GetComponent<Camera> ().orthographicSize < 9f)
				mainCam.GetComponent<Camera> ().orthographicSize += 0.01f;
		}
		if ((energy > 70) && (energy < 90)) {
			if (mainCam.GetComponent<Camera> ().orthographicSize > 10f)
				mainCam.GetComponent<Camera> ().orthographicSize -= 0.01f;
			if (mainCam.GetComponent<Camera> ().orthographicSize < 10f)
				mainCam.GetComponent<Camera> ().orthographicSize += 0.01f;
		}
		if (energy > 90) {
			if (mainCam.GetComponent<Camera> ().orthographicSize < 10f)
				mainCam.GetComponent<Camera> ().orthographicSize += 0.01f;
		}

		if (Input.GetKey ("x")) {
			passiveLight.GetComponent<Light> ().intensity = 0f;
			//turn up the light slowly
			if (Light.GetComponent<Light> ().intensity < lightIntensity)
				Light.GetComponent<Light> ().intensity += 1;
			energy -= 0.02f;
			update_light ();
		} else if ((!Input.GetKey ("x")) && (Light.GetComponent<Light> ().intensity > 0f))
			Light.GetComponent<Light> ().intensity = Light.GetComponent<Light> ().intensity - 1;
		else {
			passiveLight.GetComponent<Light> ().intensity = 1f;
		}

        // get the mouse position
        if (!attackMode && !finishAttack)
        {
            playerRealPos = Input.mousePosition;
            playerRealPos.z = -1;
            playerRealPos = Camera.main.ScreenToWorldPoint(playerRealPos);
            playerRealPos.x = GetComponent<Transform>().position.x;
        }

	    if (Input.GetMouseButton (0)) {

		    // Move to the position of the mouse
		    float step = speed * Time.deltaTime;
		    GetComponent<Rigidbody2D> ().position = Vector3.MoveTowards(GetComponent<Transform> ().position, playerRealPos, step);
	    }

        //      
        //Attack
        //                                                
        if (Input.GetMouseButtonDown(1) && (energy > 25) )
        {
            energy -= 10;
            attackMode = true;
            //get the mouse position
            attackPos = Input.mousePosition;
            attackPos.z = -1;
            attackPos = Camera.main.ScreenToWorldPoint(attackPos);
        }

        if (attackMode)
        {
            // Move to the position of the mouse
            GetComponent<Rigidbody2D>().position = Vector3.MoveTowards(GetComponent<Transform>().position, attackPos, 150 * Time.deltaTime);
            if (Mathf.Round(GetComponent<Rigidbody2D>().position.x) == Mathf.Round(attackPos.x) && Mathf.Round(GetComponent<Rigidbody2D>().position.y) == Mathf.Round(attackPos.y))
            {
                finishAttack = true;
                attackMode = false;
            }
        }
        // Return to the position
        if (finishAttack)
        {
            GetComponent<Rigidbody2D>().position = Vector3.MoveTowards(GetComponent<Transform>().position, playerRealPos, 20 * Time.deltaTime);
            if (Mathf.Round(GetComponent<Rigidbody2D>().position.x) == Mathf.Round(playerRealPos.x) && Mathf.Round(GetComponent<Rigidbody2D>().position.y) == Mathf.Round(playerRealPos.y)) finishAttack = false;
        }
        //
        //end attack  
        //                                      
    }


    //
    //Collide with something
    //
	IEnumerator OnTriggerEnter2D (Collider2D other) 
	{
		if (other.CompareTag ("small_fish")) {
            // PLayer eats something
			anim.SetBool("eat", true);
            if (energy <= 90f)
            {
                energy += other.gameObject.GetComponent<Transform>().localScale.x * 15f;
                score += other.gameObject.GetComponent<Transform>().localScale.x * 15f;
				PlayerPrefs.SetFloat ("score", score);
                Debug.Log(Mathf.Round(score));
            }
            update_light();
            Destroy (other.gameObject);
		} else {
            // Player dies - animation
            anim.SetBool ("dead", true);
			last_eat = 0f;

            // Wait a few seconds
			yield return new WaitForSeconds (3f);

            // Dim the light
            Light.GetComponent<Light>().range = 230f;
            Light.GetComponent<Light>().transform.position = new Vector3(0, 0, -122f);
            Light.GetComponent<Light>().intensity = 2.5f;
 
            //play diyng sound - nu merge...
            source = GetComponent<AudioSource>();
            source.PlayOneShot(crashSound, 1F);
            source.Stop();

            Application.LoadLevel("TappyDied");

            Destroy(this);
        }
	}
	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	// misca banda verde
	private void HandleHealth()
	{
		float currentXValue;

		if (currentHealth > 100) {
			currentXValue = MapValues (100, 0, maxHealth, minXValue, maxXValue);
		} else {
			currentXValue = MapValues (currentHealth, 0, maxHealth, minXValue, maxXValue);
		}

		healthTransformGreen.position = new Vector3 (currentXValue, cachedY);

		if (currentHealth > maxHealth / 2) {  //mai mult de 50% viata
			visualHealth.color = new Color32((byte)MapValues(currentHealth, maxHealth/2, maxHealth, 255, 0), 255, 0, 255);
		} else {
			visualHealth.color = new Color32 (255, (byte)MapValues(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
		}
	}
}
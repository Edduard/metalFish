using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// The source where the sound comes from
	private AudioSource source;

	//	The sound when the player clicks
	public AudioClip clickSound;

	void Start () 
	{
		source = GetComponent<AudioSource>();
	}

	public void NewGame()
	{
		//source.PlayOneShot (clickSound, 40F);
		Application.LoadLevel ("TappyPlane");
	}
	public void Continue()
	{
		//source.PlayOneShot (clickSound, 40F);
		Application.LoadLevel ("TappyPlane");
	}
	public void Exit()
	{
		//source.PlayOneShot (clickSound, 40F);
		Application.Quit();
	}

	public void Inventory()
	{
		Application.LoadLevel ("Inventory");
	}
		
	public void FirstPage()
	{
		Application.LoadLevel ("FirstScreen");
	}
}

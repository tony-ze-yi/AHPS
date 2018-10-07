using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))]

public class videoPlayer : MonoBehaviour {

	public MovieTexture movie;
	private AudioSource audio;

	// Use this for initialization
	void Start () {
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		audio = GetComponent<AudioSource>();
		audio.clip = movie.audioClip;
		movie.Play ();
		audio.Play ();
		StartCoroutine (changeScene());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator changeScene() {
		//Instantiate the scene here so it preloads. Better for performance
		var result = SceneManager.LoadSceneAsync ("Main");
		//But don't allow the scene to be activated
		result.allowSceneActivation = false;
		//Wait for 56 seconds
		yield return new WaitForSeconds (56);
		//Activate the scene
		result.allowSceneActivation = true;
	}
}

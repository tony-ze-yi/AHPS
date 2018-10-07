using UnityEngine;
using System.Collections;

public class soundPlayerScript : MonoBehaviour {
	public AudioClip anthem;
	public float maxDistance;
	public float minDistance;


	//Star spangled banner and stars and stripes forever
	private AudioSource banner;



	// Use this for initialization
	void Start () {
		banner.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public AudioSource AddAudio (AudioClip clip, bool loop, bool playawake, float vol)
	{
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();

		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = playawake;
		newAudio.volume = vol;
		newAudio.maxDistance = maxDistance;
		newAudio.minDistance = minDistance;

		return newAudio;
	}

	public void Awake()
	{
		banner = AddAudio(anthem, true, false, 1);
		//stars = AddAudio(stars, t, false, 1);
	}
}

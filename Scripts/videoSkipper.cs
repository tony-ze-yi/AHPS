using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class videoSkipper : MonoBehaviour {
	
	public void onStart() {
		//Instantiate the scene here so it preloads. Better for performance
		Debug.Log("Hello");
		//var result = SceneManager.LoadSceneAsync ("Main");

		SceneManager.LoadScene ("Main");

	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RankUpUIScript : MonoBehaviour {

    public GUITexture _myTexture;    //The guiTexture you want to manipulate. Set this up in the inspector.
    public float stayTime = 2f;   // Time before fading starts
    public float fadeTime = 2f;   // How long we want to fade
    private float lastStartTime = -100f;

    void Update()
    {
        if(_myTexture.enabled == true) AnimateImage(); //Animate the texture if it is enabled
    }
    void AnimateImage()
    {
        float t = (Time.time - lastStartTime - stayTime) / fadeTime;
        if (t <= 1f)
        {
            Color fullColor = Color.white;
            Color noColor = new Color(1f, 1f, 1f, 0f);
            _myTexture.color = Color.Lerp(fullColor, noColor, t);
        }
        else
        {    //Once we faded out we shut the texture off, in order to save memory
            _myTexture.enabled = false;
        }
    }
    public void ShowImage()
    {    //Call this to start the effect
        _myTexture.enabled = true;
        lastStartTime = Time.time;
    }
}

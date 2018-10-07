using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveNo : MonoBehaviour
{
    public Text txt;
    void Start()
    {

    }

    void FixedUpdate()
    {
        txt.text = "Wave number: " + PlayerScript.WaveNumber;
    }
}

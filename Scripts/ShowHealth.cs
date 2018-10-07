using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowHealth : MonoBehaviour
{
    public Text txt;

    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = "Health: " + PlayerScript.playerHealth + "/100";
    }

    void FixedUpdate()
    {
        txt.text = "Health: " + PlayerScript.playerHealth + "/100";
    }
}

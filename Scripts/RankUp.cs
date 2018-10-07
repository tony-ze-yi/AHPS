using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RankUp : MonoBehaviour
{
    Image image;
    AudioSource rankUpSound;
    void Start()
    {
        image = GetComponent<Image>();
        rankUpSound = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        Debug.Log("Ranked image enabled");
        image = GetComponent<Image>();
        rankUpSound = GetComponent<AudioSource>();
        StartCoroutine(Fade());
    }
    
    IEnumerator Fade()
    {
        rankUpSound.Play();
        yield return new WaitForSeconds(2);
        image.CrossFadeAlpha(0.0f, 3f, false);
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}


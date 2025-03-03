﻿using UnityEngine;
using System.Collections;


public class FadeScript : MonoBehaviour
{
    public Texture2D fadeOutTexture;
    public float fadeSpeed = .8f;

    private int drawDepth = -1000;
    private float alpha = 1f;
    private int fadeDir = -1;
    private AudioSource[] audioSource;
    
    void Awake()
    {
        Time.timeScale = 1;
    }
    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),fadeOutTexture);
    }
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }
    
    void OnLevelWasLoaded()
    {
        alpha = 1;
        BeginFade(-1);
    }

    void FixedUpdate()
    {
        if (PlayerScript.playerHealth <= 0)
        {
            StartCoroutine(GameOver());
        }
    }
    
    void StopAllAudio()
    {
        audioSource = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in audioSource)
        {
            audioS.Stop();
        }
    }

    IEnumerator GameOver()
    {
        BeginFade(1);
        StopAllAudio();
        yield return new WaitForSeconds(3);
    }
}

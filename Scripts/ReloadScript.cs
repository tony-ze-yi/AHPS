using UnityEngine;
using System.Collections;

public class ReloadScript : MonoBehaviour
{

    public static Animation ReloadGun;
    // Use this for initialization
    void Start()
    {
        ReloadGun = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public static void ReloadGunFunc()
    {
        
        ReloadGun.Play();
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class AmmoValues
{
    static public int m1911ammoInMag = 9; //used to store ammo values for different guns
    static public int m1911ammoInStore = 120;
    static public int m1897ammoInMag = 5;
    static public int m1897ammoInStore = 120;
    static public int m4a1ammoInMag = 30;
    static public int m4a1ammoInStore = 240;
}
public class GlobalAmmo : MonoBehaviour
{
    public Text txt;
    
    void OnLevelWasLoaded()
    {
        AmmoValues.m1911ammoInMag = 9; //used to store ammo values for different guns
        AmmoValues.m1911ammoInStore = 120;
        AmmoValues.m1897ammoInMag = 5;
        AmmoValues.m1897ammoInStore = 120;
        AmmoValues.m4a1ammoInMag = 30;
        AmmoValues.m4a1ammoInStore = 240;
    }
    void Start()
    {
        txt = gameObject.GetComponent<Text>(); //gets ammo text indicator
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerScript.GunSelected == 1)
        {
            ShowAmmo(9, AmmoValues.m1911ammoInStore, AmmoValues.m1911ammoInMag); //shows ammo amount depending on gun active
        }
        else if (PlayerScript.GunSelected == 2)
        {
            ShowAmmo(30, AmmoValues.m4a1ammoInStore, AmmoValues.m4a1ammoInMag);
        }
        else
        {
            ShowAmmo(5, AmmoValues.m1897ammoInStore, AmmoValues.m1897ammoInMag);
        }
    }

    void ShowAmmo(int maxAmmo, int ammoInStore, int ammoInMag)
    {
        Mathf.Clamp(ammoInMag, 0, maxAmmo);
        txt.text = "Ammo: " + ammoInMag + "/" + ammoInStore; //displays ammo
    }
}


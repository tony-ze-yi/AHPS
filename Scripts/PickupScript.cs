using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //finds player object
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            AmmoValues.m1911ammoInStore += 30; //adds ammo
            AmmoValues.m1897ammoInStore += 30;
            AmmoValues.m4a1ammoInStore += 60;
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play(); //plays pickup sound
            PlayerScript.numberOfCratesSpawned -= 1;
            Destroy(gameObject); //destroys object
        }
    }
}

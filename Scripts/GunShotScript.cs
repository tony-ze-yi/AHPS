using UnityEngine;
using System.Collections;

public class GunShotScript : MonoBehaviour
{
    public GameObject bulletSpawn;
    public GameObject bullet;
    public GameObject bulletHole;
    public float maxDistance;
    public AudioClip clipGunshot;
    public AudioClip clipReload;
    public float delayTime = 0.1f;
    bool isReloading = false;
    Animator anime;
    public float reloadTime;
    bool isDoneShooting = true;
    public float DamageDone;
    public GameObject MuzzleFlash;
    public GameObject MuzzleFlashSpawnLocation;
    

    private float counter = 0;
    private AudioSource audioReload;
    private AudioSource audioGunshot;

    // Use this for initialization
    void Start()
    {
        anime = GetComponent<Animator>();
        anime.SetBool("IsReloading", false);
    }
    void OnEnable()
    {
        anime.SetBool("IsReloading", false); //to prevent glitches
        isReloading = false;
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R) && !Input.GetKeyDown(KeyCode.Mouse1) && AmmoValues.m1911ammoInStore > 0 && AmmoValues.m1911ammoInMag < 9 && isReloading == false)
        {
            isReloading = true;
            audioReload.Play();
            StartCoroutine(WaitForReload());
        }
        if (Input.GetKey(KeyCode.Mouse0) && counter > delayTime && AmmoValues.m1911ammoInMag > 0 && isReloading == false && isDoneShooting)
        {
            ShootGun();
        }
        else
        {
            anime.SetBool("IsShooting", false);
        }
        counter = counter + Time.deltaTime;
    }   

    IEnumerator WaitForReload()
    {
        anime.SetBool("IsReloading", true);
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        ReloadGun();
        anime.SetBool("IsReloading", false);
        isReloading = false;
    }

    void ShootGun()
	{
		audioGunshot.Play ();
		anime.SetBool ("IsShooting", true);
		Instantiate (bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
		Instantiate(MuzzleFlash, MuzzleFlashSpawnLocation.transform.position, transform.rotation);
		//Instantiate(MuzzleFlash, MuzzleFlashSpawnLocation.transform.position, 90f);
        counter = 0;
        RaycastHit hit;
        Ray ray = new Ray(bulletSpawn.transform.position, bulletSpawn.transform.forward);
        AmmoValues.m1911ammoInMag -= 1;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            //Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            hit.transform.SendMessage("DamageEnemy", DamageDone, SendMessageOptions.RequireReceiver);
        }
    }

    void ReloadGun ()
    {
        if (AmmoValues.m1911ammoInStore > 0 && AmmoValues.m1911ammoInMag < 9)
        {
            if (AmmoValues.m1911ammoInMag + AmmoValues.m1911ammoInStore <= 9)
            {
                AmmoValues.m1911ammoInMag += AmmoValues.m1911ammoInStore;
                AmmoValues.m1911ammoInStore = 0;
            }
            else
            {
                AmmoValues.m1911ammoInStore -= (9 - AmmoValues.m1911ammoInMag);
                AmmoValues.m1911ammoInMag = 9;
            }
        }
    }

    public AudioSource AddAudio (AudioClip clip, bool loop, bool playawake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playawake;
        newAudio.volume = vol;

        return newAudio;
    }

    void Awake()
    {
        audioGunshot = AddAudio(clipGunshot, false, false, 0.6f);
        audioReload = AddAudio(clipReload, false, false, 0.6f);
        isReloading = false;
        isDoneShooting = true;
    }
}

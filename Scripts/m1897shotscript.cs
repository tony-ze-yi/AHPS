using UnityEngine;
using System.Collections;

public class m1897shotscript : MonoBehaviour
{
    public GameObject bulletSpawn; //where bullet will be instantiated
    public GameObject bullet;
    public GameObject bulletHole;
    public float maxDistance;
    public AudioClip clipGunshot;
    public AudioClip clipReload;
    public float delayTime = 0.1f;
    bool isReloading = false;
    Animator anime; //animator controller
    public float reloadTime;
    bool isDoneShooting = true;
    public float DamageDone;
    public GameObject MuzzleFlash;
    public GameObject MuzzleFlashSpawnLocation;
    float ReloadDuration = 1f;
    int i = 0; //for shotgun reload animation

    float TotalReloadTime;
    private float counter = 0; //to delay shots
    private float counterShot = 0;
    private AudioSource audioReload;
    private AudioSource audioGunshot;

    void Start()
    {
        anime = GetComponent<Animator>();
        anime.SetBool("IsReloading", false);
    }

    void OnEnable()
    {
        anime.SetBool("IsReloading", false); //to prevent glitches, reset variables
        isReloading = false;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R) && !Input.GetKeyDown(KeyCode.Mouse1) && AmmoValues.m1897ammoInStore > 0 && AmmoValues.m1897ammoInMag < 5 && isReloading == false)
        {
            anime.SetInteger("ReloadNumber", i); 
            isReloading = true;
            i = 5 - AmmoValues.m1897ammoInMag; //amount to reload
            anime.SetInteger("ReloadNumber", i);
            anime.SetBool("IsReloading", true);
            StartCoroutine(ReloadGun());
        }
        else if (AmmoValues.m1897ammoInMag == 5)
        {
            anime.SetBool("IsReloading", false);
            isReloading = false;
        }
        if (Input.GetKey(KeyCode.Mouse0) && counter > delayTime && AmmoValues.m1897ammoInMag > 0 && isReloading == false && isDoneShooting)
        {
            ShootGun();
        }
        else
        {
            anime.SetBool("IsShooting", false);
        }
        counter = counter + Time.deltaTime;
        counterShot += Time.deltaTime;
    }
    void ShootGun()
    {
        audioGunshot.Play(); //plays shot sound
        anime.SetBool("IsShooting", true);
        Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation); //makes bullet
        Instantiate(MuzzleFlash, MuzzleFlashSpawnLocation.transform.position, MuzzleFlash.transform.rotation); //makes muzzle flash / smoke
        counter = 0;
        RaycastHit hit;
        Ray ray = new Ray(bulletSpawn.transform.position, bulletSpawn.transform.forward);
        AmmoValues.m1897ammoInMag -= 1;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            //Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            hit.transform.SendMessage("DamageEnemy", DamageDone, SendMessageOptions.RequireReceiver); //sends message to enemy to damage self
        }
    }

    IEnumerator ReloadGun()
    {
        anime.SetBool("InReloadLoop", true); //first reload cycle
        yield return new WaitForSeconds(0.8f);
        audioReload.Play();
        ReloadOnce();
        i -= 1;
        anime.SetInteger("ReloadNumber", i);
        while (i > 0) //if needs to reload more than once execute i amount of times
        {
            yield return new WaitForSeconds(ReloadDuration);
            audioReload.Play();
            ReloadOnce();
            i -= 1;
            anime.SetInteger("ReloadNumber", i);
            anime.SetBool("InReloadLoop", false);
        }
    }
    void ReloadOnce()
    {
        AmmoValues.m1897ammoInMag += 1;
        AmmoValues.m1897ammoInStore -= 1;
    }
    public AudioSource AddAudio(AudioClip clip, bool loop, bool playawake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playawake;
        newAudio.volume = vol;

        return newAudio;
    }

    public void Awake()
    {
        audioGunshot = AddAudio(clipGunshot, false, false, 0.6f);
        audioReload = AddAudio(clipReload, false, false, 0.6f);
        isReloading = false;
        isDoneShooting = true;
    }
}

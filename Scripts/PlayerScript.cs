using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SpeechLib;


[System.Serializable]
public class Ranks
{
    public GameObject bronze, silver, gold, platinum, diamond, trump;
    public GameObject bronzeRank, silverRank, goldRank, platinumRank, diamondRank, trumpRank;

}
[System.Serializable]
public class spawnLocations {
	public GameObject spawnLocation1, spawnLocation2, spawnLocation3, spawnLocation4, spawnLocation5, spawnLocation6, spawnLocation7, spawnLocation8, spawnLocation9, spawnLocation10, spawnLocation11, spawnLocation12; 
}
/*
[System.Serializable]
public class SpawnLocations
{
    Vector3 Spawn1 = new Vector3(0,0,0);
}
*/
public class PlayerScript : MonoBehaviour {

	static public float playerHealth = 100f;
    static public int numberOfCratesSpawned = 0; //used to limit amount of crates
    Vector3 spawnLocation; //spawn location for zombies
    public GameObject zombie;
    static public int zombiesKilled = 0; //used for rank system
    public Ranks rank; //used for rank gameobjects
    static public int WaveNumber = 1; //used to spawn more zombies per round
    bool waveFinished = false; //spawns new wave after a bit
    bool canSpawnZombie = true; //prevents multiple waves from spawning
    public static int ZombiesLeft = 0; //determines whether or not to spawn a new wave
    static public int GunSelected = 1; //1 = m1911, 2 = m4a1, 3 = m1897
    public GameObject m1911, m4a1, m1897; //vessels of freedom
    public Slider slider; //health
    public GameObject Crate; //replenish ammo
    bool canSpawnCrate = true; //used to limit amount of crates
    Vector3 crateSpawnLocation; //spawn location for crates
    int zombieNumber = 0;
	bool playerDied = false;
    public GameObject pauseMenu;
    public spawnLocations spawnLocations;
    static public bool isPaused = false;
    private SpVoice voice;
    
    void OnLevelWasLoaded()
    {
        playerHealth = 100f;
        zombiesKilled = 0;
        GunSelected = 1;
        isPaused = false;
        WaveNumber = 1;
        ZombiesLeft = 0;
        numberOfCratesSpawned = 0;
        voice = new SpVoice();
        voice.Volume = 100;
        voice.Rate = 0;
    }
    void Start () 
	{
        voice = new SpVoice();
        StartCoroutine(CreateWave()); //starts wave on spawn after a bit
        voice.Volume = 100; 
        voice.Rate = 0;
    }
	
	void FixedUpdate () 
	{
        slider.value = playerHealth; //updates player health every frame
        
		if (playerHealth <= 0 && playerDied == false) {
            StartCoroutine(LoadDeathScene());
		}

        if (ZombiesLeft == 0 && waveFinished)
        {
            WaveNumber += 1; //increases wave number by 1 and spawns wave of zombies
            StartCoroutine(CreateWave());
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) //activates gun depending on hotkey
        {
            DeactivateGuns();
            GunSelected = 1; 
            m1911.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DeactivateGuns();
            GunSelected = 2;
            m4a1.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DeactivateGuns();
            GunSelected = 3;
            m1897.SetActive(true);
        }
        if (canSpawnCrate && numberOfCratesSpawned <= 200) //spawns crate if less than 200 are in arena
        {
            StartCoroutine(SpawnCrate());
        }
        if (zombieNumber != zombiesKilled)
        {
            RankUp(zombiesKilled);
        }
        zombieNumber = zombiesKilled;
        if (Input.GetKey(KeyCode.Escape) && !isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
        }
    }

    public void RankUpPub(int ZombiesKilled)
    {
        RankUp(ZombiesKilled);
    }

    void RankUp(int ZombiesKilled)
    {
        switch(ZombiesKilled) //sets rank depending on zombies killed
        {
            case 10: rank.bronze.SetActive(true); rank.bronzeRank.SetActive(true); break;
            case 20: rank.silver.SetActive(true); rank.bronze.SetActive(false); rank.silverRank.SetActive(true); break;
            case 40: rank.gold.SetActive(true); rank.silver.SetActive(false); rank.goldRank.SetActive(true); break;
            case 80: rank.platinum.SetActive(true); rank.gold.SetActive(false); rank.platinumRank.SetActive(true); break;
            case 160: rank.diamond.SetActive(true); rank.diamond.SetActive(true); rank.diamondRank.SetActive(true); break;
            case 400: rank.trump.SetActive(true); rank.diamond.SetActive(false); rank.trumpRank.SetActive(true); break;
            default: break;
        }
    }

    void DeactivateGuns() //deactivates guns to activate another one
    {
        m1911.SetActive(false);
        m1897.SetActive(false);
        m4a1.SetActive(false);
    }

    IEnumerator LoadDeathScene()
    {
        playerDied = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("PlayerDied");
    }
    IEnumerator SpawnWave(int WaveNumber)
    {
        int MaxAmountOfZombies = WaveNumber * 15; //spawns wavenumber * 15 amount of zombies
        ZombiesLeft = MaxAmountOfZombies;
        int i = 1;
        for ( ; i <= MaxAmountOfZombies; )
        {
            yield return new WaitForSeconds(2); //waits 2 seconds before spawning a zombie
            SpawnZombies();
            i++;
        }
    }

    IEnumerator CreateWave()
    {
        waveFinished = false;
        yield return new WaitForSeconds(5f); //delays wave so player can get ready
        voice.Speak("Wave number" + WaveNumber, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        StartCoroutine(SpawnWave(WaveNumber));
        Debug.Log("Wave has started");
        waveFinished = true;
    }
    void SpawnZombies()
    {
		
		canSpawnZombie = false;
		int spawner = Random.Range(1, 13); //Determines which spawner to spawn
		if (spawner == 1)
        {
			spawnLocation = spawnLocations.spawnLocation1.transform.position;
		}
        else if (spawner == 2)
        {
			spawnLocation = spawnLocations.spawnLocation2.transform.position;
		}
        else if (spawner == 3)
        {
			spawnLocation = spawnLocations.spawnLocation3.transform.position;
		}
        else if (spawner == 4)
        {
			spawnLocation = spawnLocations.spawnLocation4.transform.position;
		}
        else if (spawner == 5)
        {
			spawnLocation = spawnLocations.spawnLocation5.transform.position;
		}
        else if (spawner == 6)
        {
			spawnLocation = spawnLocations.spawnLocation6.transform.position;
		}
        else if (spawner == 7)
        {
			spawnLocation = spawnLocations.spawnLocation7.transform.position;
		}
        else if (spawner == 8)
        {
			spawnLocation = spawnLocations.spawnLocation8.transform.position;
		}
        else if (spawner == 9)
        {
			spawnLocation = spawnLocations.spawnLocation9.transform.position;
		}
        else if (spawner == 10)
        {
			spawnLocation = spawnLocations.spawnLocation10.transform.position;
		}
        else if (spawner == 11)
        {
			spawnLocation = spawnLocations.spawnLocation11.transform.position;
		}
        else if (spawner == 12)
        {
			spawnLocation = spawnLocations.spawnLocation12.transform.position;
		}
        else if (spawner == 13)
        {
			spawnLocation = spawnLocations.spawnLocation1.transform.position;
		}
		Instantiate(zombie, spawnLocation, zombie.transform.rotation);
		canSpawnZombie = true;
    }

    IEnumerator SpawnCrate()
    {
        canSpawnCrate = false;
        crateSpawnLocation.x = Random.Range(100, 600);
        crateSpawnLocation.y = 0.64f;
        crateSpawnLocation.z = Random.Range(100, 750);
        Instantiate(Crate, crateSpawnLocation, Crate.transform.rotation);
        numberOfCratesSpawned += 1;
        yield return new WaitForSeconds(5);
        canSpawnCrate = true;
        Debug.Log("Spawned crate");
    }
}
/*
        if (zombiesKilled == 5)
        {
            RemoveRankPromotion();
            rank.santorum.SetActive(true);
        }
        else if (zombiesKilled == 10)
        {
            RemoveRankPromotion();
            rank.carson.SetActive(true);
        }
        else if (zombiesKilled == 20)
        {
            RemoveRankPromotion();
            rank.christie.SetActive(true);
        }
        else if (zombiesKilled == 30)
        {
            RemoveRankPromotion();
            rank.paul.SetActive(true);
        }
        else if (zombiesKilled == 40)
        {
            RemoveRankPromotion();
            rank.bush.SetActive(true);
        }
        else if (zombiesKilled == 50)
        {
            RemoveRankPromotion();
            rank.fiorina.SetActive(true);
        }
        else if (zombiesKilled == 60)
        {
            RemoveRankPromotion();
            rank.rubio.SetActive(true);
        }
        else if (zombiesKilled == 70)
        {
            RemoveRankPromotion();
            rank.ted.SetActive(true);
        }
        else if (zombiesKilled == 80)
        {
            RemoveRankPromotion();
            rank.kaisch.SetActive(true);
        }
        else if (zombiesKilled == 90)
        {
            RemoveRankPromotion();
            rank.clinton.SetActive(true);

        }
        else if (zombiesKilled == 120)
        {
            RemoveRankPromotion();
            rank.trump.SetActive(true);
        }
        if (zombiesKilled == 5)
        {
            RemoveRankPromotion();
            rank.santorum.SetActive(true);
        }
        else if (zombiesKilled == 10)
        {
            RemoveRankPromotion();
            rank.carson.SetActive(true);
        }
        else if (zombiesKilled == 20)
        {
            RemoveRankPromotion();
            rank.christie.SetActive(true);
        }
        else if (zombiesKilled == 30)
        {
            RemoveRankPromotion();
            rank.paul.SetActive(true);
        }
        else if (zombiesKilled == 40)
        {
            RemoveRankPromotion();
            rank.bush.SetActive(true);
        }
        else if (zombiesKilled == 50)
        {
            RemoveRankPromotion();
            rank.fiorina.SetActive(true);
        }
        else if (zombiesKilled == 60)
        {
            RemoveRankPromotion();
            rank.rubio.SetActive(true);
        }
        else if (zombiesKilled == 70)
        {
            RemoveRankPromotion();
            rank.ted.SetActive(true);
        }
        else if (zombiesKilled == 80)
        {
            RemoveRankPromotion();
            rank.kaisch.SetActive(true);
        }
        else if (zombiesKilled == 90)
        {
            RemoveRankPromotion();
            rank.clinton.SetActive(true);

        }
        else if (zombiesKilled == 120)
        {
            RemoveRankPromotion();
            rank.trump.SetActive(true);
        }
        void RemoveRankPromotion()
        {
        rank.bush.SetActive(false);
        rank.carson.SetActive(false);
        rank.christie.SetActive(false);
        rank.clinton.SetActive(false);
        rank.fiorina.SetActive(false);
        rank.kaisch.SetActive(false);
        rank.ted.SetActive(false);
        rank.paul.SetActive(false);
        rank.rubio.SetActive(false);
        rank.santorum.SetActive(false);
        rank.trump.SetActive(false);
        }
        public GameObject bush, carson, christie, clinton, fiorina, kaisch, ted, paul, rubio, santorum, trump;
*/

using UnityEngine;
using System.Collections;

public class AmmoBoxSpawner : MonoBehaviour
{
    static int TOT = 0;
    public GameObject someObject;
	//public Rigidbody someObject;
    int MaxObjects = 100;
    float MinTime = .05f;
    float MaxTime = .1f;
    int MinX = 50;
    int MaxX = 450;
    int MinZ = 50;
    int MaxZ = 450;
    // Use this for initialization
    void Start()
    {
       StartCoroutine(SpawnAmmoCrates());

    }

    // Update is called once per frame
    void Update()
    { 
        
    }
    IEnumerator SpawnAmmoCrates()
    {
        while (true)
        {
            if (TOT < MaxObjects)
            {
                yield return new WaitForSeconds(Random.Range(MinTime, MaxTime));
                Vector3 randomPos = new Vector3(Random.Range(MinX, MaxX), 5, Random.Range(MinZ, MaxZ));
                Instantiate(someObject, randomPos, Quaternion.identity);
                TOT++;
            }
        }
    }
}



using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    GameObject player;
    private float enemyHealth = 15f;
	UnityEngine.AI.NavMeshAgent nav;
	Animator animator;
    public float timeBetweenAttacks = 15f;
    public float enemyDamage = 10;
    private bool playerInRange;
    private float timer;
    private bool setToDestroy = false;
    public float enemyTurnSpeed = 5.0f;
    private float counter = 0;
    public float damageTaken = .5f;
    private bool StopDamage = false;
	public Collider rb;
    
    void DamageEnemy (float DamageTaken)
    {
        enemyHealth -= DamageTaken; //for raycast send message
    }

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player"); //finds player and targets him
		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();

	}
    void Start()
    {
		animator = GetComponent<Animator>(); 
		rb = GetComponent<Collider>(); //idk why we need a rigidbody philippe pls explain
    }

    void FixedUpdate()
    {
		if (enemyHealth > 0 && PlayerScript.playerHealth > 0 && playerInRange == false)
		{
			nav.SetDestination (player.transform.position); //TARGET ACQUIRED 
		}

        if (PlayerScript.playerHealth <= 0)
        {
            animator.SetBool("IsPlayerDead", true); //zombie supposed to perform a little dance, but we were too lazy to implement that

        }

        if (enemyHealth <= 0)
        {
            animator.SetBool("IsDead", true);
            StopDamage = true; //to prevent dead body from damaging player
            StartCoroutine(DestroyInTime()); //to delay destruction and ensures zombie death is logged only once
			rb.enabled = false;
            if (setToDestroy)
            {
                Destroy(gameObject);
                PlayerScript.zombiesKilled += 1;
                PlayerScript.ZombiesLeft -= 1;
                Debug.Log(PlayerScript.zombiesKilled);
            }
        }

        if (playerInRange)
        {
            nav.enabled = false;
            if (counter > timeBetweenAttacks && StopDamage == false)
            {
                PlayerScript.playerHealth -= damageTaken;
                counter = 0;
            }
            counter += Time.deltaTime;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is in range.
            playerInRange = true;
            nav.enabled = false;
            animator.SetBool("IsInRange", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
            nav.enabled = true;
            animator.SetBool("IsInRange", false);
        }
    }

    IEnumerator DestroyInTime()
    {
        yield return new WaitForSeconds(3.5f);
        setToDestroy = true;
    }
}






//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), enemyTurnSpeed * Time.deltaTime);
//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), enemyTurnSpeed * Time.deltaTime);
//transform.position += transform.forward * enemyApproachSpeed * Time.deltaTime;
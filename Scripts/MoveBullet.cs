using UnityEngine;
using System.Collections;

public class MoveBullet : MonoBehaviour
{
    public float bulletSpeed;
	public GameObject bullet;
	public float destroyTime;
    // Use this for initialization
    void Start()
    {
		StartCoroutine(destroyBullet());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0, 0, bulletSpeed);
    }

	IEnumerator destroyBullet () {
		yield return new WaitForSeconds(destroyTime);
		Destroy (bullet.gameObject);
	}
}

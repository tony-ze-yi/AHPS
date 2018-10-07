using UnityEngine;
using System.Collections;

public class GunshotDamage : MonoBehaviour
{
    public int DamageAmount = 5;
    public float AllowedRange = 50f; 
    public float TargetDistance;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, AllowedRange))
        {
            hit.transform.SendMessage("DamageEnemy", DamageAmount);
        }
    }
}

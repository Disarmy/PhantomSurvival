using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrigger : MonoBehaviour
{
    public float damage = 10.0f;

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy")
        {
            GameObject target = GameObject.Find(other.name);
            target.GetComponent<EnemyController>().takeDamage(damage);
        }
    }
}

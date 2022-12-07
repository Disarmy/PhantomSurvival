using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public bool isHit;
    public float hitTime = 0.5f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = player.transform.position - transform.position;

        if (!isHit)
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(hitTime);
        isHit = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Damage Obj로 교체할 예정
        if(collision.transform.tag == "Player")
        {
            if (!isHit)
            {
                isHit = true;
                StartCoroutine(Hit());
            }
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);

        EnemySpawner.instance.InsertQueue(gameObject);
    }

    private void OnEnable()
    {
        //enemy init
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSkull : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distance = 0.3f;
    public float attackDistance = 10.0f;

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
        }
    }

    private void Move()
    {
        if (player == null)
            return;

        Vector3 dir = player.transform.position - transform.position;
        transform.Translate(dir * speed * Time.deltaTime);

        if(Mathf.Abs(dir.x) < distance && Mathf.Abs(dir.y) < distance)
        {
            //CoinÈ¹µæ
            KillEnemy();
            Destroy(gameObject);
        }
    }

    private void KillEnemy()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, attackDistance);

        for(int i = 0; i < hit.Length; i++)
        {
            if(hit[i].tag == "Enemy")
            {
                hit[i].GetComponent<EnemyController>().Die();
            }
        }

    }

    public void SetPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj;
        speed *= 2;
    }
}

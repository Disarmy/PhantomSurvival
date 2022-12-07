using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public int HP;
    private int maxHP = 100;
    public float respawnTime = 1.0f;

    Color color = new Color(255, 255, 255, 255);
    public GameObject[] dropitems;

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
    }

    void Die()
    {
        //create random dropitem
        int dropindex = Random.Range(0, dropitems.Length);

        Instantiate(dropitems[dropindex], transform.position, Quaternion.identity);

        //start respawn coroutine
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        color.a = 0;
        spr.color = color;
        yield return new WaitForSeconds(respawnTime);
        color.a = 255;
        spr.color = color;
        HP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HP > 0 && collision.gameObject.tag == "Player")
        {
            HP -= 50;
            if (HP <= 0)
            {
                Die();
            }
        }
    }
}

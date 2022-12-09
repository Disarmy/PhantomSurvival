using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHat : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distance = 0.3f;

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
            GainAllItems();
            Destroy(gameObject);
        }
    }

    private void GainAllItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        for(int i = 0; i < items.Length; i++)
        {
            items[i].SendMessage("SetPlayer");
        }

    }
    public void SetPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj;
        speed *= 2;
    }
}

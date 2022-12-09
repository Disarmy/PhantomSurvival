using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    private SpriteRenderer sr;
    public float speed;
    public bool isHit = false;
    public bool isStop = false;
    public float hitTime = 0.5f;
    public float stopTime = 3.0f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = player.transform.position - transform.position;

        if (!isHit && !isStop)
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
        //Damage Obj로 교체할 예정 TimeStop 상태에서는 안맞아야함
        if(collision.transform.tag == "Player")
        {
            if (!isHit)
            {
                isHit = true;
                StartCoroutine(Hit());
            }
        }

        //player에게 데미지 주기
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

    public void TimeStopFunc()
    {
        StartCoroutine(TimeStop());
    }

    IEnumerator TimeStop()
    {
        isStop = true;
        Color originalColor = sr.material.color;
        sr.material.color = Color.black;
        yield return new WaitForSeconds(stopTime);
        sr.material.color = originalColor;
        isStop = false;
    }
}

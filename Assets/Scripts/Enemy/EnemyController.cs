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
    public float hp = 10.0f;
    public float maxHp = 100.0f;
    public float myExp = 1;

    public GameObject exp;

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

    public void takeDamage(float damage)
    {
        StartCoroutine(Hit(damage));
    }

    IEnumerator Hit(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Die();
        }
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
                takeDamage(collision.gameObject.GetComponent<PlayerStat>().getDamage());
            }
        }

        //player에게 데미지 주기
    }
    public void Die()
    {
        gameObject.SetActive(false);
        

        //경험치 소환
        GameObject e = Instantiate(exp, transform.position, Quaternion.identity);
        e.GetComponent<ItemExp>().SetExp(myExp);

        EnemySpawner.instance.InsertQueue(gameObject);
    }

    private void OnEnable()
    {
        //enemy init
        hp = maxHp;
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

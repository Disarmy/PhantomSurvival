using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject monster;
    public GameObject player;

    public static EnemySpawner instance;
    public Queue<GameObject> m_queue = new Queue<GameObject>();
    public float spawnTime = 1f;
    public float spawnDistance = 5.0f;
    public int maxCount = 10;
    public float stopTime = 5.0f;
    public bool isStop = false;

    // Update is called once per frame
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        for (int i = 0; i < maxCount; i++)
        {
            GameObject t_Obj = Instantiate(monster, this.gameObject.transform);
            m_queue.Enqueue(t_Obj);
            t_Obj.SetActive(false);
        }

        StartCoroutine(EnemySpawn());
    }

    public void InsertQueue(GameObject obj)
    {
        m_queue.Enqueue(obj);
        obj.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject obj = m_queue.Dequeue();
        obj.SetActive(true);

        return obj;
    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {
            if(m_queue.Count != 0 && !isStop)
            {
                GameObject t_obj = GetQueue();
                Vector2 point = new Vector2(player.transform.position.x, player.transform.position.y);
                t_obj.transform.position = SetRandomPoint(point, spawnDistance, true);
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private Vector3 SetRandomPoint(Vector2 center, float radius = 0, bool normalized = false){
        Vector2 randomPoint = (normalized ? center + Random.insideUnitCircle.normalized * radius : center + Random.insideUnitCircle * radius);

        return randomPoint;
    }
    public void TimeStopFunc()
    {
        StartCoroutine(TimeStop());
    }

    IEnumerator TimeStop()
    {
        isStop = true;
        yield return new WaitForSeconds(stopTime);
        isStop = false;
    }
}

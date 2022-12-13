using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotateSpeed = 1.0f;
    private Vector3 moveDirection = Vector3.zero;
    public GameObject attackObj;
    private float attackDirection;
    public float fireDuration = 3.0f;

    void Awake()
    {
        attackDirection = attackObj.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(inputX, inputY, 0);

        if (inputX != 0 || inputY != 0)
        {
            Rotate(inputX, inputY);
        }

        transform.position += moveDirection * speed * Time.deltaTime;
    }
    public void OnFire()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        Transform fire = attackObj.transform.Find("Fire");
        fire.gameObject.SetActive(true);
        yield return new WaitForSeconds(fireDuration);
        fire.gameObject.SetActive(false);
    }

    void Rotate(float x, float y)
    {
        if(x == 1)
        {
            if (attackDirection != 0)
            {
                if (attackDirection > 0 && attackDirection <= 180)
                    attackDirection -= rotateSpeed;
                else
                {
                    attackDirection += rotateSpeed;
                    if (attackDirection >= 360)
                        attackDirection = 0;
                }
            }
        }
        if(x == -1)
        {
            if (attackDirection != 180)
            {
                if (attackDirection > 180 && attackDirection <= 360)
                    attackDirection -= rotateSpeed;
                else
                {
                    attackDirection += rotateSpeed;
                }
            }
        }
        if(y == 1)
        {
            if (attackDirection != 90)
            {
                if (attackDirection >= 270)
                {
                    attackDirection += rotateSpeed;
                    if (attackDirection >= 360)
                        attackDirection = 0;
                }
                else if (attackDirection > 90)
                    attackDirection -= rotateSpeed;
                else
                    attackDirection += rotateSpeed;
            }
        }
        if(y == -1)
        {
            if (attackDirection != 270)
            {
                if (attackDirection > 270)
                    attackDirection -= rotateSpeed;
                else if (attackDirection <= 90)
                {
                    attackDirection -= rotateSpeed;
                    if (attackDirection <= 0)
                        attackDirection = 360;
                }
                else
                    attackDirection += rotateSpeed;
            }
        }

        attackObj.transform.rotation = Quaternion.Euler(0, 0, attackDirection);
    }
}

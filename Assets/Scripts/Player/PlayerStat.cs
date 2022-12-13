using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int level = 1;
    public float exp = 0;
    public float maxExp = 5;
    public float damage = 1.0f;

    public void TakeExp(float value)
    {
        exp += value;

        //·¹º§¾÷
        if(exp >= maxExp)
        {
            exp = exp - maxExp;
            maxExp *= 2;
            level += 1;
        }
    }

    public float getDamage()
    {
        return damage;
    }
}

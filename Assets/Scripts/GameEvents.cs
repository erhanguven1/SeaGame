using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    public event Action<Ship> onEnterFight, exitFight;
    private void Awake()
    {
        current = this;
    }
    void EnterFight(Ship who)
    {
        if (onEnterFight!=null)
        {
            onEnterFight(who);
        }
    }

}

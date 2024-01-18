using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public EnemyData data;

    private void Start()
    {
        var stats = gameObject.AddComponent<Stats>();
        stats.TransferData(data);
    }
}

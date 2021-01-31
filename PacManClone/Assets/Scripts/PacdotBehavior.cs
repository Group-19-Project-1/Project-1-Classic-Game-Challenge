﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacdotBehavior : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "Player")
            Destroy(gameObject);
    }
}
